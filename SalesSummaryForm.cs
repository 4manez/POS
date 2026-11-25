using System.ComponentModel;
using System.Drawing.Printing;
using System.Globalization;

namespace POS;

internal partial class SalesSummaryForm : Form
{
    private readonly CsvDataStore _store;
    private DailySalesSummary? _currentSummary;
    private int _currentPrintCount;

    internal SalesSummaryForm(CsvDataStore store)
    {
        InitializeComponent();
        _store = store;
        _datePicker.ValueChanged += (_, _) => LoadSummary();
        _printButton.Click += (_, _) => PrintSummary();
        LoadSummary();
    }

    private void LoadSummary()
    {
        try
        {
            DailySalesSummary summary = _store.LoadDailySalesSummary(_datePicker.Value.Date);
            int printCount = _store.LoadSalesSummaryPrintCount(_datePicker.Value.Date);
            _currentSummary = summary;
            _currentPrintCount = printCount;
            _printCountLabel.Text = $"PRINT COUNT: {printCount.ToString("N0", CultureInfo.CurrentCulture)}";
            _printButton.Enabled = true;
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

    private void PrintSummary()
    {
        DailySalesSummary? summary = _currentSummary;
        if (summary is null)
        {
            return;
        }

        using PrintDocument document = new()
        {
            DocumentName = $"Sales Summary {summary.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}"
        };
        using PrintDialog printDialog = new()
        {
            Document = document,
            UseEXDialog = true
        };

        int productIndex = 0;
        int printCount = _currentPrintCount;
        bool firstPage = true;
        document.PrintPage += (_, eventArgs) =>
        {
            productIndex = DrawSummaryPage(
                eventArgs.Graphics, eventArgs.MarginBounds, summary, productIndex, firstPage, printCount);
            eventArgs.HasMorePages = productIndex < summary.Products.Count;
            firstPage = false;
        };

        try
        {
            if (printDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            printCount = _store.IncrementSalesSummaryPrintCount(summary.Date);
            _currentPrintCount = printCount;
            _printCountLabel.Text = $"PRINT COUNT: {printCount.ToString("N0", CultureInfo.CurrentCulture)}";
            document.Print();
        }
        catch (IOException exception)
        {
            ShowPrintError(exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            ShowPrintError(exception);
        }
        catch (InvalidDataException exception)
        {
            ShowPrintError(exception);
        }
        catch (FormatException exception)
        {
            ShowPrintError(exception);
        }
        catch (OverflowException exception)
        {
            ShowPrintError(exception);
        }
        catch (InvalidPrinterException exception)
        {
            ShowPrintError(exception);
        }
        catch (Win32Exception exception)
        {
            ShowPrintError(exception);
        }
        catch (InvalidOperationException exception)
        {
            ShowPrintError(exception);
        }
    }

    private static int DrawSummaryPage(
        Graphics? graphics,
        Rectangle marginBounds,
        DailySalesSummary summary,
        int productIndex,
        bool firstPage,
        int printCount)
    {
        Graphics pageGraphics = graphics ??
            throw new InvalidOperationException("The printer did not provide a drawing surface.");
        using Font titleFont = new("Segoe UI", 18F, FontStyle.Bold);
        using Font bodyFont = new("Segoe UI", 10F);
        using Font columnFont = new("Segoe UI", 10F, FontStyle.Bold);
        using Brush textBrush = new SolidBrush(Color.Black);
        using Brush headerBrush = new SolidBrush(Color.FromArgb(232, 237, 245));
        using Pen linePen = new(Color.FromArgb(180, 190, 205));
        using StringFormat leftFormat = new()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center,
            Trimming = StringTrimming.EllipsisCharacter
        };
        using StringFormat rightFormat = new()
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Center,
            Trimming = StringTrimming.EllipsisCharacter
        };

        float left = marginBounds.Left;
        float width = marginBounds.Width;
        float y = marginBounds.Top;
        pageGraphics.DrawString(firstPage ? "Sales Summary" : "Sales Summary (continued)",
            titleFont, textBrush, left, y);
        float titleHeight = titleFont.GetHeight(pageGraphics) + 8;
        pageGraphics.DrawString(
            $"PRINT COUNT: {printCount.ToString("N0", CultureInfo.CurrentCulture)}",
            bodyFont,
            textBrush,
            new RectangleF(left, y, width, titleHeight),
            rightFormat);
        y += titleHeight;
        pageGraphics.DrawString(summary.Date.ToString("D", CultureInfo.CurrentCulture),
            bodyFont, textBrush, left, y);
        y += bodyFont.GetHeight(pageGraphics) + 12;

        if (firstPage)
        {
            string[] metrics =
            [
                $"Total sales: {summary.SalesTotal.ToString("C2", CultureInfo.CurrentCulture)}",
                $"Transactions: {summary.TransactionCount.ToString("N0", CultureInfo.CurrentCulture)}",
                $"Items sold: {summary.ItemsSold.ToString("N0", CultureInfo.CurrentCulture)}",
                $"Cash received: {summary.CashReceived.ToString("C2", CultureInfo.CurrentCulture)}",
                $"Change given: {summary.Change.ToString("C2", CultureInfo.CurrentCulture)}"
            ];
            float metricWidth = width / 2;
            float metricHeight = bodyFont.GetHeight(pageGraphics) + 5;
            for (int i = 0; i < metrics.Length; i++)
            {
                float x = left + (i % 2) * metricWidth;
                float metricY = y + (i / 2) * metricHeight;
                pageGraphics.DrawString(metrics[i], bodyFont, textBrush,
                    new RectangleF(x, metricY, metricWidth, metricHeight), leftFormat);
            }

            y += 3 * metricHeight + 10;
        }

        float rowHeight = bodyFont.GetHeight(pageGraphics) + 10;
        float barcodeWidth = width * 0.22F;
        float productWidth = width * 0.42F;
        float quantityWidth = width * 0.16F;
        float revenueWidth = width - barcodeWidth - productWidth - quantityWidth;
        pageGraphics.FillRectangle(headerBrush, left, y, width, rowHeight);
        pageGraphics.DrawString("Barcode", columnFont, textBrush,
            new RectangleF(left + 4, y, barcodeWidth - 8, rowHeight), leftFormat);
        pageGraphics.DrawString("Product", columnFont, textBrush,
            new RectangleF(left + barcodeWidth + 4, y, productWidth - 8, rowHeight), leftFormat);
        pageGraphics.DrawString("Units Sold", columnFont, textBrush,
            new RectangleF(left + barcodeWidth + productWidth + 4, y, quantityWidth - 8, rowHeight), rightFormat);
        pageGraphics.DrawString("Revenue", columnFont, textBrush,
            new RectangleF(left + barcodeWidth + productWidth + quantityWidth + 4, y, revenueWidth - 8, rowHeight), rightFormat);
        y += rowHeight;

        if (summary.Products.Count == 0)
        {
            pageGraphics.DrawString("No product sales recorded.", bodyFont, textBrush, left + 4, y + 4);
            return productIndex;
        }

        float maxY = marginBounds.Bottom;
        int firstProductIndex = productIndex;
        while (productIndex < summary.Products.Count &&
               (productIndex == firstProductIndex || y + rowHeight <= maxY))
        {
            ProductSalesSummary product = summary.Products[productIndex];
            pageGraphics.DrawString(product.Barcode, bodyFont, textBrush,
                new RectangleF(left + 4, y, barcodeWidth - 8, rowHeight), leftFormat);
            pageGraphics.DrawString(product.ProductName, bodyFont, textBrush,
                new RectangleF(left + barcodeWidth + 4, y, productWidth - 8, rowHeight), leftFormat);
            pageGraphics.DrawString(product.QuantitySold.ToString("N0", CultureInfo.CurrentCulture), bodyFont, textBrush,
                new RectangleF(left + barcodeWidth + productWidth + 4, y, quantityWidth - 8, rowHeight), rightFormat);
            pageGraphics.DrawString(product.Revenue.ToString("C2", CultureInfo.CurrentCulture), bodyFont, textBrush,
                new RectangleF(left + barcodeWidth + productWidth + quantityWidth + 4, y, revenueWidth - 8, rowHeight), rightFormat);
            y += rowHeight;
            pageGraphics.DrawLine(linePen, left, y, left + width, y);
            productIndex++;
        }

        return productIndex;
    }

    private void ShowPrintError(Exception exception)
    {
        MessageBox.Show($"The sales summary could not be printed.\n\n{exception.Message}",
            "Unable to print sales summary", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void ShowLoadError(Exception exception)
    {
        _currentSummary = null;
        _currentPrintCount = 0;
        _printCountLabel.Text = "PRINT COUNT: —";
        _printButton.Enabled = false;
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
