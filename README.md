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
- Open **Inventory** to search product details and add or remove stock for a selected item.
- Enter the cash received and press Enter to complete a sale.

## CSV data

- `Data/categories.csv` contains category IDs, names, and icons.
- `Data/products.csv` contains product barcodes, names, category IDs, prices, stock counts, icons, and optional image filenames.
- Sales are stored by date in `Data/Sales/sales_YYYYMMDD.csv`. Each row records a sold product line; the file is created when needed.

Keep category IDs in `products.csv` aligned with IDs in `categories.csv`. Product barcodes must be unique. Optional product images belong in `res`; see [res/README.txt](res/README.txt).

The included product barcodes are sample values. Replace them with the actual item barcodes before using a barcode scanner with store inventory.
