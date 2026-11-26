using System.Drawing;

namespace POS;

internal sealed class CashierDialog : Form
{
    private readonly TextBox _nameInput = new();
    private readonly Label _errorLabel = new();

    private CashierDialog()
    {
        Text = "Cashier Login";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(390, 160);
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;

        Label prompt = new()
        {
            AutoSize = true,
            Location = new Point(18, 18),
            Text = "Enter the cashier name:"
        };
        _nameInput.Location = new Point(18, 48);
        _nameInput.Size = new Size(354, 27);

        _errorLabel.AutoSize = true;
        _errorLabel.ForeColor = Color.FromArgb(160, 32, 32);
        _errorLabel.Location = new Point(18, 82);

        Button cancelButton = new()
        {
            DialogResult = DialogResult.Cancel,
            Location = new Point(192, 116),
            Size = new Size(85, 28),
            Text = "Cancel"
        };
        Button loginButton = new()
        {
            Location = new Point(287, 116),
            Size = new Size(85, 28),
            Text = "Continue"
        };
        loginButton.Click += (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(_nameInput.Text))
            {
                _errorLabel.Text = "Enter a cashier name to continue.";
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        };

        Controls.AddRange([prompt, _nameInput, _errorLabel, cancelButton, loginButton]);
        AcceptButton = loginButton;
        CancelButton = cancelButton;
        Shown += (_, _) => _nameInput.Focus();
    }

    internal string CashierName => _nameInput.Text.Trim();

    internal static string? Prompt(IWin32Window? owner = null)
    {
        using CashierDialog dialog = new();
        return dialog.ShowDialog(owner) == DialogResult.OK
            ? dialog.CashierName
            : null;
    }
}
