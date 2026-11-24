using System.Globalization;

namespace POS;

internal partial class InventoryForm : Form
{
    private readonly CsvDataStore _store;
    private readonly IReadOnlyDictionary<string, string> _categoryNames;
    private readonly IReadOnlyDictionary<string, int> _reservedStock;
    private List<Product> _products;

    internal InventoryForm(
        CsvDataStore store,
        IReadOnlyCollection<ProductCategory> categories,
        IReadOnlyDictionary<string, int> reservedStock)
    {
        InitializeComponent();
        _store = store;
        _categoryNames = categories.ToDictionary(category => category.Id, category => category.Name,
            StringComparer.OrdinalIgnoreCase);
        _reservedStock = reservedStock;
        _products = _store.LoadProducts();

        _searchBox.TextChanged += (_, _) => RefreshInventory();
        _inventoryGrid.SelectionChanged += (_, _) => UpdateSelectionStatus();
        _addStockButton.Click += (_, _) => AdjustSelectedStock(adding: true);
        _removeStockButton.Click += (_, _) => AdjustSelectedStock(adding: false);

        RefreshInventory();
    }

    private void RefreshInventory()
    {
        string? selectedBarcode = GetSelectedBarcode();
        string searchText = _searchBox.Text.Trim();
        _inventoryGrid.SuspendLayout();
        try
        {
            _inventoryGrid.Rows.Clear();
            foreach (Product product in _products.Where(product =>
                         string.IsNullOrEmpty(searchText) ||
                         product.Name.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) ||
                         product.Barcode.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                         (_categoryNames.TryGetValue(product.CategoryId, out string? categoryName) &&
                          categoryName.Contains(searchText, StringComparison.CurrentCultureIgnoreCase))))
            {
                string category = _categoryNames.TryGetValue(product.CategoryId, out string? name)
                    ? name
                    : product.CategoryId;
                int rowIndex = _inventoryGrid.Rows.Add(
                    product.Barcode,
                    product.Name,
                    category,
                    product.Price.ToString("C2", CultureInfo.CurrentCulture),
                    product.Stock);
                DataGridViewRow row = _inventoryGrid.Rows[rowIndex];
                if (product.Barcode.Equals(selectedBarcode, StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;
                    _inventoryGrid.CurrentCell = row.Cells[0];
                }
            }

            if (_inventoryGrid.CurrentRow is null && _inventoryGrid.Rows.Count > 0)
            {
                _inventoryGrid.Rows[0].Selected = true;
                _inventoryGrid.CurrentCell = _inventoryGrid.Rows[0].Cells[0];
            }
        }
        finally
        {
            _inventoryGrid.ResumeLayout();
        }

        UpdateSelectionStatus();
    }

    private void AdjustSelectedStock(bool adding)
    {
        Product? product = GetSelectedProduct();
        if (product is null)
        {
            MessageBox.Show("Select a product first.", "No product selected",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        int quantity = decimal.ToInt32(_quantityInput.Value);
        int adjustment = adding ? quantity : -quantity;
        int reserved = _reservedStock.TryGetValue(product.Barcode, out int reservedQuantity)
            ? reservedQuantity
            : 0;
        if (!adding && product.Stock - quantity < reserved)
        {
            int removable = Math.Max(0, product.Stock - reserved);
            MessageBox.Show(
                reserved > 0
                    ? $"There are {reserved} {product.Name} reserved in the current cart. You can remove at most {removable}."
                    : $"Only {product.Stock} {product.Name} available to remove.",
                "Insufficient stock",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        try
        {
            _store.AdjustStock(product.Barcode, adjustment);
        }
        catch (IOException exception)
        {
            ShowAdjustmentError(exception);
            return;
        }
        catch (UnauthorizedAccessException exception)
        {
            ShowAdjustmentError(exception);
            return;
        }
        catch (InvalidOperationException exception)
        {
            ShowAdjustmentError(exception);
            return;
        }
        catch (InvalidDataException exception)
        {
            ShowAdjustmentError(exception);
            return;
        }

        _products = _store.LoadProducts();
        RefreshInventory();
        string direction = adding ? "to" : "from";
        _statusLabel.Text = $"{(adding ? "Added" : "Removed")} {quantity} unit(s) {direction} {product.Name}.";
    }

    private Product? GetSelectedProduct()
    {
        string? barcode = GetSelectedBarcode();
        return barcode is null
            ? null
            : _products.FirstOrDefault(product => product.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
    }

    private string? GetSelectedBarcode() =>
        _inventoryGrid.CurrentRow?.Cells["barcodeColumn"].Value as string;

    private void UpdateSelectionStatus()
    {
        Product? product = GetSelectedProduct();
        if (product is null)
        {
            _statusLabel.Text = "Select a product to adjust stock.";
            return;
        }

        int reserved = _reservedStock.TryGetValue(product.Barcode, out int quantity) ? quantity : 0;
        _statusLabel.Text = reserved > 0
            ? $"{reserved} unit(s) reserved in the current cart."
            : $"Current stock: {product.Stock}";
    }

    private static void ShowAdjustmentError(Exception exception)
    {
        MessageBox.Show($"Stock was not changed.\n\n{exception.Message}",
            "Unable to update inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
