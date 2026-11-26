#nullable enable

namespace POS;

partial class SalesSummaryForm
{
    private System.ComponentModel.IContainer? components = null;
    private System.Windows.Forms.TableLayoutPanel _rootLayout = null!;
    private System.Windows.Forms.TableLayoutPanel _titleLayout = null!;
    private System.Windows.Forms.Label _titleLabel = null!;
    private System.Windows.Forms.Label _subtitleLabel = null!;
    private System.Windows.Forms.TableLayoutPanel _dateLayout = null!;
    private System.Windows.Forms.Label _dateLabel = null!;
    private System.Windows.Forms.DateTimePicker _datePicker = null!;
    private System.Windows.Forms.Label _printCountLabel = null!;
    private System.Windows.Forms.Button _printButton = null!;
    private System.Windows.Forms.TableLayoutPanel _cardsLayout = null!;
    private System.Windows.Forms.Panel _salesCard = null!;
    private System.Windows.Forms.Panel _transactionsCard = null!;
    private System.Windows.Forms.Panel _itemsCard = null!;
    private System.Windows.Forms.Panel _cashCard = null!;
    private System.Windows.Forms.Label _salesCaption = null!;
    private System.Windows.Forms.Label _salesValue = null!;
    private System.Windows.Forms.Label _transactionsCaption = null!;
    private System.Windows.Forms.Label _transactionsValue = null!;
    private System.Windows.Forms.Label _itemsCaption = null!;
    private System.Windows.Forms.Label _itemsValue = null!;
    private System.Windows.Forms.Label _cashCaption = null!;
    private System.Windows.Forms.Label _cashValue = null!;
    private System.Windows.Forms.DataGridView _salesGrid = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _barcodeColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _productColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _cashierColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _quantityColumn = null!;
    private System.Windows.Forms.DataGridViewTextBoxColumn _revenueColumn = null!;
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
        _dateLayout = new System.Windows.Forms.TableLayoutPanel();
        _dateLabel = new System.Windows.Forms.Label();
        _datePicker = new System.Windows.Forms.DateTimePicker();
        _printCountLabel = new System.Windows.Forms.Label();
        _printButton = new System.Windows.Forms.Button();
        _cardsLayout = new System.Windows.Forms.TableLayoutPanel();
        _salesCard = new System.Windows.Forms.Panel();
        _transactionsCard = new System.Windows.Forms.Panel();
        _itemsCard = new System.Windows.Forms.Panel();
        _cashCard = new System.Windows.Forms.Panel();
        _salesCaption = new System.Windows.Forms.Label();
        _salesValue = new System.Windows.Forms.Label();
        _transactionsCaption = new System.Windows.Forms.Label();
        _transactionsValue = new System.Windows.Forms.Label();
        _itemsCaption = new System.Windows.Forms.Label();
        _itemsValue = new System.Windows.Forms.Label();
        _cashCaption = new System.Windows.Forms.Label();
        _cashValue = new System.Windows.Forms.Label();
        _salesGrid = new System.Windows.Forms.DataGridView();
        _barcodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _productColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _cashierColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _revenueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        _statusLabel = new System.Windows.Forms.Label();
        _rootLayout.SuspendLayout();
        _titleLayout.SuspendLayout();
        _dateLayout.SuspendLayout();
        _cardsLayout.SuspendLayout();
        _salesCard.SuspendLayout();
        _transactionsCard.SuspendLayout();
        _itemsCard.SuspendLayout();
        _cashCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_salesGrid).BeginInit();
        SuspendLayout();
        //
        // _rootLayout
        //
        _rootLayout.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
        _rootLayout.ColumnCount = 1;
        _rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _rootLayout.Controls.Add(_titleLayout, 0, 0);
        _rootLayout.Controls.Add(_dateLayout, 0, 1);
        _rootLayout.Controls.Add(_cardsLayout, 0, 2);
        _rootLayout.Controls.Add(_salesGrid, 0, 3);
        _rootLayout.Controls.Add(_statusLabel, 0, 4);
        _rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _rootLayout.Padding = new System.Windows.Forms.Padding(18);
        _rootLayout.RowCount = 5;
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
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
        _titleLabel.Text = "Sales Summary";
        _titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _subtitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(84, 96, 118);
        _subtitleLabel.Text = "Daily sales totals and product breakdown.";
        _subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        //
        // _dateLayout
        //
        _dateLayout.ColumnCount = 5;
        _dateLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
        _dateLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
        _dateLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        _dateLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
        _dateLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
        _dateLayout.Controls.Add(_dateLabel, 0, 0);
        _dateLayout.Controls.Add(_datePicker, 1, 0);
        _dateLayout.Controls.Add(_printCountLabel, 3, 0);
        _dateLayout.Controls.Add(_printButton, 4, 0);
        _dateLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _dateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _dateLabel.ForeColor = System.Drawing.Color.FromArgb(32, 42, 62);
        _dateLabel.Text = "Sales date:";
        _dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        _datePicker.Dock = System.Windows.Forms.DockStyle.Fill;
        _datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        _datePicker.CustomFormat = "MMM d, yyyy";
        _datePicker.MaxDate = System.DateTime.Today;
        _datePicker.Value = System.DateTime.Today;
        _printCountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _printCountLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        _printCountLabel.ForeColor = System.Drawing.Color.FromArgb(32, 42, 62);
        _printCountLabel.Text = "PRINT COUNT: 0";
        _printCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        _printButton.BackColor = System.Drawing.Color.FromArgb(47, 126, 223);
        _printButton.Enabled = false;
        _printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        _printButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        _printButton.ForeColor = System.Drawing.Color.White;
        _printButton.Size = new System.Drawing.Size(120, 34);
        _printButton.Text = "Print Summary";
        //
        // _cardsLayout
        //
        _cardsLayout.ColumnCount = 4;
        _cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
        _cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
        _cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
        _cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
        _cardsLayout.Controls.Add(_salesCard, 0, 0);
        _cardsLayout.Controls.Add(_transactionsCard, 1, 0);
        _cardsLayout.Controls.Add(_itemsCard, 2, 0);
        _cardsLayout.Controls.Add(_cashCard, 3, 0);
        _cardsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        _cardsLayout.RowCount = 1;
        _cardsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        //
        // Summary cards
        //
        _salesCard.BackColor = System.Drawing.Color.White;
        _salesCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        _salesCard.Controls.Add(_salesValue);
        _salesCard.Controls.Add(_salesCaption);
        _salesCard.Dock = System.Windows.Forms.DockStyle.Fill;
        _salesCard.Margin = new System.Windows.Forms.Padding(0, 4, 6, 8);
        _transactionsCard.BackColor = System.Drawing.Color.White;
        _transactionsCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        _transactionsCard.Controls.Add(_transactionsValue);
        _transactionsCard.Controls.Add(_transactionsCaption);
        _transactionsCard.Dock = System.Windows.Forms.DockStyle.Fill;
        _transactionsCard.Margin = new System.Windows.Forms.Padding(2, 4, 4, 8);
        _itemsCard.BackColor = System.Drawing.Color.White;
        _itemsCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        _itemsCard.Controls.Add(_itemsValue);
        _itemsCard.Controls.Add(_itemsCaption);
        _itemsCard.Dock = System.Windows.Forms.DockStyle.Fill;
        _itemsCard.Margin = new System.Windows.Forms.Padding(4, 4, 2, 8);
        _cashCard.BackColor = System.Drawing.Color.White;
        _cashCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        _cashCard.Controls.Add(_cashValue);
        _cashCard.Controls.Add(_cashCaption);
        _cashCard.Dock = System.Windows.Forms.DockStyle.Fill;
        _cashCard.Margin = new System.Windows.Forms.Padding(6, 4, 0, 8);
        _salesCaption.Dock = System.Windows.Forms.DockStyle.Top;
        _salesCaption.ForeColor = System.Drawing.Color.FromArgb(84, 96, 118);
        _salesCaption.Height = 30;
        _salesCaption.Padding = new System.Windows.Forms.Padding(10, 4, 0, 0);
        _salesCaption.Text = "Total Sales";
        _salesValue.Dock = System.Windows.Forms.DockStyle.Fill;
        _salesValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        _salesValue.ForeColor = System.Drawing.Color.FromArgb(31, 78, 145);
        _salesValue.Padding = new System.Windows.Forms.Padding(10, 0, 0, 2);
        _salesValue.Text = "₱0.00";
        _transactionsCaption.Dock = System.Windows.Forms.DockStyle.Top;
        _transactionsCaption.ForeColor = System.Drawing.Color.FromArgb(84, 96, 118);
        _transactionsCaption.Height = 30;
        _transactionsCaption.Padding = new System.Windows.Forms.Padding(10, 4, 0, 0);
        _transactionsCaption.Text = "Transactions";
        _transactionsValue.Dock = System.Windows.Forms.DockStyle.Fill;
        _transactionsValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        _transactionsValue.ForeColor = System.Drawing.Color.FromArgb(31, 78, 145);
        _transactionsValue.Padding = new System.Windows.Forms.Padding(10, 0, 0, 2);
        _transactionsValue.Text = "0";
        _itemsCaption.Dock = System.Windows.Forms.DockStyle.Top;
        _itemsCaption.ForeColor = System.Drawing.Color.FromArgb(84, 96, 118);
        _itemsCaption.Height = 30;
        _itemsCaption.Padding = new System.Windows.Forms.Padding(10, 4, 0, 0);
        _itemsCaption.Text = "Items Sold";
        _itemsValue.Dock = System.Windows.Forms.DockStyle.Fill;
        _itemsValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        _itemsValue.ForeColor = System.Drawing.Color.FromArgb(31, 78, 145);
        _itemsValue.Padding = new System.Windows.Forms.Padding(10, 0, 0, 2);
        _itemsValue.Text = "0";
        _cashCaption.Dock = System.Windows.Forms.DockStyle.Top;
        _cashCaption.ForeColor = System.Drawing.Color.FromArgb(84, 96, 118);
        _cashCaption.Height = 30;
        _cashCaption.Padding = new System.Windows.Forms.Padding(10, 4, 0, 0);
        _cashCaption.Text = "Cash Received";
        _cashValue.Dock = System.Windows.Forms.DockStyle.Fill;
        _cashValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        _cashValue.ForeColor = System.Drawing.Color.FromArgb(31, 78, 145);
        _cashValue.Padding = new System.Windows.Forms.Padding(10, 0, 0, 2);
        _cashValue.Text = "₱0.00";
        //
        // _salesGrid
        //
        _salesGrid.AllowUserToAddRows = false;
        _salesGrid.AllowUserToDeleteRows = false;
        _salesGrid.AllowUserToResizeRows = false;
        _salesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        _salesGrid.BackgroundColor = System.Drawing.Color.White;
        _salesGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        _salesGrid.ColumnHeadersHeight = 38;
        _salesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
        {
            _barcodeColumn,
            _productColumn,
            _cashierColumn,
            _quantityColumn,
            _revenueColumn
        });
        _salesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        _salesGrid.MultiSelect = false;
        _salesGrid.ReadOnly = true;
        _salesGrid.RowHeadersVisible = false;
        _salesGrid.RowTemplate.Height = 34;
        _salesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        _barcodeColumn.FillWeight = 120F;
        _barcodeColumn.HeaderText = "Barcode";
        _barcodeColumn.Name = "barcodeColumn";
        _barcodeColumn.ReadOnly = true;
        _productColumn.FillWeight = 220F;
        _productColumn.HeaderText = "Product";
        _productColumn.Name = "productColumn";
        _productColumn.ReadOnly = true;
        _cashierColumn.FillWeight = 130F;
        _cashierColumn.HeaderText = "Cashier";
        _cashierColumn.Name = "cashierColumn";
        _cashierColumn.ReadOnly = true;
        _quantityColumn.FillWeight = 80F;
        _quantityColumn.HeaderText = "Units Sold";
        _quantityColumn.Name = "quantityColumn";
        _quantityColumn.ReadOnly = true;
        _revenueColumn.FillWeight = 110F;
        _revenueColumn.HeaderText = "Revenue";
        _revenueColumn.Name = "revenueColumn";
        _revenueColumn.ReadOnly = true;
        //
        // _statusLabel
        //
        _statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        _statusLabel.ForeColor = System.Drawing.Color.FromArgb(84, 96, 118);
        _statusLabel.Text = "No sales were recorded on this date.";
        _statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        //
        // SalesSummaryForm
        //
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.White;
        ClientSize = new System.Drawing.Size(900, 650);
        Controls.Add(_rootLayout);
        Font = new System.Drawing.Font("Segoe UI", 10F);
        MinimumSize = new System.Drawing.Size(760, 540);
        Name = "SalesSummaryForm";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        Text = $"Sales Summary | MADIES Store | version {AppVersion.Display}";
        ((System.ComponentModel.ISupportInitialize)_salesGrid).EndInit();
        _cashCard.ResumeLayout(false);
        _itemsCard.ResumeLayout(false);
        _transactionsCard.ResumeLayout(false);
        _salesCard.ResumeLayout(false);
        _cardsLayout.ResumeLayout(false);
        _dateLayout.ResumeLayout(false);
        _titleLayout.ResumeLayout(false);
        _rootLayout.ResumeLayout(false);
        ResumeLayout(false);
    }
}
