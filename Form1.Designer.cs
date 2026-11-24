#nullable enable

namespace POS;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;
    private System.Windows.Forms.TableLayoutPanel _rootLayout = null!;
    private System.Windows.Forms.Panel _headerPanel = null!;
    private System.Windows.Forms.TableLayoutPanel _headerLayout = null!;
    private System.Windows.Forms.TableLayoutPanel _brandLayout = null!;
    private System.Windows.Forms.PictureBox _logoPictureBox = null!;
    private System.Windows.Forms.TableLayoutPanel _headerDetails = null!;
    private System.Windows.Forms.Label _brandLabel = null!;
    private System.Windows.Forms.Label _clockLabel = null!;
    private System.Windows.Forms.Label _cashierLabel = null!;
    private System.Windows.Forms.TableLayoutPanel _mainContent = null!;
    private System.Windows.Forms.TableLayoutPanel _browseLayout = null!;
    private System.Windows.Forms.FlowLayoutPanel _categoryList = null!;
    private System.Windows.Forms.TableLayoutPanel _productLayout = null!;
    private System.Windows.Forms.TextBox _searchBox = null!;
    private System.Windows.Forms.FlowLayoutPanel _productList = null!;
    private System.Windows.Forms.TableLayoutPanel _orderLayout = null!;
    private System.Windows.Forms.DataGridView _cartGrid = null!;
    private System.Windows.Forms.FlowLayoutPanel _quantityActions = null!;
    private System.Windows.Forms.Button _buttonMinus = null!;
    private System.Windows.Forms.Button _buttonPlus = null!;
    private System.Windows.Forms.Button _buttonRemove = null!;
    private System.Windows.Forms.TableLayoutPanel _totalsLayout = null!;
    private System.Windows.Forms.Label _subtotalLabel = null!;
    private System.Windows.Forms.Label _subtotalValue = null!;
    private System.Windows.Forms.Label _cashLabel = null!;
    private System.Windows.Forms.TextBox _cashBox = null!;
    private System.Windows.Forms.Label _changeLabel = null!;
    private System.Windows.Forms.Label _changeValue = null!;
    private System.Windows.Forms.Panel _totalBand = null!;
    private System.Windows.Forms.Label _totalLabel = null!;
    private System.Windows.Forms.Label _totalValue = null!;
    private System.Windows.Forms.TableLayoutPanel _footerLayout = null!;
    private System.Windows.Forms.Label _instructionLabel = null!;
    private System.Windows.Forms.Button _inventoryButton = null!;
    private System.Windows.Forms.Button _salesSummaryButton = null!;
    private System.Windows.Forms.Button _clearCartButton = null!;
    private System.Windows.Forms.Timer _clock = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _barcodeColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _itemColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _quantityColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _priceColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _lineTotalColumn = null!;
    private System.Windows.Forms.Button _previewProduct1 = null!;
    private System.Windows.Forms.Button _previewProduct2 = null!;
    private System.Windows.Forms.Button _previewProduct3 = null!;
    private System.Windows.Forms.Button _previewProduct4 = null!;
    private System.Windows.Forms.Button _previewProduct5 = null!;
    private System.Windows.Forms.Button _previewProduct6 = null!;
    private System.Windows.Forms.Button _previewProduct7 = null!;
    private System.Windows.Forms.Button _previewProduct8 = null!;
    private System.Windows.Forms.Button _previewProduct9 = null!;
    private System.Windows.Forms.Button _previewProduct10 = null!;
    private System.Windows.Forms.Button _previewProduct11 = null!;
    private System.Windows.Forms.Button _previewProduct12 = null!;
    private System.Windows.Forms.Button _previewCategoryAll = null!;
    private System.Windows.Forms.Button _previewCategoryDrinks = null!;
    private System.Windows.Forms.Button _previewCategorySnacks = null!;
    private System.Windows.Forms.Button _previewCategoryCanned = null!;
    private System.Windows.Forms.Button _previewCategoryPersonalCare = null!;
    private System.Windows.Forms.Button _previewCategoryHousehold = null!;
    private System.Windows.Forms.Button _previewCategoryMore = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        _clock = new System.Windows.Forms.Timer(components);
        _rootLayout = new System.Windows.Forms.TableLayoutPanel();
        _headerPanel = new System.Windows.Forms.Panel();
        _headerLayout = new System.Windows.Forms.TableLayoutPanel();
        _brandLayout = new System.Windows.Forms.TableLayoutPanel();
        _logoPictureBox = new System.Windows.Forms.PictureBox();
        _brandLabel = new System.Windows.Forms.Label();
        _headerDetails = new System.Windows.Forms.TableLayoutPanel();
        _clockLabel = new System.Windows.Forms.Label();
        _cashierLabel = new System.Windows.Forms.Label();
        _mainContent = new System.Windows.Forms.TableLayoutPanel();
        _browseLayout = new System.Windows.Forms.TableLayoutPanel();
        _categoryList = new System.Windows.Forms.FlowLayoutPanel();
        _productLayout = new System.Windows.Forms.TableLayoutPanel();
        _searchBox = new System.Windows.Forms.TextBox();
        _productList = new System.Windows.Forms.FlowLayoutPanel();
        _previewProduct1 = new System.Windows.Forms.Button();
        _previewProduct2 = new System.Windows.Forms.Button();
        _previewProduct3 = new System.Windows.Forms.Button();
        _previewProduct4 = new System.Windows.Forms.Button();
        _previewProduct5 = new System.Windows.Forms.Button();
        _previewProduct6 = new System.Windows.Forms.Button();
        _previewProduct7 = new System.Windows.Forms.Button();
        _previewProduct8 = new System.Windows.Forms.Button();
        _previewProduct9 = new System.Windows.Forms.Button();
        _previewProduct10 = new System.Windows.Forms.Button();
        _previewProduct11 = new System.Windows.Forms.Button();
        _previewProduct12 = new System.Windows.Forms.Button();
        _previewCategoryAll = new System.Windows.Forms.Button();
        _previewCategoryDrinks = new System.Windows.Forms.Button();
        _previewCategorySnacks = new System.Windows.Forms.Button();
        _previewCategoryCanned = new System.Windows.Forms.Button();
        _previewCategoryPersonalCare = new System.Windows.Forms.Button();
        _previewCategoryHousehold = new System.Windows.Forms.Button();
        _previewCategoryMore = new System.Windows.Forms.Button();
        _orderLayout = new System.Windows.Forms.TableLayoutPanel();
        _cartGrid = new System.Windows.Forms.DataGridView();
        _barcodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _itemColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _priceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _lineTotalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _quantityActions = new System.Windows.Forms.FlowLayoutPanel();
        _buttonMinus = new System.Windows.Forms.Button();
        _buttonPlus = new System.Windows.Forms.Button();
        _buttonRemove = new System.Windows.Forms.Button();
        _totalsLayout = new System.Windows.Forms.TableLayoutPanel();
        _subtotalLabel = new System.Windows.Forms.Label();
        _subtotalValue = new System.Windows.Forms.Label();
        _cashLabel = new System.Windows.Forms.Label();
        _cashBox = new System.Windows.Forms.TextBox();
        _changeLabel = new System.Windows.Forms.Label();
        _changeValue = new System.Windows.Forms.Label();
        _totalBand = new System.Windows.Forms.Panel();
        _totalLabel = new System.Windows.Forms.Label();
        _totalValue = new System.Windows.Forms.Label();
        _footerLayout = new System.Windows.Forms.TableLayoutPanel();
        _instructionLabel = new System.Windows.Forms.Label();
        _inventoryButton = new System.Windows.Forms.Button();
        _salesSummaryButton = new System.Windows.Forms.Button();
        _clearCartButton = new System.Windows.Forms.Button();
        _rootLayout.SuspendLayout();
        _headerPanel.SuspendLayout();
        _headerLayout.SuspendLayout();
        _brandLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_logoPictureBox).BeginInit();
        _headerDetails.SuspendLayout();
        _mainContent.SuspendLayout();
        _browseLayout.SuspendLayout();
        _productLayout.SuspendLayout();
        _productList.SuspendLayout();
        _orderLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_cartGrid).BeginInit();
        _quantityActions.SuspendLayout();
        _totalsLayout.SuspendLayout();
        _totalBand.SuspendLayout();
        _footerLayout.SuspendLayout();
        SuspendLayout();
        //
        // _rootLayout
        //
        _rootLayout.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
        _rootLayout.ColumnCount = 1;
        _rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _rootLayout.Controls.Add(_headerPanel, 0, 0);
        _rootLayout.Controls.Add(_mainContent, 0, 1);
        _rootLayout.Controls.Add(_footerLayout, 0, 2);
        _rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _rootLayout.Padding = new System.Windows.Forms.Padding(12);
        _rootLayout.RowCount = 3;
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
        //
        // _headerPanel
        //
        _headerPanel.BackColor = System.Drawing.Color.FromArgb(31, 78, 145);
        _headerPanel.Controls.Add(_headerLayout);
        _headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        _headerPanel.Padding = new System.Windows.Forms.Padding(18, 8, 18, 8);
        //
        // _headerLayout
        //
        _headerLayout.ColumnCount = 2;
        _headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
        _headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
        _headerLayout.Controls.Add(_brandLayout, 0, 0);
        _headerLayout.Controls.Add(_headerDetails, 1, 0);
        _headerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _headerLayout.RowCount = 1;
        _headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        //
        // _brandLayout
        //
        _brandLayout.ColumnCount = 2;
        _brandLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
        _brandLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _brandLayout.Controls.Add(_logoPictureBox, 0, 0);
        _brandLayout.Controls.Add(_brandLabel, 1, 0);
        _brandLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _brandLayout.RowCount = 1;
        _brandLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        //
        // _logoPictureBox
        //
        _logoPictureBox.BackColor = System.Drawing.Color.White;
        _logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
        _logoPictureBox.ImageLocation = "res/madies_it_logo.png";
        _logoPictureBox.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
        _logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        _logoPictureBox.WaitOnLoad = true;
        //
        // _brandLabel
        //
        _brandLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _brandLabel.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
        _brandLabel.ForeColor = System.Drawing.Color.White;
        _brandLabel.Text = "MADIES Store";
        _brandLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        //
        // _headerDetails
        //
        _headerDetails.ColumnCount = 2;
        _headerDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
        _headerDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
        _headerDetails.Controls.Add(_clockLabel, 0, 0);
        _headerDetails.Controls.Add(_cashierLabel, 1, 0);
        _headerDetails.Dock = System.Windows.Forms.DockStyle.Fill;
        _headerDetails.RowCount = 1;
        _headerDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        //
        // _clockLabel
        //
        _clockLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _clockLabel.ForeColor = System.Drawing.Color.White;
        _clockLabel.Text = "Nov 24, 2026   02:15 PM";
        _clockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        //
        // _cashierLabel
        //
        _cashierLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _cashierLabel.ForeColor = System.Drawing.Color.White;
        _cashierLabel.Text = "Cashier: Admin";
        _cashierLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        //
        // _mainContent
        //
        _mainContent.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
        _mainContent.ColumnCount = 2;
        _mainContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56F));
        _mainContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44F));
        _mainContent.Controls.Add(_browseLayout, 0, 0);
        _mainContent.Controls.Add(_orderLayout, 1, 0);
        _mainContent.Dock = System.Windows.Forms.DockStyle.Fill;
        _mainContent.Padding = new System.Windows.Forms.Padding(0, 12, 0, 10);
        _mainContent.RowCount = 1;
        _mainContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        //
        // _browseLayout
        //
        _browseLayout.ColumnCount = 2;
        _browseLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
        _browseLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _browseLayout.Controls.Add(_categoryList, 0, 0);
        _browseLayout.Controls.Add(_productLayout, 1, 0);
        _browseLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _browseLayout.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
        _browseLayout.RowCount = 1;
        _browseLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        //
        // _categoryList
        //
        _categoryList.AutoScroll = true;
        _categoryList.Dock = System.Windows.Forms.DockStyle.Fill;
        _categoryList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        _categoryList.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
        _categoryList.WrapContents = false;
        _categoryList.Controls.Add(_previewCategoryAll);
        _categoryList.Controls.Add(_previewCategoryDrinks);
        _categoryList.Controls.Add(_previewCategorySnacks);
        _categoryList.Controls.Add(_previewCategoryCanned);
        _categoryList.Controls.Add(_previewCategoryPersonalCare);
        _categoryList.Controls.Add(_previewCategoryHousehold);
        _categoryList.Controls.Add(_previewCategoryMore);
        _previewCategoryAll.Text = "▦  All Items";
        _previewCategoryDrinks.Text = "🥤  Drinks";
        _previewCategorySnacks.Text = "🍿  Snacks";
        _previewCategoryCanned.Text = "🥫  Canned Goods";
        _previewCategoryPersonalCare.Text = "🧴  Personal Care";
        _previewCategoryHousehold.Text = "🧹  Household";
        _previewCategoryMore.Text = "•••  More  ▾";
        _previewCategoryAll.Size = new System.Drawing.Size(122, 50);
        _previewCategoryDrinks.Size = new System.Drawing.Size(122, 50);
        _previewCategorySnacks.Size = new System.Drawing.Size(122, 50);
        _previewCategoryCanned.Size = new System.Drawing.Size(122, 50);
        _previewCategoryPersonalCare.Size = new System.Drawing.Size(122, 50);
        _previewCategoryHousehold.Size = new System.Drawing.Size(122, 50);
        _previewCategoryMore.Size = new System.Drawing.Size(122, 50);
        _previewCategoryAll.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
        _previewCategoryDrinks.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
        _previewCategorySnacks.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
        _previewCategoryCanned.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
        _previewCategoryPersonalCare.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
        _previewCategoryHousehold.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
        _previewCategoryMore.Margin = new System.Windows.Forms.Padding(0, 0, 0, 7);
        _previewCategoryAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewCategoryDrinks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewCategorySnacks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewCategoryCanned.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewCategoryPersonalCare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewCategoryHousehold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewCategoryMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewCategoryAll.BackColor = System.Drawing.Color.FromArgb(47, 126, 223);
        _previewCategoryDrinks.BackColor = System.Drawing.Color.FromArgb(239, 244, 253);
        _previewCategorySnacks.BackColor = System.Drawing.Color.FromArgb(239, 244, 253);
        _previewCategoryCanned.BackColor = System.Drawing.Color.FromArgb(239, 244, 253);
        _previewCategoryPersonalCare.BackColor = System.Drawing.Color.FromArgb(239, 244, 253);
        _previewCategoryHousehold.BackColor = System.Drawing.Color.FromArgb(239, 244, 253);
        _previewCategoryMore.BackColor = System.Drawing.Color.FromArgb(239, 244, 253);
        _previewCategoryAll.ForeColor = System.Drawing.Color.White;
        _previewCategoryDrinks.ForeColor = System.Drawing.Color.FromArgb(32, 42, 62);
        _previewCategorySnacks.ForeColor = System.Drawing.Color.FromArgb(32, 42, 62);
        _previewCategoryCanned.ForeColor = System.Drawing.Color.FromArgb(32, 42, 62);
        _previewCategoryPersonalCare.ForeColor = System.Drawing.Color.FromArgb(32, 42, 62);
        _previewCategoryHousehold.ForeColor = System.Drawing.Color.FromArgb(32, 42, 62);
        _previewCategoryMore.ForeColor = System.Drawing.Color.FromArgb(32, 42, 62);
        _previewCategoryAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _previewCategoryDrinks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _previewCategorySnacks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _previewCategoryCanned.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _previewCategoryPersonalCare.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _previewCategoryHousehold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _previewCategoryMore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _previewCategoryAll.Padding = new System.Windows.Forms.Padding(9, 0, 2, 0);
        _previewCategoryDrinks.Padding = new System.Windows.Forms.Padding(9, 0, 2, 0);
        _previewCategorySnacks.Padding = new System.Windows.Forms.Padding(9, 0, 2, 0);
        _previewCategoryCanned.Padding = new System.Windows.Forms.Padding(9, 0, 2, 0);
        _previewCategoryPersonalCare.Padding = new System.Windows.Forms.Padding(9, 0, 2, 0);
        _previewCategoryHousehold.Padding = new System.Windows.Forms.Padding(9, 0, 2, 0);
        _previewCategoryMore.Padding = new System.Windows.Forms.Padding(9, 0, 2, 0);
        //
        // _productLayout
        //
        _productLayout.ColumnCount = 1;
        _productLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _productLayout.Controls.Add(_searchBox, 0, 0);
        _productLayout.Controls.Add(_productList, 0, 1);
        _productLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _productLayout.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
        _productLayout.RowCount = 2;
        _productLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
        _productLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        //
        // _searchBox
        //
        _searchBox.Dock = System.Windows.Forms.DockStyle.Fill;
        _searchBox.Font = new System.Drawing.Font("Segoe UI", 11F);
        _searchBox.PlaceholderText = "Search name or scan barcode...";
        //
        // _productList
        //
        _productList.AutoScroll = true;
        _productList.Dock = System.Windows.Forms.DockStyle.Fill;
        _productList.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
        _productList.WrapContents = true;
        _productList.Controls.AddRange(new System.Windows.Forms.Control[]
        {
            _previewProduct1, _previewProduct2, _previewProduct3, _previewProduct4,
            _previewProduct5, _previewProduct6, _previewProduct7, _previewProduct8,
            _previewProduct9, _previewProduct10, _previewProduct11, _previewProduct12
        });
        //
        // Preview product cards
        //
        _previewProduct1.Text = "🥤\nCoca-Cola\n₱18.00";
        _previewProduct2.Text = "🥤\nSprite\n₱18.00";
        _previewProduct3.Text = "🥤\nRoyal\n₱18.00";
        _previewProduct4.Text = "💧\nNature's Spring\n₱12.00";
        _previewProduct5.Text = "🍟\nPiattos\n₱20.00";
        _previewProduct6.Text = "🍟\nChippy\n₱20.00";
        _previewProduct7.Text = "🍿\nNova\n₱20.00";
        _previewProduct8.Text = "🍘\nOishi Crackers\n₱20.00";
        _previewProduct9.Text = "🍜\nLucky Me Pancit Canton\n₱14.00";
        _previewProduct10.Text = "🥫\nCentury Tuna\n₱25.00";
        _previewProduct11.Text = "🥫\n555 Sardines\n₱20.00";
        _previewProduct12.Text = "🍘\nSkyFlakes\n₱16.00";
        _previewProduct1.Size = new System.Drawing.Size(120, 132);
        _previewProduct2.Size = new System.Drawing.Size(120, 132);
        _previewProduct3.Size = new System.Drawing.Size(120, 132);
        _previewProduct4.Size = new System.Drawing.Size(120, 132);
        _previewProduct5.Size = new System.Drawing.Size(120, 132);
        _previewProduct6.Size = new System.Drawing.Size(120, 132);
        _previewProduct7.Size = new System.Drawing.Size(120, 132);
        _previewProduct8.Size = new System.Drawing.Size(120, 132);
        _previewProduct9.Size = new System.Drawing.Size(120, 132);
        _previewProduct10.Size = new System.Drawing.Size(120, 132);
        _previewProduct11.Size = new System.Drawing.Size(120, 132);
        _previewProduct12.Size = new System.Drawing.Size(120, 132);
        _previewProduct1.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct2.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct3.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct4.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct5.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct6.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct7.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct8.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct9.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct10.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct11.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct12.Margin = new System.Windows.Forms.Padding(3);
        _previewProduct1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _previewProduct1.BackColor = System.Drawing.Color.White;
        _previewProduct2.BackColor = System.Drawing.Color.White;
        _previewProduct3.BackColor = System.Drawing.Color.White;
        _previewProduct4.BackColor = System.Drawing.Color.White;
        _previewProduct5.BackColor = System.Drawing.Color.White;
        _previewProduct6.BackColor = System.Drawing.Color.White;
        _previewProduct7.BackColor = System.Drawing.Color.White;
        _previewProduct8.BackColor = System.Drawing.Color.White;
        _previewProduct9.BackColor = System.Drawing.Color.White;
        _previewProduct10.BackColor = System.Drawing.Color.White;
        _previewProduct11.BackColor = System.Drawing.Color.White;
        _previewProduct12.BackColor = System.Drawing.Color.White;
        _previewProduct1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct4.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct5.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct6.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct7.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct8.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct9.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct10.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct11.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct12.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
        _previewProduct1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        _previewProduct2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        _previewProduct3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        _previewProduct4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        _previewProduct5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        _previewProduct6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        _previewProduct7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        _previewProduct8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        _previewProduct9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        _previewProduct10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        _previewProduct11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        _previewProduct12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // _orderLayout
        //
        _orderLayout.BackColor = System.Drawing.Color.White;
        _orderLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
        _orderLayout.ColumnCount = 1;
        _orderLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _orderLayout.Controls.Add(_cartGrid, 0, 0);
        _orderLayout.Controls.Add(_quantityActions, 0, 1);
        _orderLayout.Controls.Add(_totalsLayout, 0, 2);
        _orderLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _orderLayout.Margin = new System.Windows.Forms.Padding(0);
        _orderLayout.RowCount = 3;
        _orderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _orderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
        _orderLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 174F));
        //
        // _cartGrid
        //
        _cartGrid.AllowUserToAddRows = false;
        _cartGrid.AllowUserToDeleteRows = false;
        _cartGrid.AllowUserToResizeRows = false;
        _cartGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        _cartGrid.BackgroundColor = System.Drawing.Color.White;
        _cartGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
        _cartGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(31, 78, 145);
        _cartGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        _cartGrid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
        _cartGrid.ColumnHeadersHeight = 38;
        _cartGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
        {
            _barcodeColumn, _itemColumn, _quantityColumn, _priceColumn, _lineTotalColumn
        });
        _cartGrid.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
        _cartGrid.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(222, 234, 252);
        _cartGrid.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(32, 42, 62);
        _cartGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        _cartGrid.EnableHeadersVisualStyles = false;
        _cartGrid.MultiSelect = false;
        _cartGrid.ReadOnly = true;
        _cartGrid.RowHeadersVisible = false;
        _cartGrid.RowTemplate.Height = 34;
        _cartGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        //
        // _barcodeColumn
        //
        _barcodeColumn.HeaderText = "Barcode";
        _barcodeColumn.Name = "barcode";
        _barcodeColumn.Visible = false;
        //
        // _itemColumn
        //
        _itemColumn.FillWeight = 42F;
        _itemColumn.HeaderText = "Item";
        _itemColumn.Name = "name";
        //
        // _quantityColumn
        //
        _quantityColumn.FillWeight = 15F;
        _quantityColumn.HeaderText = "Qty";
        _quantityColumn.Name = "quantity";
        //
        // _priceColumn
        //
        _priceColumn.FillWeight = 20F;
        _priceColumn.HeaderText = "Price";
        _priceColumn.Name = "price";
        //
        // _lineTotalColumn
        //
        _lineTotalColumn.FillWeight = 23F;
        _lineTotalColumn.HeaderText = "Total";
        _lineTotalColumn.Name = "total";
        //
        // _quantityActions
        //
        _quantityActions.BackColor = System.Drawing.Color.White;
        _quantityActions.Dock = System.Windows.Forms.DockStyle.Fill;
        _quantityActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
        _quantityActions.Padding = new System.Windows.Forms.Padding(5, 4, 5, 2);
        _quantityActions.Controls.Add(_buttonRemove);
        _quantityActions.Controls.Add(_buttonPlus);
        _quantityActions.Controls.Add(_buttonMinus);
        //
        // quantity buttons
        //
        _buttonMinus.Text = "− Qty";
        _buttonMinus.Size = new System.Drawing.Size(76, 34);
        _buttonPlus.Text = "+ Qty";
        _buttonPlus.Size = new System.Drawing.Size(76, 34);
        _buttonRemove.Text = "Remove item";
        _buttonRemove.Size = new System.Drawing.Size(104, 34);
        //
        // _totalsLayout
        //
        _totalsLayout.BackColor = System.Drawing.Color.FromArgb(250, 251, 254);
        _totalsLayout.ColumnCount = 2;
        _totalsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52F));
        _totalsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
        _totalsLayout.Controls.Add(_subtotalLabel, 0, 0);
        _totalsLayout.Controls.Add(_subtotalValue, 1, 0);
        _totalsLayout.Controls.Add(_cashLabel, 0, 1);
        _totalsLayout.Controls.Add(_cashBox, 1, 1);
        _totalsLayout.Controls.Add(_changeLabel, 0, 2);
        _totalsLayout.Controls.Add(_changeValue, 1, 2);
        _totalsLayout.Controls.Add(_totalBand, 0, 3);
        _totalsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _totalsLayout.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
        _totalsLayout.RowCount = 4;
        _totalsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24F));
        _totalsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
        _totalsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
        _totalsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
        _totalsLayout.SetColumnSpan(_totalBand, 2);
        //
        // subtotal and change
        //
        _subtotalLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _subtotalLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        _subtotalLabel.Text = "Subtotal:";
        _subtotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _subtotalValue.Dock = System.Windows.Forms.DockStyle.Fill;
        _subtotalValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        _subtotalValue.Text = "₱66.00";
        _subtotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        _cashLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _cashLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        _cashLabel.Text = "Cash received:";
        _cashLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _cashBox.Dock = System.Windows.Forms.DockStyle.Fill;
        _cashBox.Font = new System.Drawing.Font("Segoe UI", 11F);
        _cashBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
        _cashBox.PlaceholderText = "Enter cash received";
        _cashBox.Text = "0.00";
        _cashBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        _changeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _changeLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        _changeLabel.Text = "Change:";
        _changeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _changeValue.Dock = System.Windows.Forms.DockStyle.Fill;
        _changeValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        _changeValue.Text = "—";
        _changeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        //
        // _totalBand
        //
        _totalBand.BackColor = System.Drawing.Color.FromArgb(31, 78, 145);
        _totalBand.Controls.Add(_totalValue);
        _totalBand.Controls.Add(_totalLabel);
        _totalBand.Dock = System.Windows.Forms.DockStyle.Fill;
        _totalBand.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
        _totalBand.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
        _totalLabel.Dock = System.Windows.Forms.DockStyle.Left;
        _totalLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
        _totalLabel.ForeColor = System.Drawing.Color.White;
        _totalLabel.Text = "TOTAL";
        _totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _totalLabel.Width = 90;
        _totalValue.Dock = System.Windows.Forms.DockStyle.Fill;
        _totalValue.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
        _totalValue.ForeColor = System.Drawing.Color.White;
        _totalValue.Text = "₱66.00";
        _totalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        //
        // _footerLayout
        //
        _footerLayout.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
        _footerLayout.ColumnCount = 4;
        _footerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _footerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
        _footerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
        _footerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
        _footerLayout.Controls.Add(_instructionLabel, 0, 0);
        _footerLayout.Controls.Add(_inventoryButton, 1, 0);
        _footerLayout.Controls.Add(_salesSummaryButton, 2, 0);
        _footerLayout.Controls.Add(_clearCartButton, 3, 0);
        _footerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _footerLayout.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
        _footerLayout.RowCount = 1;
        _footerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _instructionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _instructionLabel.ForeColor = System.Drawing.Color.FromArgb(84, 96, 118);
        _instructionLabel.Text = "Search products or scan a barcode. Press Enter in Cash received to complete a sale.";
        _instructionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _inventoryButton.BackColor = System.Drawing.Color.FromArgb(47, 126, 223);
        _inventoryButton.Dock = System.Windows.Forms.DockStyle.Fill;
        _inventoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _inventoryButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        _inventoryButton.ForeColor = System.Drawing.Color.White;
        _inventoryButton.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
        _inventoryButton.Text = "Inventory";
        _salesSummaryButton.BackColor = System.Drawing.Color.FromArgb(47, 126, 223);
        _salesSummaryButton.Dock = System.Windows.Forms.DockStyle.Fill;
        _salesSummaryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _salesSummaryButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        _salesSummaryButton.ForeColor = System.Drawing.Color.White;
        _salesSummaryButton.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
        _salesSummaryButton.Text = "Sales Summary";
        _clearCartButton.BackColor = System.Drawing.Color.FromArgb(224, 75, 58);
        _clearCartButton.Dock = System.Windows.Forms.DockStyle.Fill;
        _clearCartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _clearCartButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        _clearCartButton.ForeColor = System.Drawing.Color.White;
        _clearCartButton.Text = "🗑  Clear Cart";
        //
        // Form1
        //
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.White;
        ClientSize = new System.Drawing.Size(1240, 790);
        Controls.Add(_rootLayout);
        Font = new System.Drawing.Font("Segoe UI", 10F);
        MinimumSize = new System.Drawing.Size(1000, 650);
        Name = "Form1";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Sari-Sari Store | Point of Sale";
        _footerLayout.ResumeLayout(false);
        _totalBand.ResumeLayout(false);
        _totalsLayout.ResumeLayout(false);
        _totalsLayout.PerformLayout();
        _quantityActions.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_cartGrid).EndInit();
        _orderLayout.ResumeLayout(false);
        _productList.ResumeLayout(false);
        _productLayout.ResumeLayout(false);
        _productLayout.PerformLayout();
        _browseLayout.ResumeLayout(false);
        _mainContent.ResumeLayout(false);
        _headerDetails.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_logoPictureBox).EndInit();
        _brandLayout.ResumeLayout(false);
        _headerLayout.ResumeLayout(false);
        _headerPanel.ResumeLayout(false);
        _rootLayout.ResumeLayout(false);
        ResumeLayout(false);
    }

}
