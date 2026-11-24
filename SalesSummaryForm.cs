using System.Globalization;

namespace POS;

internal partial class SalesSummaryForm : Form
{
    private readonly CsvDataStore _store;

    internal SalesSummaryForm(CsvDataStore store)
    {
        InitializeComponent();
        _store = store;
        _datePicker.ValueChanged += (_, _) => LoadSummary();
        LoadSummary();
    }

    private void LoadSummary()
    {
        try
        {
            DailySalesSummary summary = _store.LoadDailySalesSummary(_datePicker.Value.Date);
            _salesValue.Text = summary.SalesTotal.ToString("C2", CultureInfo.CurrentCulture);
            _transactionsValue.Text = summary.TransactionCount.ToString("N0", CultureInfo.CurrentCulture);
            _itemsValue.Text = summary.ItemsSold.ToString("N0", CultureInfo.CurrentCulture);
            _cashValue.Text = summary.CashReceived.ToString("C2", CultureInfo.CurrentCulture);

            _salesGrid.Rows.Clear();
            foreach (ProductSalesSummary product in summary.Products)
            {
                _salesGrid.Rows.Add(
                    product.Barcode,
                    product.ProductName,
                    product.QuantitySold,
                    product.Revenue.ToString("C2", CultureInfo.CurrentCulture));
            }

            _statusLabel.Text = summary.TransactionCount == 0
                ? "No sales were recorded on this date."
                : $"Change given: {summary.Change.ToString("C2", CultureInfo.CurrentCulture)}";
        }
        catch (IOException exception)
        {
            ShowLoadError(exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            ShowLoadError(exception);
        }
        catch (InvalidDataException exception)
        {
            ShowLoadError(exception);
        }
        catch (FormatException exception)
        {
            ShowLoadError(exception);
        }
        catch (OverflowException exception)
        {
            ShowLoadError(exception);
        }
    }

    private void ShowLoadError(Exception exception)
    {
        _salesValue.Text = "—";
        _transactionsValue.Text = "—";
        _itemsValue.Text = "—";
        _cashValue.Text = "—";
        _salesGrid.Rows.Clear();
        _statusLabel.Text = "Unable to load this day's sales.";
        MessageBox.Show($"The sales summary could not be loaded.\n\n{exception.Message}",
            "Unable to load sales summary", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
