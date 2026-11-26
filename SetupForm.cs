using System.Drawing;

namespace POS;

internal sealed class SetupForm : Form
{
    private readonly string _configPath;
    private readonly TextBox _storeNameInput = new();
    private readonly TextBox _branchInput = new();
    private readonly TextBox _adminPasswordInput = new() { UseSystemPasswordChar = true };
    private readonly TextBox _confirmPasswordInput = new() { UseSystemPasswordChar = true };
    private readonly Label _errorLabel = new();

    internal SetupForm(string configPath)
    {
        _configPath = configPath;
        Text = "Set Up MADIES Store POS";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(520, 350);
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;

        TableLayoutPanel layout = new()
        {
            ColumnCount = 2,
            Dock = DockStyle.Fill,
            Padding = new Padding(22),
            RowCount = 8
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        for (int i = 0; i < 4; i++)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        }
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        Label titleLabel = new()
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 15F, FontStyle.Bold),
            ForeColor = Color.FromArgb(32, 42, 62),
            Text = "Welcome - configure this store",
            TextAlign = ContentAlignment.MiddleLeft
        };
        Label subtitleLabel = new()
        {
            Dock = DockStyle.Fill,
            ForeColor = Color.FromArgb(84, 96, 118),
            Text = "Enter the store details and create an administrator password.",
            TextAlign = ContentAlignment.MiddleLeft
        };

        AddField(layout, 2, "Store name:", _storeNameInput);
        AddField(layout, 3, "Branch:", _branchInput);
        AddField(layout, 4, "Admin password:", _adminPasswordInput);
        AddField(layout, 5, "Confirm password:", _confirmPasswordInput);

        _errorLabel.Dock = DockStyle.Fill;
        _errorLabel.ForeColor = Color.FromArgb(160, 32, 32);
        _errorLabel.TextAlign = ContentAlignment.MiddleLeft;

        Button cancelButton = new()
        {
            DialogResult = DialogResult.Cancel,
            Text = "Cancel",
            Width = 90
        };
        Button saveButton = new()
        {
            Text = "Save",
            Width = 90
        };
        saveButton.Click += SaveConfiguration;

        FlowLayoutPanel buttonPanel = new()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false
        };
        buttonPanel.Controls.Add(saveButton);
        buttonPanel.Controls.Add(cancelButton);

        layout.Controls.Add(titleLabel, 0, 0);
        layout.SetColumnSpan(titleLabel, 2);
        layout.Controls.Add(subtitleLabel, 0, 1);
        layout.SetColumnSpan(subtitleLabel, 2);
        layout.Controls.Add(_errorLabel, 0, 6);
        layout.SetColumnSpan(_errorLabel, 2);
        layout.Controls.Add(buttonPanel, 0, 7);
        layout.SetColumnSpan(buttonPanel, 2);
        Controls.Add(layout);

        AcceptButton = saveButton;
        CancelButton = cancelButton;
        Shown += (_, _) => _storeNameInput.Focus();
    }

    private static void AddField(TableLayoutPanel layout, int row, string labelText, TextBox input)
    {
        Label label = new()
        {
            Dock = DockStyle.Fill,
            ForeColor = Color.FromArgb(32, 42, 62),
            Text = labelText,
            TextAlign = ContentAlignment.MiddleLeft
        };
        input.Dock = DockStyle.Fill;
        input.Margin = new Padding(0, 8, 0, 8);
        layout.Controls.Add(label, 0, row);
        layout.Controls.Add(input, 1, row);
    }

    private void SaveConfiguration(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_storeNameInput.Text) ||
            string.IsNullOrWhiteSpace(_branchInput.Text) ||
            string.IsNullOrWhiteSpace(_adminPasswordInput.Text))
        {
            _errorLabel.Text = "Complete all fields before saving.";
            return;
        }

        if (!string.Equals(_adminPasswordInput.Text, _confirmPasswordInput.Text, StringComparison.Ordinal))
        {
            _errorLabel.Text = "The administrator passwords do not match.";
            return;
        }

        ApplicationConfig configuration = new(
            _adminPasswordInput.Text,
            _storeNameInput.Text.Trim(),
            _branchInput.Text.Trim());

        try
        {
            ApplicationConfig.Save(_configPath, configuration);
        }
        catch (Exception exception) when (
            exception is IOException or
            UnauthorizedAccessException or
            InvalidDataException)
        {
            _errorLabel.Text = $"Unable to save application_config: {exception.Message}";
            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }
}
