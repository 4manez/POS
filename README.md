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

## Release publishing

From Git Bash, run the CLI with `major`, `minor`, or `patch` to increment the project version and publish a self-contained Windows x64 executable:

```bash
bash ./madiescli publish release minor
```

## Using the app

- Search by product name or scan a barcode. Press Enter to add an exact barcode match to the cart.
- Open **Inventory** to search product details, add or remove stock, add, edit, or delete items and categories, and set a low-stock threshold for the selected item. Product names are saved in uppercase. Editing an item changes its barcode, name, category, or price while preserving its stock and threshold. Stock counts are saved in `Data/stocks.csv` and thresholds in `Data/products.csv`; items at or below their threshold are highlighted red. Blank stock and threshold values for new items default to 0 and 5. Categories cannot be deleted while products are assigned to them.
- On first launch, the setup form asks for the store name, branch, and a new administrator password. These settings are saved in a plain-text `application_config` file beside the application executable (or in the current directory if an existing config file is there). The file is excluded from Git; do not distribute it with the application.
- Adding or removing stock, creating or deleting items, and managing categories requires the administrator password. Existing `application_config` files containing only `<admincreds>your-password</admincreds>` remain supported.
- Each app launch prompts for a cashier name. Click the cashier field in the header to log out and enter another cashier. The active name is held in memory and isn't saved in `application_config`; completed sale lines and the sales summary record the cashier.
- Open **Sales Summary** to review and print daily sales totals and product-level units and revenue.
- Enter the cash received and press Enter to complete a sale.

## CSV data

- `Data/categories.csv` contains category IDs, names, and icons.
- `Data/products.csv` contains product barcodes, names, category IDs, prices, optional `image_filename` values, and low-stock thresholds. Image values resolve to `res/items/<filename>.png`; blank or missing image files use `res/items/missing.png`.
- `Data/stocks.csv` contains each product's barcode and stock count. The barcode connects each stock record to its product.
- Sales are stored by date in `Data/Sales/sales_YYYYMMDD.csv`. Each row records a sold product line; the file is created when needed.
- `Data/Sales/sales_summary_print_counts.csv` stores the persistent print count for each sales-summary date.

Keep category IDs in `products.csv` aligned with IDs in `categories.csv`. Product barcodes must be unique. Optional product images belong in `res`; see [res/README.txt](res/README.txt).

The included product barcodes are sample values. Replace them with the actual item barcodes before using a barcode scanner with store inventory.
