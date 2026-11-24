using System.Globalization;
using System.Drawing;

namespace POS;

public partial class Form1 : Form
{
    private static readonly Color Blue = Color.FromArgb(47, 126, 223);
    private static readonly Color PaleBlue = Color.FromArgb(239, 244, 253);
    private static readonly Color Border = Color.FromArgb(211, 221, 239);
    private static readonly Color Ink = Color.FromArgb(32, 42, 62);

    private readonly CsvDataStore _store = new();
    private readonly Dictionary<string, int> _cart = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<Image> _productImages = [];
    private readonly List<ProductCategory> _categories;
    private List<Product> _products = [];
    private string? _selectedCategoryId;

    public Form1()
    {
        InitializeComponent();
        components ??= new System.ComponentModel.Container();
        _logoPictureBox.ImageLocation = Path.Combine(AppContext.BaseDirectory, "res", "madies_it_logo.png");
        _logoPictureBox.Load();

        _categories = _store.LoadCategories();
        _products = _store.LoadProducts();

        _searchBox.TextChanged += (_, _) => RefreshProducts();
        _searchBox.KeyDown += SearchBox_KeyDown;
        _productList.SizeChanged += (_, _) => RefreshProducts();
        _cashBox.KeyDown += CashBox_KeyDown;
        _cashBox.TextChanged += (_, _) => UpdateTotals();
        _buttonMinus.Click += (_, _) => ChangeSelectedQuantity(-1);
        _buttonPlus.Click += (_, _) => ChangeSelectedQuantity(1);
        _buttonRemove.Click += (_, _) => RemoveSelectedProduct();
        _clearCartButton.Click += (_, _) => ClearCart();
        _inventoryButton.Click += (_, _) => ManageInventory();
        _clock.Tick += (_, _) => UpdateHeader();
        _clock.Start();

        BuildCategories();
        RefreshProducts();
        RefreshCart();
        UpdateHeader();
    }

    private void ManageInventory()
    {
        Dictionary<string, int> reservedStock = new(_cart, StringComparer.OrdinalIgnoreCase);
        using InventoryForm inventoryForm = new(_store, _categories, reservedStock);
        inventoryForm.ShowDialog(this);

        _products = _store.LoadProducts();
        RefreshProducts();
        RefreshCart();
    }

    private void BuildCategories()
    {
        _categoryList.Controls.Clear();
        _categoryList.Controls.Add(CreateCategoryButton("▦", "All Items", null));
        foreach (ProductCategory category in _categories.Take(5))
        {
            _categoryList.Controls.Add(CreateCategoryButton(category.Icon, category.Name, category.Id));
        }

        ProductCategory[] additionalCategories = _categories.Skip(5).ToArray();
        if (additionalCategories.Length == 0)
        {
            return;
        }

        Button moreButton = CreateCategoryButton("•••", "More  ▾", null, isMore: true);
        moreButton.Click += (_, _) =>
        {
            ContextMenuStrip menu = new();
            foreach (ProductCategory category in additionalCategories)
            {
                ToolStripMenuItem item = new(category.Icon + "  " + category.Name);
                item.Click += (_, _) =>
                {
                    _selectedCategoryId = category.Id;
                    BuildCategories();
                    RefreshProducts();
                };
                menu.Items.Add(item);
            }

            menu.Show(moreButton, new Point(0, moreButton.Height));
        };
        _categoryList.Controls.Add(moreButton);
    }

    private Button CreateCategoryButton(string icon, string name, string? categoryId, bool isMore = false)
    {
        Button button = new()
        {
            Width = 122,
            Height = 50,
            Margin = new Padding(0, 0, 0, 7),
            FlatStyle = FlatStyle.Flat,
            Text = icon + "  " + name,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(9, 0, 2, 0),
            Font = new Font("Segoe UI", 9.5F),
            BackColor = categoryId == _selectedCategoryId ? Blue : PaleBlue,
            ForeColor = categoryId == _selectedCategoryId ? Color.White : Ink,
            Tag = categoryId
        };
        button.FlatAppearance.BorderColor = Border;
        button.FlatAppearance.BorderSize = 1;
        button.Click += (_, _) =>
        {
            if (isMore)
            {
                return;
            }

            _selectedCategoryId = categoryId;
            BuildCategories();
            RefreshProducts();
        };
        return button;
    }

    private void RefreshProducts()
    {
        if (_productList.IsDisposed)
        {
            return;
        }

        _productList.SuspendLayout();
        try
        {
            foreach (Image image in _productImages)
            {
                image.Dispose();
            }

            _productImages.Clear();
            foreach (Control control in _productList.Controls.Cast<Control>().ToArray())
            {
                control.Dispose();
            }

            _productList.Controls.Clear();
            int cardWidth = Math.Max(112, (_productList.ClientSize.Width - 24) / 4);
            IEnumerable<Product> filtered = _products.Where(product =>
                (_selectedCategoryId is null || product.CategoryId.Equals(_selectedCategoryId, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(_searchBox.Text) ||
                 product.Name.Contains(_searchBox.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) ||
                 product.Barcode.Contains(_searchBox.Text.Trim(), StringComparison.OrdinalIgnoreCase)));

            foreach (Product product in filtered)
            {
                Image? productImage = LoadProductImage(product);
                if (productImage is not null)
                {
                    _productImages.Add(productImage);
                }

                Button card = new()
                {
                    Width = cardWidth,
                    Height = 132,
                    Margin = new Padding(3),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.White,
                    ForeColor = Ink,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    Text = (productImage is null ? product.Icon + Environment.NewLine : string.Empty) +
                           product.Name + Environment.NewLine + $"₱{product.Price:N2}" +
                           Environment.NewLine + $"Stock: {product.Stock}",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = product.Barcode,
                    Enabled = product.Stock > 0,
                    Image = productImage,
                    ImageAlign = ContentAlignment.MiddleCenter,
                    TextImageRelation = TextImageRelation.ImageAboveText
                };
                card.FlatAppearance.BorderColor = Border;
                card.FlatAppearance.BorderSize = 1;
                card.Click += (_, _) => AddProductToCart(product);
                _productList.Controls.Add(card);
            }
        }
        finally
        {
            _productList.ResumeLayout();
        }
    }

    private void AddProductToCart(Product product)
    {
        int quantityInCart = _cart.TryGetValue(product.Barcode, out int current) ? current : 0;
        if (quantityInCart >= product.Stock)
        {
            MessageBox.Show($"Only {product.Stock} {product.Name} available in stock.",
                "Insufficient stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _cart[product.Barcode] = quantityInCart + 1;
        RefreshCart();
    }

    private void RefreshCart()
    {
        _cartGrid.Rows.Clear();
        foreach ((string productId, int quantity) in _cart)
        {
            Product? product = _products.FirstOrDefault(item => item.Barcode.Equals(productId, StringComparison.OrdinalIgnoreCase));
            if (product is null)
            {
                continue;
            }

            _cartGrid.Rows.Add(product.Barcode, product.Name, quantity, $"₱{product.Price:N2}",
                $"₱{product.Price * quantity:N2}");
        }

        UpdateTotals();
    }

    private void UpdateTotals()
    {
        decimal subtotal = GetSubtotal();
        _subtotalValue.Text = $"₱{subtotal:N2}";
        _totalValue.Text = $"₱{subtotal:N2}";
        if (TryReadCash(out decimal cash))
        {
            decimal change = cash - subtotal;
            _changeValue.Text = change >= 0 ? $"₱{change:N2}" : "—";
        }
        else
        {
            _changeValue.Text = "—";
        }
    }

    private decimal GetSubtotal() => _cart.Sum(entry =>
    {
        Product? product = _products.FirstOrDefault(item => item.Barcode.Equals(entry.Key, StringComparison.OrdinalIgnoreCase));
        return product is null ? 0 : product.Price * entry.Value;
    });

    private void CashBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            CompleteSale();
        }
    }

    private void SearchBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        string barcode = _searchBox.Text.Trim();
        Product? product = _products.FirstOrDefault(item =>
            item.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
        if (product is not null)
        {
            e.SuppressKeyPress = true;
            AddProductToCart(product);
            _searchBox.Clear();
        }
        else if (barcode.Length >= 8 && barcode.All(char.IsDigit))
        {
            e.SuppressKeyPress = true;
            MessageBox.Show($"No product is registered with barcode {barcode}.",
                "Barcode not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _searchBox.SelectAll();
        }
    }

    private void CompleteSale()
    {
        if (_cart.Count == 0)
        {
            MessageBox.Show("Add at least one product before completing a sale.",
                "Cart is empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!TryReadCash(out decimal cashReceived))
        {
            MessageBox.Show("Enter a valid cash amount.", "Invalid amount",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _cashBox.Focus();
            return;
        }

        decimal total = GetSubtotal();
        if (cashReceived < total)
        {
            MessageBox.Show($"Cash received must be at least ₱{total:N2}.", "Insufficient payment",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _cashBox.Focus();
            return;
        }

        DialogResult confirmation = MessageBox.Show(
            $"Complete this cash sale for ₱{total:N2}?\nCash received: ₱{cashReceived:N2}\nChange: ₱{cashReceived - total:N2}",
            "Confirm sale", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirmation != DialogResult.Yes)
        {
            return;
        }

        DateTime soldAt = DateTime.Now;
        string saleId = soldAt.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
        List<Product> updatedProducts = _products.Select(product => new Product
        {
            Barcode = product.Barcode,
            Name = product.Name,
            CategoryId = product.CategoryId,
            Price = product.Price,
            Stock = product.Stock - (_cart.TryGetValue(product.Barcode, out int quantity) ? quantity : 0),
            Icon = product.Icon,
            ImageFile = product.ImageFile
        }).ToList();

        List<SaleLine> saleLines = _cart.Select(entry =>
        {
            Product product = _products.First(item => item.Barcode.Equals(entry.Key, StringComparison.OrdinalIgnoreCase));
            return new SaleLine
            {
                SaleId = saleId,
                SoldAt = soldAt,
                Product = product,
                Quantity = entry.Value,
                SaleTotal = total,
                CashReceived = cashReceived,
                Change = cashReceived - total
            };
        }).ToList();

        try
        {
            _store.CompleteSale(saleLines, updatedProducts);
        }
        catch (IOException exception)
        {
            ShowSaveError(exception);
            return;
        }
        catch (UnauthorizedAccessException exception)
        {
            ShowSaveError(exception);
            return;
        }

        _products = updatedProducts;
        _cart.Clear();
        _cashBox.Text = "0.00";
        RefreshProducts();
        RefreshCart();
        MessageBox.Show($"Sale saved.\nTotal: ₱{total:N2}\nChange: ₱{cashReceived - total:N2}",
            "Sale complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static void ShowSaveError(Exception exception)
    {
        MessageBox.Show($"The sale could not be saved. No cart items were cleared.\n\n{exception.Message}",
            "Unable to save sale", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void ClearCart()
    {
        if (_cart.Count == 0)
        {
            return;
        }

        DialogResult confirmation = MessageBox.Show("Remove all items from the current cart?",
            "Clear cart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirmation == DialogResult.Yes)
        {
            _cart.Clear();
            _cashBox.Text = "0.00";
            RefreshCart();
        }
    }

    private void ChangeSelectedQuantity(int change)
    {
        if (_cartGrid.CurrentRow?.Cells["barcode"].Value is not string productId ||
            !_cart.TryGetValue(productId, out int quantity))
        {
            return;
        }

        Product? product = _products.FirstOrDefault(item => item.Barcode.Equals(productId, StringComparison.OrdinalIgnoreCase));
        if (product is null)
        {
            return;
        }

        int updatedQuantity = quantity + change;
        if (updatedQuantity > product.Stock)
        {
            MessageBox.Show($"Only {product.Stock} {product.Name} available in stock.",
                "Insufficient stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (updatedQuantity <= 0)
        {
            _cart.Remove(productId);
        }
        else
        {
            _cart[productId] = updatedQuantity;
        }

        RefreshCart();
    }

    private void RemoveSelectedProduct()
    {
        if (_cartGrid.CurrentRow?.Cells["barcode"].Value is string productId)
        {
            _cart.Remove(productId);
            RefreshCart();
        }
    }

    private bool TryReadCash(out decimal cash)
    {
        return decimal.TryParse(_cashBox.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out cash) ||
               decimal.TryParse(_cashBox.Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out cash);
    }

    private Image? LoadProductImage(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.ImageFile))
        {
            return null;
        }

        if (!string.Equals(Path.GetFileName(product.ImageFile), product.ImageFile, StringComparison.Ordinal))
        {
            throw new InvalidDataException(
                $"The image value for barcode {product.Barcode} must be a filename inside the res folder.");
        }

        string sourcePath = Path.Combine(Environment.CurrentDirectory, "res", product.ImageFile);
        string outputPath = Path.Combine(AppContext.BaseDirectory, "res", product.ImageFile);
        string imagePath = File.Exists(sourcePath) ? sourcePath : outputPath;
        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException(
                $"Image '{product.ImageFile}' for barcode {product.Barcode} was not found in the res folder.",
                imagePath);
        }

        using Image sourceImage = Image.FromFile(imagePath);
        return new Bitmap(sourceImage, new Size(58, 58));
    }

    private void UpdateHeader()
    {
        _clockLabel.Text = DateTime.Now.ToString("MMM d, yyyy   hh:mm tt", CultureInfo.CurrentCulture);
        _cashierLabel.Text = "Cashier: Juan Dela Cruz";
    }
}
