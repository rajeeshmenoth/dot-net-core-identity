# ASP.NET Core 5.1
The ASP.Net Core 5.1 template have Open API ( Swagger ) selection option in the template window.

# Project Structure

1. Grocery API -> API implementation details.
2. GroceryAppStore -> Authentication
3. Repository -> Code First data base implementation using Entity Framework Core ( Version 5.0.3 ).

## Caching
Caching is a technique of storing frequently used data or information in a local memory for a certain time period.

## Packages

The following packages are used in the project development.

### Microsoft.AspNetCore.ResponseCaching
Microsoft AspNetCore currently support only Response Caching. All the caching is happening in proxy server and here we can use Microsoft.AspNetCore.ResponseCaching Nuget package for the Response Caching implementation in ASP.Net Core.

### Microsoft.EntityFrameworkCore

Entity Framework Core is a modern object-database mapper for . NET. It supports LINQ queries, change tracking, updates, and schema migrations. EF Core works with SQL Server, Azure SQL Database, SQLite, Azure Cosmos DB, MySQL, PostgreSQL, and other databases through a provider plugin API. [Nuget](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore).

### Microsoft.EntityFrameworkCore.InMemory
In memory database is using for testing purpose.

## Output
![swagger-output](https://github.com/rajeeshmenoth/dot-net-core-web-api/blob/main/GroceryAPI/GroceryAPI/Grocery.JPG?raw=true)

Note: The application in development mode.
