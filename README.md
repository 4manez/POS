# MADIES Store POS

A Windows Forms point-of-sale application backed by CSV files.

## Requirements

- Windows
- .NET 10 SDK

## Run

From the project directory:

```powershell
dotnet run --project POS.csproj
```

## Using the app

- Search by product name or scan a barcode. Press Enter to add an exact barcode match to the cart.
- Open **Inventory** to search product details, add or remove stock, add, edit, or delete items and categories, and set a low-stock threshold for the selected item. Product names are saved in uppercase. Editing an item changes its barcode, name, category, or price while preserving its stock and threshold. Stock counts are saved in `Data/stocks.csv` and thresholds in `Data/products.csv`; items at or below their threshold are highlighted red. Blank stock and threshold values for new items default to 0 and 5. Categories cannot be deleted while products are assigned to them.
- Adding or removing stock, creating or deleting items, and managing categories requires the administrator password in a plain-text `application_config` file formatted as `<admincreds>your-password</admincreds>`. Replace `your-password` with your chosen password. The file is excluded from Git; create it in the project directory before running locally, or beside the published executable after publishing. Do not distribute the file with the application.
- Open **Sales Summary** to review and print daily sales totals and product-level units and revenue.
- Enter the cash received and press Enter to complete a sale.

## CSV data

- `Data/categories.csv` contains category IDs, names, and icons.
- `Data/products.csv` contains product barcodes, names, category IDs, prices, icons, optional `image_filename` values, and low-stock thresholds. Image values resolve to `res/items/<filename>.png`.
- `Data/stocks.csv` contains each product's barcode and stock count. The barcode connects each stock record to its product.
- Sales are stored by date in `Data/Sales/sales_YYYYMMDD.csv`. Each row records a sold product line; the file is created when needed.
- `Data/Sales/sales_summary_print_counts.csv` stores the persistent print count for each sales-summary date.

Keep category IDs in `products.csv` aligned with IDs in `categories.csv`. Product barcodes must be unique. Optional product images belong in `res`; see [res/README.txt](res/README.txt).

The included product barcodes are sample values. Replace them with the actual item barcodes before using a barcode scanner with store inventory.
