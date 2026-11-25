using System.Drawing;

namespace POS;

internal sealed class CategoryManagementForm : Form
{
    private readonly CsvDataStore _store;
    private readonly DataGridView _categoryGrid = new();
    private readonly Button _editButton = new();
    private readonly Button _deleteButton = new();
    private readonly Label _statusLabel = new();

    internal CategoryManagementForm(CsvDataStore store)
    {
        _store = store;
        InitializeComponent();
        RefreshCategories();
    }

    private void InitializeComponent()
    {
        Text = "Manage Categories";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        ClientSize = new Size(680, 430);
        MinimumSize = new Size(600, 380);
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        Font = new Font("Segoe UI", 10F);
        BackColor = Color.White;

        TableLayoutPanel layout = new()
        {
            BackColor = Color.FromArgb(248, 249, 252),
            ColumnCount = 1,
            Dock = DockStyle.Fill,
            Padding = new Padding(18),
            RowCount = 4
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));

        Label titleLabel = new()
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 16F, FontStyle.Bold),
            ForeColor = Color.FromArgb(32, 42, 62),
            Text = "Product Categories",
            TextAlign = ContentAlignment.MiddleLeft
        };

        _categoryGrid.AllowUserToAddRows = false;
        _categoryGrid.AllowUserToDeleteRows = false;
        _categoryGrid.AllowUserToResizeRows = false;
        _categoryGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _categoryGrid.BackgroundColor = Color.White;
        _categoryGrid.BorderStyle = BorderStyle.FixedSingle;
        _categoryGrid.ColumnHeadersHeight = 38;
        _categoryGrid.Columns.AddRange(
            new DataGridViewTextBoxColumn
            {
                FillWeight = 90F,
                HeaderText = "ID",
                Name = "categoryIdColumn",
                ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                FillWeight = 180F,
                HeaderText = "Name",
                Name = "categoryNameColumn",
                ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                FillWeight = 60F,
                HeaderText = "Icon",
                Name = "categoryIconColumn",
                ReadOnly = true
            });
        _categoryGrid.Dock = DockStyle.Fill;
        _categoryGrid.MultiSelect = false;
        _categoryGrid.ReadOnly = true;
        _categoryGrid.RowHeadersVisible = false;
        _categoryGrid.RowTemplate.Height = 34;
        _categoryGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _categoryGrid.SelectionChanged += (_, _) => UpdateSelection();
        _categoryGrid.CellDoubleClick += (_, _) => EditSelectedCategory();

        _statusLabel.Dock = DockStyle.Fill;
        _statusLabel.ForeColor = Color.FromArgb(84, 96, 118);
        _statusLabel.TextAlign = ContentAlignment.MiddleLeft;

        Button addButton = CreateButton("Add", Color.FromArgb(47, 126, 223), 85);
        _editButton.Text = "Edit";
        _editButton.Width = 85;
        Button closeButton = CreateButton("Close", Color.FromArgb(110, 120, 136), 85);
        _deleteButton.Text = "Delete";
        _deleteButton.Width = 95;
        _deleteButton.BackColor = Color.FromArgb(224, 75, 58);
        _deleteButton.ForeColor = Color.White;
        _deleteButton.FlatStyle = FlatStyle.Flat;
        _deleteButton.Enabled = false;
        _editButton.Enabled = false;

        FlowLayoutPanel buttonPanel = new()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false
        };
        buttonPanel.Controls.Add(closeButton);
        buttonPanel.Controls.Add(_deleteButton);
        buttonPanel.Controls.Add(_editButton);
        buttonPanel.Controls.Add(addButton);

        layout.Controls.Add(titleLabel, 0, 0);
        layout.Controls.Add(_categoryGrid, 0, 1);
        layout.Controls.Add(_statusLabel, 0, 2);
        layout.Controls.Add(buttonPanel, 0, 3);
        Controls.Add(layout);

        addButton.Click += (_, _) => AddCategory();
        _editButton.Click += (_, _) => EditSelectedCategory();
        _deleteButton.Click += (_, _) => DeleteSelectedCategory();
        closeButton.Click += (_, _) => Close();
    }

    private void AddCategory()
    {
        ProductCategory? category = ShowCategoryEditor(null);
        if (category is null || !TryApplyCategoryChange(() => _store.AddCategory(category)))
        {
            return;
        }

        RefreshCategories(category.Id);
        _statusLabel.Text = $"Added category {category.Name}.";
    }

    private void EditSelectedCategory()
    {
        string? id = GetSelectedCategoryId();
        if (id is null)
        {
            return;
        }

        ProductCategory? existingCategory = _store.LoadCategories().FirstOrDefault(category =>
            category.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        if (existingCategory is null)
        {
            RefreshCategories();
            return;
        }

        ProductCategory? updatedCategory = ShowCategoryEditor(existingCategory);
        if (updatedCategory is null ||
            !TryApplyCategoryChange(() => _store.UpdateCategory(existingCategory.Id, updatedCategory)))
        {
            return;
        }

        RefreshCategories(updatedCategory.Id);
        _statusLabel.Text = $"Updated category {updatedCategory.Name}.";
    }

    private void DeleteSelectedCategory()
    {
        string? id = GetSelectedCategoryId();
        if (id is null)
        {
            return;
        }

        ProductCategory? category = _store.LoadCategories().FirstOrDefault(item =>
            item.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        if (category is null)
        {
            RefreshCategories();
            return;
        }

        DialogResult confirmation = MessageBox.Show(
            $"Delete the category {category.Name} ({category.Id})?",
            "Confirm category deletion",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);
        if (confirmation != DialogResult.Yes ||
            !TryApplyCategoryChange(() => _store.DeleteCategory(category.Id)))
        {
            return;
        }

        RefreshCategories();
        _statusLabel.Text = $"Deleted category {category.Name}.";
    }

    private ProductCategory? ShowCategoryEditor(ProductCategory? existingCategory)
    {
        using Form dialog = new()
        {
            Text = existingCategory is null ? "Add Category" : "Edit Category",
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            ClientSize = new Size(440, 250),
            MaximizeBox = false,
            MinimizeBox = false,
            ShowInTaskbar = false,
            Font = Font
        };

        TableLayoutPanel layout = new()
        {
            ColumnCount = 2,
            Dock = DockStyle.Fill,
            Padding = new Padding(16),
            RowCount = 5
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        TextBox idInput = new() { Dock = DockStyle.Fill, Text = existingCategory?.Id ?? string.Empty };
        TextBox nameInput = new() { Dock = DockStyle.Fill, Text = existingCategory?.Name ?? string.Empty };
        TextBox iconInput = new() { Dock = DockStyle.Fill, Text = existingCategory?.Icon ?? string.Empty };
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
            Width = 85
        };
        Button saveButton = new()
        {
            Text = "Save",
            Width = 85
        };
        FlowLayoutPanel buttonPanel = new()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false
        };
        buttonPanel.Controls.Add(saveButton);
        buttonPanel.Controls.Add(cancelButton);

        AddEditorRow(layout, 0, "ID:", idInput);
        AddEditorRow(layout, 1, "Name:", nameInput);
        AddEditorRow(layout, 2, "Icon:", iconInput);
        layout.Controls.Add(errorLabel, 0, 3);
        layout.SetColumnSpan(errorLabel, 2);
        layout.Controls.Add(buttonPanel, 0, 4);
        layout.SetColumnSpan(buttonPanel, 2);
        dialog.Controls.Add(layout);
        dialog.AcceptButton = saveButton;
        dialog.CancelButton = cancelButton;

        ProductCategory? result = null;
        saveButton.Click += (_, _) =>
        {
            string id = idInput.Text.Trim();
            string name = nameInput.Text.Trim();
            string icon = iconInput.Text.Trim();
            if (id.Length == 0 || name.Length == 0 || icon.Length == 0)
            {
                errorLabel.Text = "Category ID, name, and icon are required.";
                return;
            }

            result = new ProductCategory { Id = id, Name = name, Icon = icon };
            dialog.DialogResult = DialogResult.OK;
            dialog.Close();
        };

        return dialog.ShowDialog(this) == DialogResult.OK ? result : null;
    }

    private bool TryApplyCategoryChange(Action change)
    {
        try
        {
            change();
            return true;
        }
        catch (Exception exception) when (
            exception is IOException or UnauthorizedAccessException or InvalidDataException or InvalidOperationException)
        {
            MessageBox.Show(exception.Message,
                "Unable to update category", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    private void RefreshCategories(string? selectedId = null)
    {
        _categoryGrid.Rows.Clear();
        foreach (ProductCategory category in _store.LoadCategories())
        {
            int rowIndex = _categoryGrid.Rows.Add(category.Id, category.Name, category.Icon);
            if (category.Id.Equals(selectedId, StringComparison.OrdinalIgnoreCase))
            {
                _categoryGrid.Rows[rowIndex].Selected = true;
                _categoryGrid.CurrentCell = _categoryGrid.Rows[rowIndex].Cells[0];
            }
        }

        if (_categoryGrid.CurrentRow is null && _categoryGrid.Rows.Count > 0)
        {
            _categoryGrid.Rows[0].Selected = true;
            _categoryGrid.CurrentCell = _categoryGrid.Rows[0].Cells[0];
        }

        _statusLabel.Text = $"{_categoryGrid.Rows.Count} category(ies).";
        UpdateSelection();
    }

    private string? GetSelectedCategoryId() =>
        _categoryGrid.CurrentRow?.Cells["categoryIdColumn"].Value as string;

    private void UpdateSelection()
    {
        bool hasSelection = GetSelectedCategoryId() is not null;
        _editButton.Enabled = hasSelection;
        _deleteButton.Enabled = hasSelection;
    }

    private static Button CreateButton(string text, Color color, int width) => new()
    {
        BackColor = color,
        FlatStyle = FlatStyle.Flat,
        ForeColor = Color.White,
        Text = text,
        Width = width
    };

    private static void AddEditorRow(TableLayoutPanel layout, int row, string labelText, Control editor)
    {
        Label label = new()
        {
            Dock = DockStyle.Fill,
            ForeColor = Color.FromArgb(32, 42, 62),
            Text = labelText,
            TextAlign = ContentAlignment.MiddleLeft
        };
        layout.Controls.Add(label, 0, row);
        layout.Controls.Add(editor, 1, row);
    }
}
