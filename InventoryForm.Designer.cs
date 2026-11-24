#nullable enable

namespace POS;

partial class InventoryForm
{
    private System.ComponentModel.IContainer? components = null;
    private System.Windows.Forms.TableLayoutPanel _rootLayout = null!;
    private System.Windows.Forms.TableLayoutPanel _titleLayout = null!;
    private System.Windows.Forms.Label _titleLabel = null!;
    private System.Windows.Forms.Label _subtitleLabel = null!;
    private System.Windows.Forms.TextBox _searchBox = null!;
    private System.Windows.Forms.DataGridView _inventoryGrid = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _barcodeColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _productColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _categoryColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _priceColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _stockColumn = null!;
    private System.Windows.Forms.TableLayoutPanel _actionsLayout = null!;
    private System.Windows.Forms.Label _quantityLabel = null!;
    private System.Windows.Forms.NumericUpDown _quantityInput = null!;
    private System.Windows.Forms.Button _addStockButton = null!;
    private System.Windows.Forms.Button _removeStockButton = null!;
    private System.Windows.Forms.Label _statusLabel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        _rootLayout = new System.Windows.Forms.TableLayoutPanel();
        _titleLayout = new System.Windows.Forms.TableLayoutPanel();
        _titleLabel = new System.Windows.Forms.Label();
        _subtitleLabel = new System.Windows.Forms.Label();
        _searchBox = new System.Windows.Forms.TextBox();
        _inventoryGrid = new System.Windows.Forms.DataGridView();
        _barcodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _productColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _priceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _stockColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _actionsLayout = new System.Windows.Forms.TableLayoutPanel();
        _quantityLabel = new System.Windows.Forms.Label();
        _quantityInput = new System.Windows.Forms.NumericUpDown();
        _addStockButton = new System.Windows.Forms.Button();
        _removeStockButton = new System.Windows.Forms.Button();
        _statusLabel = new System.Windows.Forms.Label();
        _rootLayout.SuspendLayout();
        _titleLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_inventoryGrid).BeginInit();
        _actionsLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_quantityInput).BeginInit();
        SuspendLayout();
        //
        // _rootLayout
        //
        _rootLayout.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
        _rootLayout.ColumnCount = 1;
        _rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _rootLayout.Controls.Add(_titleLayout, 0, 0);
        _rootLayout.Controls.Add(_searchBox, 0, 1);
        _rootLayout.Controls.Add(_inventoryGrid, 0, 2);
        _rootLayout.Controls.Add(_actionsLayout, 0, 3);
        _rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _rootLayout.Padding = new System.Windows.Forms.Padding(18);
        _rootLayout.RowCount = 4;
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
        //
        // _titleLayout
        //
        _titleLayout.ColumnCount = 1;
        _titleLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _titleLayout.Controls.Add(_titleLabel, 0, 0);
        _titleLayout.Controls.Add(_subtitleLabel, 0, 1);
        _titleLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _titleLayout.RowCount = 2;
        _titleLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58F));
        _titleLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42F));
        _titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _titleLabel.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
        _titleLabel.ForeColor = System.Drawing.Color.FromArgb(32, 42, 62);
        _titleLabel.Text = "Inventory";
        _titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _subtitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(84, 96, 118);
        _subtitleLabel.Text = "Review product details and adjust available stock.";
        _subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        //
        // _searchBox
        //
        _searchBox.Dock = System.Windows.Forms.DockStyle.Fill;
        _searchBox.Font = new System.Drawing.Font("Segoe UI", 11F);
        _searchBox.Margin = new System.Windows.Forms.Padding(0, 4, 0, 8);
        _searchBox.PlaceholderText = "Search product name, barcode, or category...";
        //
        // _inventoryGrid
        //
        _inventoryGrid.AllowUserToAddRows = false;
        _inventoryGrid.AllowUserToDeleteRows = false;
        _inventoryGrid.AllowUserToResizeRows = false;
        _inventoryGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        _inventoryGrid.BackgroundColor = System.Drawing.Color.White;
        _inventoryGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        _inventoryGrid.ColumnHeadersHeight = 38;
        _inventoryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
        {
            _barcodeColumn,
            _productColumn,
            _categoryColumn,
            _priceColumn,
            _stockColumn
        });
        _inventoryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        _inventoryGrid.MultiSelect = false;
        _inventoryGrid.ReadOnly = true;
        _inventoryGrid.RowHeadersVisible = false;
        _inventoryGrid.RowTemplate.Height = 34;
        _inventoryGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        _barcodeColumn.FillWeight = 125F;
        _barcodeColumn.HeaderText = "Barcode";
        _barcodeColumn.Name = "barcodeColumn";
        _barcodeColumn.ReadOnly = true;
        _productColumn.FillWeight = 180F;
        _productColumn.HeaderText = "Product";
        _productColumn.Name = "productColumn";
        _productColumn.ReadOnly = true;
        _categoryColumn.FillWeight = 115F;
        _categoryColumn.HeaderText = "Category";
        _categoryColumn.Name = "categoryColumn";
        _categoryColumn.ReadOnly = true;
        _priceColumn.FillWeight = 80F;
        _priceColumn.HeaderText = "Price";
        _priceColumn.Name = "priceColumn";
        _priceColumn.ReadOnly = true;
        _stockColumn.FillWeight = 65F;
        _stockColumn.HeaderText = "In Stock";
        _stockColumn.Name = "stockColumn";
        _stockColumn.ReadOnly = true;
        //
        // _actionsLayout
        //
        _actionsLayout.ColumnCount = 5;
        _actionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
        _actionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
        _actionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
        _actionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
        _actionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _actionsLayout.Controls.Add(_quantityLabel, 0, 0);
        _actionsLayout.Controls.Add(_quantityInput, 1, 0);
        _actionsLayout.Controls.Add(_addStockButton, 2, 0);
        _actionsLayout.Controls.Add(_removeStockButton, 3, 0);
        _actionsLayout.Controls.Add(_statusLabel, 4, 0);
        _actionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _actionsLayout.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
        _actionsLayout.RowCount = 1;
        _actionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _quantityLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _quantityLabel.ForeColor = System.Drawing.Color.FromArgb(32, 42, 62);
        _quantityLabel.Text = "Quantity:";
        _quantityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _quantityInput.Dock = System.Windows.Forms.DockStyle.Fill;
        _quantityInput.Font = new System.Drawing.Font("Segoe UI", 11F);
        _quantityInput.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
        _quantityInput.Maximum = 1000000;
        _quantityInput.Minimum = 1;
        _quantityInput.Value = 1;
        _addStockButton.BackColor = System.Drawing.Color.FromArgb(47, 126, 223);
        _addStockButton.Dock = System.Windows.Forms.DockStyle.Fill;
        _addStockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _addStockButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        _addStockButton.ForeColor = System.Drawing.Color.White;
        _addStockButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
        _addStockButton.Text = "Add Stock";
        _removeStockButton.BackColor = System.Drawing.Color.FromArgb(224, 75, 58);
        _removeStockButton.Dock = System.Windows.Forms.DockStyle.Fill;
        _removeStockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _removeStockButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        _removeStockButton.ForeColor = System.Drawing.Color.White;
        _removeStockButton.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
        _removeStockButton.Text = "Remove Stock";
        _statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _statusLabel.ForeColor = System.Drawing.Color.FromArgb(84, 96, 118);
        _statusLabel.Text = "Select a product to adjust stock.";
        _statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        //
        // InventoryForm
        //
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.White;
        ClientSize = new System.Drawing.Size(900, 600);
        Controls.Add(_rootLayout);
        Font = new System.Drawing.Font("Segoe UI", 10F);
        MinimumSize = new System.Drawing.Size(760, 500);
        Name = "InventoryForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "Inventory | MADIES Store";
        _actionsLayout.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_quantityInput).EndInit();
        ((System.ComponentModel.ISupportInitialize)_inventoryGrid).EndInit();
        _titleLayout.ResumeLayout(false);
        _rootLayout.ResumeLayout(false);
        _rootLayout.PerformLayout();
        ResumeLayout(false);
    }
}
