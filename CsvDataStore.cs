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
    public required string ImageFileName { get; init; }
    public required int LowStockThreshold { get; set; }
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
    public required string Cashier { get; init; }
}

internal sealed class ProductSalesSummary
{
    public required string Barcode { get; init; }
    public required string ProductName { get; init; }
    public required string Cashier { get; init; }
    public required long QuantitySold { get; init; }
    public required decimal Revenue { get; init; }
}

internal sealed class DailySalesSummary
{
    public required DateTime Date { get; init; }
    public required int TransactionCount { get; init; }
    public required long ItemsSold { get; init; }
    public required decimal SalesTotal { get; init; }
    public required decimal CashReceived { get; init; }
    public required decimal Change { get; init; }
    public required List<ProductSalesSummary> Products { get; init; }
}

internal sealed class CsvDataStore
{
    internal const int DefaultLowStockThreshold = 5;
    private const string CategoriesHeader = "id,name,icon";
    private const string ProductsHeader = "barcode,name,category_id,price,image_filename,low_stock_threshold";
    private const string StocksHeader = "barcode,stock";
    private const string PrintCountsHeader = "date,print_count";
    private const string SalesHeader = "sale_id,sold_at,barcode,product_name,quantity,unit_price,line_total,sale_total,payment_method,cash_received,change,cashier";

    private readonly string _categoriesPath;
    private readonly string _productsPath;
    private readonly string _stocksPath;
    private readonly string _printCountsPath;
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
        _stocksPath = Path.Combine(dataDirectory, "stocks.csv");
        _salesDirectory = Path.Combine(dataDirectory, "Sales");
        _printCountsPath = Path.Combine(_salesDirectory, "sales_summary_print_counts.csv");

        EnsureFile(_categoriesPath, CategoriesHeader);
        EnsureFile(_productsPath, ProductsHeader);
        EnsureFile(_stocksPath, StocksHeader);
        Directory.CreateDirectory(_salesDirectory);
        EnsureFile(_printCountsPath, PrintCountsHeader);
        EnsureFile(GetSalesPath(DateTime.Now), SalesHeader);
    }

    public List<ProductCategory> LoadCategories() =>
        ReadRows(_categoriesPath).Select(row => new ProductCategory
        {
            Id = Get(row, "id"),
            Name = Get(row, "name"),
            Icon = Get(row, "icon")
        }).ToList();

    public void AddCategory(ProductCategory category)
    {
        ProductCategory normalizedCategory = NormalizeCategory(category);
        List<ProductCategory> categories = LoadCategories();
        if (categories.Any(existing =>
                existing.Id.Equals(normalizedCategory.Id, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidDataException($"A category is already registered with ID {normalizedCategory.Id}.");
        }

        categories.Add(normalizedCategory);
        SaveCategories(categories);
    }

    public void UpdateCategory(string id, ProductCategory updatedCategory)
    {
        ProductCategory normalizedCategory = NormalizeCategory(updatedCategory);
        List<ProductCategory> categories = LoadCategories();
        int categoryIndex = categories.FindIndex(category =>
            category.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        if (categoryIndex < 0)
        {
            throw new InvalidDataException($"No category is registered with ID {id}.");
        }

        if (categories.Where((_, index) => index != categoryIndex).Any(category =>
                category.Id.Equals(normalizedCategory.Id, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidDataException($"A category is already registered with ID {normalizedCategory.Id}.");
        }

        ProductCategory existingCategory = categories[categoryIndex];
        categories[categoryIndex] = normalizedCategory;
        if (existingCategory.Id.Equals(normalizedCategory.Id, StringComparison.OrdinalIgnoreCase))
        {
            SaveCategories(categories);
            return;
        }

        List<Product> products = LoadProducts().Select(product =>
            product.CategoryId.Equals(existingCategory.Id, StringComparison.OrdinalIgnoreCase)
                ? new Product
                {
                    Barcode = product.Barcode,
                    Name = product.Name,
                    CategoryId = normalizedCategory.Id,
                    Price = product.Price,
                    Stock = product.Stock,
                    ImageFileName = product.ImageFileName,
                    LowStockThreshold = product.LowStockThreshold
                }
                : product).ToList();

        ReplaceFilesTogether(
            _categoriesPath,
            SerializeCategories(categories),
            _productsPath,
            SerializeProducts(products));
    }

    public void DeleteCategory(string id)
    {
        List<ProductCategory> categories = LoadCategories();
        int categoryIndex = categories.FindIndex(category =>
            category.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        if (categoryIndex < 0)
        {
            throw new InvalidDataException($"No category is registered with ID {id}.");
        }

        ProductCategory category = categories[categoryIndex];
        if (LoadProducts().Any(product =>
                product.CategoryId.Equals(category.Id, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                $"Category {category.Name} is assigned to products. Reassign those products before deleting it.");
        }

        categories.RemoveAt(categoryIndex);
        SaveCategories(categories);
    }

    public List<Product> LoadProducts()
    {
        Dictionary<string, Dictionary<string, string>> stocksByBarcode = new(StringComparer.OrdinalIgnoreCase);
        foreach (Dictionary<string, string> row in ReadRows(_stocksPath))
        {
            string barcode = Get(row, "barcode");
            if (string.IsNullOrWhiteSpace(barcode))
            {
                throw new InvalidDataException("Every stock record must have a barcode in stocks.csv.");
            }

            if (!stocksByBarcode.TryAdd(barcode, row))
            {
                throw new InvalidDataException("Each product barcode in stocks.csv must be unique.");
            }
        }

        List<Product> products = ReadRows(_productsPath).Select(row =>
        {
            string barcode = Get(row, "barcode");
            if (!stocksByBarcode.TryGetValue(barcode, out Dictionary<string, string>? stock))
            {
                throw new InvalidDataException($"No stock record is registered for product barcode {barcode} in stocks.csv.");
            }

            return new Product
            {
                Barcode = barcode,
                Name = Get(row, "name"),
                CategoryId = Get(row, "category_id"),
                Price = decimal.Parse(Get(row, "price"), CultureInfo.InvariantCulture),
                Stock = int.Parse(Get(stock, "stock"), CultureInfo.InvariantCulture),
                ImageFileName = Get(row, "image_filename"),
                LowStockThreshold = string.IsNullOrWhiteSpace(Get(row, "low_stock_threshold"))
                    ? DefaultLowStockThreshold
                    : int.Parse(Get(row, "low_stock_threshold"), CultureInfo.InvariantCulture)
            };
        }).ToList();

        if (products.Any(product => string.IsNullOrWhiteSpace(product.Barcode)))
        {
            throw new InvalidDataException("Every product must have a barcode in products.csv.");
        }

        if (products.Select(product => product.Barcode).Distinct(StringComparer.OrdinalIgnoreCase).Count() != products.Count)
        {
            throw new InvalidDataException("Each product barcode in products.csv must be unique.");
        }

        if (stocksByBarcode.Keys.Except(products.Select(product => product.Barcode), StringComparer.OrdinalIgnoreCase).Any())
        {
            throw new InvalidDataException("Every barcode in stocks.csv must match a product barcode in products.csv.");
        }

        if (products.Any(product => product.LowStockThreshold < 0))
        {
            throw new InvalidDataException("Product low stock thresholds in products.csv cannot be negative.");
        }

        return products;
    }

    public void AddProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Barcode))
        {
            throw new InvalidDataException("A product barcode is required.");
        }

        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new InvalidDataException("A product name is required.");
        }

        if (product.Price < 0 || product.Stock < 0 || product.LowStockThreshold < 0)
        {
            throw new InvalidDataException("Product price, stock, and low stock threshold cannot be negative.");
        }

        List<Product> products = LoadProducts();
        if (products.Any(existing =>
                existing.Barcode.Equals(product.Barcode, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidDataException($"A product is already registered with barcode {product.Barcode}.");
        }

        if (!LoadCategories().Any(category =>
                category.Id.Equals(product.CategoryId, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidDataException($"No category is registered with ID {product.CategoryId}.");
        }

        products.Add(product);
        ReplaceFilesTogether(
            _productsPath,
            SerializeProducts(products),
            _stocksPath,
            SerializeStocks(products));
    }

    public Product UpdateProduct(string barcode, Product updatedProduct)
    {
        string updatedBarcode = updatedProduct.Barcode.Trim();
        string updatedName = updatedProduct.Name.Trim().ToUpperInvariant();
        if (updatedBarcode.Length == 0 || updatedName.Length == 0)
        {
            throw new InvalidDataException("A product barcode and name are required.");
        }

        if (updatedProduct.Price < 0)
        {
            throw new InvalidDataException("Product price cannot be negative.");
        }

        List<Product> products = LoadProducts();
        int productIndex = products.FindIndex(product =>
            product.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
        if (productIndex < 0)
        {
            throw new InvalidDataException($"No product is registered with barcode {barcode}.");
        }

        if (products.Where((_, index) => index != productIndex).Any(product =>
                product.Barcode.Equals(updatedBarcode, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidDataException($"A product is already registered with barcode {updatedBarcode}.");
        }

        if (!LoadCategories().Any(category =>
                category.Id.Equals(updatedProduct.CategoryId, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidDataException($"No category is registered with ID {updatedProduct.CategoryId}.");
        }

        Product existingProduct = products[productIndex];
        Product savedProduct = new()
        {
            Barcode = updatedBarcode,
            Name = updatedName,
            CategoryId = updatedProduct.CategoryId,
            Price = updatedProduct.Price,
            Stock = existingProduct.Stock,
            ImageFileName = updatedProduct.ImageFileName.Trim(),
            LowStockThreshold = existingProduct.LowStockThreshold
        };
        products[productIndex] = savedProduct;

        ReplaceFilesTogether(
            _productsPath,
            SerializeProducts(products),
            _stocksPath,
            SerializeStocks(products));
        return savedProduct;
    }

    public void DeleteProduct(string barcode)
    {
        List<Product> products = LoadProducts();
        int productIndex = products.FindIndex(product =>
            product.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
        if (productIndex < 0)
        {
            throw new InvalidDataException($"No product is registered with barcode {barcode}.");
        }

        products.RemoveAt(productIndex);
        ReplaceFilesTogether(
            _productsPath,
            SerializeProducts(products),
            _stocksPath,
            SerializeStocks(products));
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
        else if (!ParseLine(salesRows[0]).Contains("cashier", StringComparer.OrdinalIgnoreCase))
        {
            salesRows[0] += ",cashier";
            for (int i = 1; i < salesRows.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(salesRows[i]))
                {
                    salesRows[i] += ",";
                }
            }
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
            line.Change.ToString("0.00", CultureInfo.InvariantCulture),
            Escape(line.Cashier))));

        ReplaceFilesTogether(
            _stocksPath,
            SerializeStocks(updatedProducts),
            salesPath,
            string.Join(Environment.NewLine, salesRows) + Environment.NewLine);
    }

    private string GetSalesPath(DateTime soldAt) =>
        Path.Combine(_salesDirectory,
            "sales_" + soldAt.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + ".csv");

    public Product SetLowStockThreshold(string barcode, int threshold)
    {
        if (threshold < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(threshold), "Low stock threshold cannot be negative.");
        }

        List<Product> products = LoadProducts();
        Product? product = products.FirstOrDefault(item =>
            item.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
        if (product is null)
        {
            throw new InvalidDataException($"No product is registered with barcode {barcode}.");
        }

        product.LowStockThreshold = threshold;
        SaveProducts(products);
        return product;
    }

    public DailySalesSummary LoadDailySalesSummary(DateTime date)
    {
        string salesPath = GetSalesPath(date);
        List<Dictionary<string, string>> records = File.Exists(salesPath) ? ReadRows(salesPath) : [];
        if (records.Any(row => string.IsNullOrWhiteSpace(Get(row, "sale_id"))))
        {
            throw new InvalidDataException($"Every sale record in '{Path.GetFileName(salesPath)}' must have a sale ID.");
        }

        IGrouping<string, Dictionary<string, string>>[] transactions = records
            .GroupBy(row => Get(row, "sale_id"), StringComparer.Ordinal)
            .ToArray();
        List<ProductSalesSummary> products = records
            .GroupBy(row => Get(row, "barcode"), StringComparer.OrdinalIgnoreCase)
            .SelectMany(barcodeGroup => barcodeGroup
                .GroupBy(row => Get(row, "cashier"), StringComparer.OrdinalIgnoreCase)
                .Select(group => new ProductSalesSummary
                {
                    Barcode = barcodeGroup.Key,
                    ProductName = Get(group.First(), "product_name"),
                    Cashier = group.Key,
                    QuantitySold = group.Sum(row => long.Parse(Get(row, "quantity"), CultureInfo.InvariantCulture)),
                    Revenue = group.Sum(row => decimal.Parse(Get(row, "line_total"), CultureInfo.InvariantCulture))
                }))
            .OrderByDescending(product => product.Revenue)
            .ThenBy(product => product.ProductName, StringComparer.CurrentCultureIgnoreCase)
            .ToList();

        return new DailySalesSummary
        {
            Date = date.Date,
            TransactionCount = transactions.Length,
            ItemsSold = records.Sum(row => long.Parse(Get(row, "quantity"), CultureInfo.InvariantCulture)),
            SalesTotal = transactions.Sum(transaction =>
                decimal.Parse(Get(transaction.First(), "sale_total"), CultureInfo.InvariantCulture)),
            CashReceived = transactions.Sum(transaction =>
                decimal.Parse(Get(transaction.First(), "cash_received"), CultureInfo.InvariantCulture)),
            Change = transactions.Sum(transaction =>
                decimal.Parse(Get(transaction.First(), "change"), CultureInfo.InvariantCulture)),
            Products = products
        };
    }

    public int LoadSalesSummaryPrintCount(DateTime date)
    {
        string dateKey = date.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        Dictionary<string, int> printCounts = ReadSalesSummaryPrintCounts();
        return printCounts.TryGetValue(dateKey, out int count) ? count : 0;
    }

    public int IncrementSalesSummaryPrintCount(DateTime date)
    {
        string dateKey = date.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        Dictionary<string, int> printCounts = ReadSalesSummaryPrintCounts();
        int currentCount = printCounts.TryGetValue(dateKey, out int count) ? count : 0;
        if (currentCount == int.MaxValue)
        {
            throw new InvalidOperationException("The print count has reached its supported limit.");
        }

        int updatedCount = currentCount + 1;
        printCounts[dateKey] = updatedCount;
        SaveSalesSummaryPrintCounts(printCounts);
        return updatedCount;
    }

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
                ImageFileName = item.ImageFileName,
                LowStockThreshold = item.LowStockThreshold
            }
            : item).ToList();

        SaveStocks(updatedProducts);
        return updatedProducts.First(item => item.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
    }

    private void SaveStocks(IReadOnlyCollection<Product> products)
    {
        string contents = SerializeStocks(products);
        string temporaryPath = _stocksPath + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporaryPath, contents, new UTF8Encoding(false));
            File.Replace(temporaryPath, _stocksPath, null);
        }
        finally
        {
            DeleteIfPresent(temporaryPath);
        }
    }

    private void SaveProducts(IReadOnlyCollection<Product> products)
    {
        string contents = SerializeProducts(products);
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
    }

    private void SaveCategories(IReadOnlyCollection<ProductCategory> categories)
    {
        string contents = SerializeCategories(categories);
        string temporaryPath = _categoriesPath + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporaryPath, contents, new UTF8Encoding(false));
            File.Replace(temporaryPath, _categoriesPath, null);
        }
        finally
        {
            DeleteIfPresent(temporaryPath);
        }
    }

    private Dictionary<string, int> ReadSalesSummaryPrintCounts()
    {
        Dictionary<string, int> printCounts = new(StringComparer.Ordinal);
        foreach (Dictionary<string, string> row in ReadRows(_printCountsPath))
        {
            string date = Get(row, "date");
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime parsedDate))
            {
                throw new InvalidDataException("Every print count record must have a valid date in sales_summary_print_counts.csv.");
            }

            int count = int.Parse(Get(row, "print_count"), CultureInfo.InvariantCulture);
            if (count < 0)
            {
                throw new InvalidDataException("Sales summary print counts cannot be negative.");
            }

            if (!printCounts.TryAdd(parsedDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), count))
            {
                throw new InvalidDataException("Each date in sales_summary_print_counts.csv must be unique.");
            }
        }

        return printCounts;
    }

    private void SaveSalesSummaryPrintCounts(IReadOnlyDictionary<string, int> printCounts)
    {
        List<string> rows = [PrintCountsHeader];
        rows.AddRange(printCounts.OrderBy(entry => entry.Key, StringComparer.Ordinal)
            .Select(entry => string.Join(",", entry.Key, entry.Value.ToString(CultureInfo.InvariantCulture))));
        string contents = string.Join(Environment.NewLine, rows) + Environment.NewLine;
        string temporaryPath = _printCountsPath + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporaryPath, contents, new UTF8Encoding(false));
            File.Replace(temporaryPath, _printCountsPath, null);
        }
        finally
        {
            DeleteIfPresent(temporaryPath);
        }
    }

    private static string SerializeStocks(IEnumerable<Product> products)
    {
        List<string> rows = [StocksHeader];
        rows.AddRange(products.Select(product => string.Join(",",
            Escape(product.Barcode),
            product.Stock.ToString(CultureInfo.InvariantCulture))));
        return string.Join(Environment.NewLine, rows) + Environment.NewLine;
    }

    private static string SerializeProducts(IEnumerable<Product> products)
    {
        List<string> rows = [ProductsHeader];
        rows.AddRange(products.Select(product => string.Join(",",
            Escape(product.Barcode),
            Escape(product.Name.Trim().ToUpperInvariant()),
            Escape(product.CategoryId),
            product.Price.ToString("0.00", CultureInfo.InvariantCulture),
            Escape(product.ImageFileName),
            product.LowStockThreshold.ToString(CultureInfo.InvariantCulture))));
        return string.Join(Environment.NewLine, rows) + Environment.NewLine;
    }

    private static string SerializeCategories(IEnumerable<ProductCategory> categories)
    {
        List<string> rows = [CategoriesHeader];
        rows.AddRange(categories.Select(category => string.Join(",",
            Escape(category.Id),
            Escape(category.Name),
            Escape(category.Icon))));
        return string.Join(Environment.NewLine, rows) + Environment.NewLine;
    }

    private static ProductCategory NormalizeCategory(ProductCategory category)
    {
        string id = category.Id.Trim();
        string name = category.Name.Trim();
        string icon = category.Icon.Trim();
        if (id.Length == 0 || name.Length == 0 || icon.Length == 0)
        {
            throw new InvalidDataException("Category ID, name, and icon are required.");
        }

        return new ProductCategory
        {
            Id = id,
            Name = name,
            Icon = icon
        };
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
