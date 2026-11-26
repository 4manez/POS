using System.Drawing;
using System.Globalization;

namespace POS;

internal partial class InventoryForm : Form
{
    private readonly CsvDataStore _store;
    private readonly ApplicationConfig _configuration;
    private IReadOnlyDictionary<string, string> _categoryNames;
    private readonly IReadOnlyDictionary<string, int> _reservedStock;
    private List<Product> _products;
    private bool _adminAuthenticated;

    internal InventoryForm(
        CsvDataStore store,
        IReadOnlyCollection<ProductCategory> categories,
        IReadOnlyDictionary<string, int> reservedStock,
        ApplicationConfig configuration)
    {
        InitializeComponent();
        _store = store;
        _configuration = configuration;
        _categoryNames = categories.ToDictionary(category => category.Id, category => category.Name,
            StringComparer.OrdinalIgnoreCase);
        _reservedStock = reservedStock;
        _products = _store.LoadProducts();

        _searchBox.TextChanged += (_, _) => RefreshInventory();
        _inventoryGrid.SelectionChanged += (_, _) => UpdateSelectionStatus();
        _addStockButton.Click += (_, _) => AdjustSelectedStock(adding: true);
        _removeStockButton.Click += (_, _) => AdjustSelectedStock(adding: false);
        _addProductButton.Click += (_, _) => AddNewProduct();
        _editProductButton.Click += (_, _) => EditSelectedProduct();
        _deleteProductButton.Click += (_, _) => DeleteSelectedProduct();
        _manageCategoriesButton.Click += (_, _) => ManageCategories();
        _saveThresholdButton.Click += (_, _) => SaveSelectedThreshold();

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
                          categoryName.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)))
                     .OrderBy(product => product.Stock <= product.LowStockThreshold ? 0 : 1)
                     .ThenBy(product => product.Stock)
                     .ThenBy(product => product.Name, StringComparer.CurrentCultureIgnoreCase))
            {
                string category = _categoryNames.TryGetValue(product.CategoryId, out string? name)
                    ? name
                    : product.CategoryId;
                int rowIndex = _inventoryGrid.Rows.Add(
                    product.Barcode,
                    product.Name,
                    category,
                    product.Price.ToString("C2", CultureInfo.CurrentCulture),
                    product.Stock,
                    product.LowStockThreshold);
                DataGridViewRow row = _inventoryGrid.Rows[rowIndex];
                if (product.Stock <= product.LowStockThreshold)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(128, 24, 24);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 170, 170);
                    row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(80, 16, 16);
                }

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

        if (!AuthenticateAdmin())
        {
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

    private bool AuthenticateAdmin()
    {
        if (_adminAuthenticated)
        {
            return true;
        }

        using Form dialog = new()
        {
            Text = "Administrator Authentication",
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            ClientSize = new Size(360, 132),
            MaximizeBox = false,
            MinimizeBox = false,
            ShowInTaskbar = false
        };

        Label prompt = new()
        {
            AutoSize = true,
            Location = new Point(18, 18),
            Text = "Enter the administrator password:"
        };
        TextBox passwordInput = new()
        {
            Location = new Point(18, 46),
            Size = new Size(324, 25),
            UseSystemPasswordChar = true
        };
        Button cancelButton = new()
        {
            DialogResult = DialogResult.Cancel,
            Location = new Point(186, 88),
            Size = new Size(75, 28),
            Text = "Cancel"
        };
        Button confirmButton = new()
        {
            DialogResult = DialogResult.OK,
            Location = new Point(267, 88),
            Size = new Size(75, 28),
            Text = "OK"
        };

        dialog.Controls.AddRange([prompt, passwordInput, cancelButton, confirmButton]);
        dialog.AcceptButton = confirmButton;
        dialog.CancelButton = cancelButton;
        dialog.Shown += (_, _) => passwordInput.Focus();

        if (dialog.ShowDialog(this) != DialogResult.OK)
        {
            return false;
        }

        if (!string.Equals(passwordInput.Text, _configuration.AdminPassword, StringComparison.Ordinal))
        {
            MessageBox.Show("The administrator password is incorrect.",
                "Authentication failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        _adminAuthenticated = true;
        EnableAdminAppearance();
        return true;
    }

    private void EnableAdminAppearance()
    {
        Color darkGreen = Color.FromArgb(18, 78, 42);
        BackColor = darkGreen;
        _rootLayout.BackColor = darkGreen;
        _titleLabel.ForeColor = Color.White;
        _subtitleLabel.ForeColor = Color.FromArgb(220, 238, 226);
        _lowStockLabel.ForeColor = Color.White;
        _quantityLabel.ForeColor = Color.White;
        _statusLabel.ForeColor = Color.FromArgb(220, 238, 226);
    }

    private void SaveSelectedThreshold()
    {
        Product? product = GetSelectedProduct();
        if (product is null)
        {
            MessageBox.Show("Select a product first.", "No product selected",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        int threshold = decimal.ToInt32(_lowStockThreshold.Value);
        try
        {
            _store.SetLowStockThreshold(product.Barcode, threshold);
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
        catch (InvalidDataException exception)
        {
            ShowAdjustmentError(exception);
            return;
        }

        _products = _store.LoadProducts();
        RefreshInventory();
        _statusLabel.Text = $"Low stock threshold for {product.Name} set to {threshold}.";
    }

    private void AddNewProduct()
    {
        ShowProductEditor(null);
    }

    private void EditSelectedProduct()
    {
        Product? product = GetSelectedProduct();
        if (product is null)
        {
            MessageBox.Show("Select a product first.", "No product selected",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        ShowProductEditor(product);
    }

    private void DeleteSelectedProduct()
    {
        Product? product = GetSelectedProduct();
        if (product is null)
        {
            MessageBox.Show("Select a product first.", "No product selected",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        int reserved = _reservedStock.TryGetValue(product.Barcode, out int reservedQuantity)
            ? reservedQuantity
            : 0;
        if (reserved > 0)
        {
            MessageBox.Show(
                $"Cannot delete {product.Name} because {reserved} unit(s) are reserved in the current cart.",
                "Item in current cart",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        DialogResult confirmation = MessageBox.Show(
            $"Delete {product.Name} ({product.Barcode}) from inventory?\n\n" +
            $"This will remove its remaining stock ({product.Stock}) and low-stock threshold. " +
            "Existing sales history will be kept.",
            "Confirm item deletion",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);
        if (confirmation != DialogResult.Yes || !AuthenticateAdmin())
        {
            return;
        }

        try
        {
            _store.DeleteProduct(product.Barcode);
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
        catch (InvalidDataException exception)
        {
            ShowAdjustmentError(exception);
            return;
        }

        _products = _store.LoadProducts();
        RefreshInventory();
        _statusLabel.Text = $"Deleted {product.Name} from inventory.";
    }

    private void ManageCategories()
    {
        if (!AuthenticateAdmin())
        {
            return;
        }

        using CategoryManagementForm categoryForm = new(_store);
        categoryForm.ShowDialog(this);

        _categoryNames = _store.LoadCategories().ToDictionary(category => category.Id, category => category.Name,
            StringComparer.OrdinalIgnoreCase);
        _products = _store.LoadProducts();
        RefreshInventory();
    }

    private void ShowProductEditor(Product? existingProduct)
    {
        bool adding = existingProduct is null;
        List<ProductCategory> categories = _store.LoadCategories();
        if (categories.Count == 0)
        {
            MessageBox.Show("Add a category before creating or editing a product.",
                "No categories available", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!AuthenticateAdmin())
        {
            return;
        }

        using Form dialog = new()
        {
            Text = adding ? "Add New Item" : "Edit Item",
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            ClientSize = adding ? new Size(480, 350) : new Size(480, 275),
            MaximizeBox = false,
            MinimizeBox = false,
            ShowInTaskbar = false
        };

        TableLayoutPanel layout = new()
        {
            ColumnCount = 2,
            Dock = DockStyle.Fill,
            Padding = new Padding(16),
            RowCount = adding ? 8 : 6
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        int fieldCount = adding ? 6 : 4;
        for (int i = 0; i < fieldCount; i++)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        }

        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        TextBox barcodeInput = new() { Dock = DockStyle.Fill };
        TextBox nameInput = new()
        {
            CharacterCasing = CharacterCasing.Upper,
            Dock = DockStyle.Fill
        };
        ComboBox categoryInput = new()
        {
            DataSource = categories,
            DisplayMember = nameof(ProductCategory.Name),
            Dock = DockStyle.Fill,
            DropDownStyle = ComboBoxStyle.DropDownList,
            ValueMember = nameof(ProductCategory.Id)
        };
        if (existingProduct is not null)
        {
            categoryInput.SelectedValue = existingProduct.CategoryId;
        }

        NumericUpDown priceInput = new()
        {
            DecimalPlaces = 2,
            Dock = DockStyle.Left,
            Increment = 0.01m,
            Maximum = decimal.MaxValue,
            Minimum = 0,
            ThousandsSeparator = true,
            Width = 180
        };
        barcodeInput.Text = existingProduct?.Barcode ?? string.Empty;
        nameInput.Text = existingProduct?.Name ?? string.Empty;
        priceInput.Value = existingProduct?.Price ?? 0;

        TextBox stockInput = new()
        {
            Dock = DockStyle.Fill,
            PlaceholderText = "Optional (defaults to 0)"
        };
        TextBox thresholdInput = new()
        {
            Dock = DockStyle.Fill,
            PlaceholderText = "Optional (defaults to 5)"
        };
        Product? addedProduct = null;
        Label errorLabel = new()
        {
            AutoEllipsis = true,
            Dock = DockStyle.Fill,
            ForeColor = Color.FromArgb(160, 32, 32),
            TextAlign = ContentAlignment.MiddleLeft
        };
        Button cancelButton = new()
        {
            DialogResult = DialogResult.Cancel,
            Text = "Cancel",
            Width = 90
        };
        Button saveButton = new()
        {
            Text = adding ? "Add Item" : "Save Changes",
            Width = 115
        };
        FlowLayoutPanel buttonPanel = new()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false
        };
        buttonPanel.Controls.Add(saveButton);
        buttonPanel.Controls.Add(cancelButton);

        AddEditorRow(layout, 0, "Barcode:", barcodeInput);
        AddEditorRow(layout, 1, "Name:", nameInput);
        AddEditorRow(layout, 2, "Category:", categoryInput);
        AddEditorRow(layout, 3, "Price:", priceInput);
        if (adding)
        {
            AddEditorRow(layout, 4, "Stock:", stockInput);
            AddEditorRow(layout, 5, "Low stock threshold:", thresholdInput);
        }

        layout.Controls.Add(errorLabel, 0, fieldCount);
        layout.SetColumnSpan(errorLabel, 2);
        layout.Controls.Add(buttonPanel, 0, fieldCount + 1);
        layout.SetColumnSpan(buttonPanel, 2);
        dialog.Controls.Add(layout);
        dialog.AcceptButton = saveButton;
        dialog.CancelButton = cancelButton;

        saveButton.Click += (_, _) =>
        {
            string barcode = barcodeInput.Text.Trim();
            string name = nameInput.Text.Trim().ToUpperInvariant();
            if (barcode.Length == 0 || name.Length == 0)
            {
                errorLabel.Text = "Barcode and name are required.";
                return;
            }

            if (categoryInput.SelectedItem is not ProductCategory category)
            {
                errorLabel.Text = "Select a category.";
                return;
            }

            int stock = existingProduct?.Stock ?? 0;
            if (adding && !TryParseOptionalNonNegativeInt(stockInput.Text, 0, out stock))
            {
                errorLabel.Text = "Stock must be a non-negative whole number or blank.";
                return;
            }

            int threshold = existingProduct?.LowStockThreshold ?? CsvDataStore.DefaultLowStockThreshold;
            if (adding && !TryParseOptionalNonNegativeInt(thresholdInput.Text, threshold, out threshold))
            {
                errorLabel.Text = "Low stock threshold must be a non-negative whole number or blank.";
                return;
            }

            Product product = new()
            {
                Barcode = barcode,
                Name = name,
                CategoryId = category.Id,
                Price = priceInput.Value,
                Stock = stock,
                ImageFileName = existingProduct?.ImageFileName ?? string.Empty,
                LowStockThreshold = threshold
            };

            try
            {
                if (existingProduct is null)
                {
                    _store.AddProduct(product);
                }
                else
                {
                    _store.UpdateProduct(existingProduct.Barcode, product);
                }
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or InvalidDataException)
            {
                errorLabel.Text = exception.Message;
                return;
            }

            addedProduct = product;
            dialog.DialogResult = DialogResult.OK;
            dialog.Close();
        };

        if (dialog.ShowDialog(this) != DialogResult.OK || addedProduct is null)
        {
            return;
        }

        _products = _store.LoadProducts();
        _searchBox.Text = addedProduct.Barcode;
        RefreshInventory();
        _statusLabel.Text = adding
            ? $"Added {addedProduct.Name} to inventory."
            : $"Updated {addedProduct.Name}.";
    }

    private static void AddEditorRow(TableLayoutPanel layout, int row, string labelText, Control editor)
    {
        Label label = new()
        {
            Dock = DockStyle.Fill,
            Text = labelText,
            TextAlign = ContentAlignment.MiddleLeft
        };
        layout.Controls.Add(label, 0, row);
        layout.Controls.Add(editor, 1, row);
    }

    private static bool TryParseOptionalNonNegativeInt(string text, int defaultValue, out int value)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            value = defaultValue;
            return true;
        }

        return int.TryParse(text.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out value);
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
            _lowStockThreshold.Enabled = false;
            _saveThresholdButton.Enabled = false;
            _editProductButton.Enabled = false;
            _deleteProductButton.Enabled = false;
            _statusLabel.Text = "Select a product to adjust stock.";
            return;
        }

        _lowStockThreshold.Value = product.LowStockThreshold;
        _lowStockThreshold.Enabled = true;
        _saveThresholdButton.Enabled = true;
        _editProductButton.Enabled = true;
        int reserved = _reservedStock.TryGetValue(product.Barcode, out int quantity) ? quantity : 0;
        _deleteProductButton.Enabled = reserved == 0;
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
