using System.Globalization;
using System.Text;

namespace POS;

internal sealed class ProductCategory
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Icon { get; init; }
}

internal sealed class Product
{
    public required string Barcode { get; init; }
    public required string Name { get; init; }
    public required string CategoryId { get; init; }
    public required decimal Price { get; init; }
    public required int Stock { get; set; }
    public required string Icon { get; init; }
    public required string ImageFile { get; init; }
}

internal sealed class SaleLine
{
    public required string SaleId { get; init; }
    public required DateTime SoldAt { get; init; }
    public required Product Product { get; init; }
    public required int Quantity { get; init; }
    public required decimal SaleTotal { get; init; }
    public required decimal CashReceived { get; init; }
    public required decimal Change { get; init; }
}

internal sealed class CsvDataStore
{
    private const string CategoriesHeader = "id,name,icon";
    private const string ProductsHeader = "barcode,name,category_id,price,stock,icon,image";
    private const string SalesHeader = "sale_id,sold_at,barcode,product_name,quantity,unit_price,line_total,sale_total,payment_method,cash_received,change";

    private readonly string _categoriesPath;
    private readonly string _productsPath;
    private readonly string _salesDirectory;

    public CsvDataStore()
    {
        string currentDirectoryData = Path.Combine(Environment.CurrentDirectory, "Data");
        string executableDirectoryData = Path.Combine(AppContext.BaseDirectory, "Data");
        string dataDirectory = File.Exists(Path.Combine(currentDirectoryData, "categories.csv"))
            ? currentDirectoryData
            : executableDirectoryData;

        Directory.CreateDirectory(dataDirectory);
        _categoriesPath = Path.Combine(dataDirectory, "categories.csv");
        _productsPath = Path.Combine(dataDirectory, "products.csv");
        _salesDirectory = Path.Combine(dataDirectory, "Sales");

        EnsureFile(_categoriesPath, CategoriesHeader);
        EnsureFile(_productsPath, ProductsHeader);
        Directory.CreateDirectory(_salesDirectory);
        EnsureFile(GetSalesPath(DateTime.Now), SalesHeader);
    }

    public List<ProductCategory> LoadCategories() =>
        ReadRows(_categoriesPath).Select(row => new ProductCategory
        {
            Id = Get(row, "id"),
            Name = Get(row, "name"),
            Icon = Get(row, "icon")
        }).ToList();

    public List<Product> LoadProducts()
    {
        List<Product> products = ReadRows(_productsPath).Select(row => new Product
        {
            Barcode = Get(row, "barcode"),
            Name = Get(row, "name"),
            CategoryId = Get(row, "category_id"),
            Price = decimal.Parse(Get(row, "price"), CultureInfo.InvariantCulture),
            Stock = int.Parse(Get(row, "stock"), CultureInfo.InvariantCulture),
            Icon = Get(row, "icon"),
            ImageFile = Get(row, "image")
        }).ToList();

        if (products.Any(product => string.IsNullOrWhiteSpace(product.Barcode)))
        {
            throw new InvalidDataException("Every product must have a barcode in products.csv.");
        }

        if (products.Select(product => product.Barcode).Distinct(StringComparer.OrdinalIgnoreCase).Count() != products.Count)
        {
            throw new InvalidDataException("Each product barcode in products.csv must be unique.");
        }

        return products;
    }

    public void CompleteSale(IReadOnlyCollection<SaleLine> lines, IReadOnlyCollection<Product> updatedProducts)
    {
        if (lines.Count == 0)
        {
            throw new ArgumentException("A sale must contain at least one line.", nameof(lines));
        }

        string salesPath = GetSalesPath(lines.First().SoldAt);
        List<string> salesRows = File.Exists(salesPath)
            ? File.ReadAllLines(salesPath, Encoding.UTF8).ToList()
            : [];
        if (salesRows.Count == 0)
        {
            salesRows.Add(SalesHeader);
        }

        salesRows.AddRange(lines.Select(line => string.Join(",",
            Escape(line.SaleId),
            Escape(line.SoldAt.ToString("O", CultureInfo.InvariantCulture)),
            Escape(line.Product.Barcode),
            Escape(line.Product.Name),
            line.Quantity.ToString(CultureInfo.InvariantCulture),
            line.Product.Price.ToString("0.00", CultureInfo.InvariantCulture),
            (line.Product.Price * line.Quantity).ToString("0.00", CultureInfo.InvariantCulture),
            line.SaleTotal.ToString("0.00", CultureInfo.InvariantCulture),
            "Cash",
            line.CashReceived.ToString("0.00", CultureInfo.InvariantCulture),
            line.Change.ToString("0.00", CultureInfo.InvariantCulture))));

        ReplaceFilesTogether(
            _productsPath,
            SerializeProducts(updatedProducts),
            salesPath,
            string.Join(Environment.NewLine, salesRows) + Environment.NewLine);
    }

    private string GetSalesPath(DateTime soldAt) =>
        Path.Combine(_salesDirectory,
            "sales_" + soldAt.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + ".csv");

    public Product AdjustStock(string barcode, int adjustment)
    {
        if (adjustment == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(adjustment), "Stock adjustment cannot be zero.");
        }

        List<Product> products = LoadProducts();
        Product? product = products.FirstOrDefault(item =>
            item.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
        if (product is null)
        {
            throw new InvalidDataException($"No product is registered with barcode {barcode}.");
        }

        long resultingStock = (long)product.Stock + adjustment;
        if (resultingStock < 0)
        {
            throw new InvalidOperationException("Stock cannot be reduced below zero.");
        }

        if (resultingStock > int.MaxValue)
        {
            throw new InvalidOperationException("Stock cannot exceed the supported inventory limit.");
        }

        int updatedStock = (int)resultingStock;
        List<Product> updatedProducts = products.Select(item => item.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase)
            ? new Product
            {
                Barcode = item.Barcode,
                Name = item.Name,
                CategoryId = item.CategoryId,
                Price = item.Price,
                Stock = updatedStock,
                Icon = item.Icon,
                ImageFile = item.ImageFile
            }
            : item).ToList();

        string contents = SerializeProducts(updatedProducts);
        string temporaryPath = _productsPath + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporaryPath, contents, new UTF8Encoding(false));
            File.Replace(temporaryPath, _productsPath, null);
        }
        finally
        {
            DeleteIfPresent(temporaryPath);
        }

        return updatedProducts.First(item => item.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
    }

    private static string SerializeProducts(IEnumerable<Product> products)
    {
        List<string> rows = [ProductsHeader];
        rows.AddRange(products.Select(product => string.Join(",",
            Escape(product.Barcode),
            Escape(product.Name),
            Escape(product.CategoryId),
            product.Price.ToString("0.00", CultureInfo.InvariantCulture),
            product.Stock.ToString(CultureInfo.InvariantCulture),
            Escape(product.Icon),
            Escape(product.ImageFile))));
        return string.Join(Environment.NewLine, rows) + Environment.NewLine;
    }

    private static void ReplaceFilesTogether(string firstPath, string firstContents, string secondPath, string secondContents)
    {
        string token = Guid.NewGuid().ToString("N");
        string firstTemp = firstPath + "." + token + ".tmp";
        string secondTemp = secondPath + "." + token + ".tmp";
        string firstBackup = firstPath + "." + token + ".bak";
        string secondBackup = secondPath + "." + token + ".bak";

        try
        {
            File.WriteAllText(firstTemp, firstContents, new UTF8Encoding(false));
            File.WriteAllText(secondTemp, secondContents, new UTF8Encoding(false));
            File.Copy(firstPath, firstBackup);
            bool secondFileExists = File.Exists(secondPath);
            if (secondFileExists)
            {
                File.Copy(secondPath, secondBackup);
            }

            try
            {
                File.Replace(firstTemp, firstPath, null);
                if (secondFileExists)
                {
                    File.Replace(secondTemp, secondPath, null);
                }
                else
                {
                    File.Move(secondTemp, secondPath);
                }
            }
            catch
            {
                File.Copy(firstBackup, firstPath, true);
                if (secondFileExists)
                {
                    File.Copy(secondBackup, secondPath, true);
                }
                else
                {
                    DeleteIfPresent(secondPath);
                }

                throw;
            }
        }
        finally
        {
            DeleteIfPresent(firstTemp);
            DeleteIfPresent(secondTemp);
            DeleteIfPresent(firstBackup);
            DeleteIfPresent(secondBackup);
        }
    }

    private static void EnsureFile(string path, string header)
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, header + Environment.NewLine, new UTF8Encoding(false));
        }
    }

    private static List<Dictionary<string, string>> ReadRows(string path)
    {
        string[] lines = File.ReadAllLines(path, Encoding.UTF8);
        if (lines.Length == 0)
        {
            return [];
        }

        List<string> headers = ParseLine(lines[0]);
        List<Dictionary<string, string>> rows = [];
        foreach (string line in lines.Skip(1).Where(line => !string.IsNullOrWhiteSpace(line)))
        {
            List<string> values = ParseLine(line);
            Dictionary<string, string> row = new(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < headers.Count; i++)
            {
                row[headers[i]] = i < values.Count ? values[i] : string.Empty;
            }

            rows.Add(row);
        }

        return rows;
    }

    private static List<string> ParseLine(string line)
    {
        List<string> values = [];
        StringBuilder value = new();
        bool quoted = false;

        for (int i = 0; i < line.Length; i++)
        {
            char current = line[i];
            if (current == '"')
            {
                if (quoted && i + 1 < line.Length && line[i + 1] == '"')
                {
                    value.Append('"');
                    i++;
                }
                else
                {
                    quoted = !quoted;
                }
            }
            else if (current == ',' && !quoted)
            {
                values.Add(value.ToString());
                value.Clear();
            }
            else
            {
                value.Append(current);
            }
        }

        values.Add(value.ToString());
        return values;
    }

    private static string Get(IReadOnlyDictionary<string, string> row, string key) =>
        row.TryGetValue(key, out string? value) ? value : string.Empty;

    private static string Escape(string value) =>
        value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r')
            ? "\"" + value.Replace("\"", "\"\"") + "\""
            : value;

    private static void DeleteIfPresent(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
