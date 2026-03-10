# Course Project OOP 3 - Sale-Alert

## Project Overview

This is a C# Windows Forms application that tracks product prices by integrating with an external API (appears to be AliExpress or similar e-commerce platform). The application stores product information and price history in a SQL Server database, allowing users to monitor price changes over time.

## Features

- **Add New Products**: Users can add products by entering a 12-digit product ID
- **Automatic Price Fetching**: Fetch current prices from the external API for all products in the database
- **Price History Tracking**: Maintains a complete history of product prices with timestamps
- **Database Management**: Clear all data from the database when needed
- **User-Friendly GUI**: Windows Forms interface with status messages and feedback

## Project Structure

### Core Components

#### **Models**
- **Product.cs** - Represents a product with:
  - `Id` - 12-digit product identifier
  - `Name` - Product name
  - `RecordNum` - Auto-increment primary key
  - `Prices` - Collection of historical price records

- **PriceHistory.cs** - Tracks price changes with:
  - `Id` - Auto-increment record ID
  - `CurrentPrice` - Decimal price value
  - `AddedDate` - Timestamp when price was recorded
  - `ProductId` - Foreign key linking to Product

- **PrivateInfo.cs** - Contains sensitive credentials:
  - API credentials (clientId, clientSecret)

#### **Database**
- **AppDbContext.cs** - Entity Framework Core context that:
  - Manages connection to SQL Server (Products database)
  - Defines database tables (Products, PriceHistories)
  - Uses integrated security authentication

#### **UI & Logic**
- **Form1.cs** - Main application window containing:
  - **AddNewItemButton** - Adds new product to database via API
  - **ProcessALLItemsInListButton** - Fetches prices for all stored products
  - **ClearAllTablesButton** - Clears all data from database

- **DataCollection.cs** - API integration methods:
  - `GetAccessToken()` - Authenticates with external API
  - `GetAllItemsFromList()` - Retrieves product details and prices
  - `GetPhonePrice()` - Main workflow method

## How It Works

### 1. **Add New Product**
   - User enters a 12-digit product ID in the text box
   - Application validates input (must be digits only)
   - Calls API to fetch product name and current price
   - Stores product in `Products` table
   - Records initial price in `PriceHistories` table

### 2. **Update Prices**
   - "Process All Items" button iterates through all stored products
   - Fetches current price from API for each product
   - Creates new price records in `PriceHistories` with current timestamp
   - Status message confirms successful update

### 3. **View History**
   - `PriceHistories` table contains all price changes with dates
   - Can analyze price trends by querying this table

### 4. **Clear Data**
   - Deletes all records from both tables
   - Resets auto-increment counters to 1

## Database Schema

### Products Table
```
RecordNum (int, auto-increment, PK)
Id (string, unique, 12 digits)
Name (string)
```

### PriceHistories Table
```
Id (int, auto-increment, PK)
CurrentPrice (decimal)
AddedDate (DateTime)
ProductId (string, FK)
```

## Technology Stack

- **Language**: C# (.NET 9.0)
- **UI Framework**: Windows Forms
- **Database**: SQL Server Express (LocalDB)
- **ORM**: Entity Framework Core
- **Web Client**: HttpClient for API calls
- **Playwright**: Browser automation (referenced but not actively used)

## Prerequisites

- .NET 9.0 or later
- SQL Server Express
- API credentials (clientId, clientSecret) - stored in `PrivateInfo.cs`

## Configuration

Update the connection string in `AppDbContext.cs` if your SQL Server instance differs:
```csharp
Server=your-server-name\sqlexpress;Database=Products;Integrated Security=True;
```

## Notes

- The application uses asynchronous operations for API calls
- Database is automatically created on first run
- Input validation ensures only valid 12-digit product IDs are processed
- Price data is stored with precise timestamps for historical analysis
