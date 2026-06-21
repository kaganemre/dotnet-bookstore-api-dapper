# BookStore API

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=white)
![Dapper](https://img.shields.io/badge/Dapper-ORM-ff69b4)
![Bogus](https://img.shields.io/badge/Bogus-Fake%20Data-orange)
![Scalar](https://img.shields.io/badge/Scalar-API%20Docs-000000)
![License](https://img.shields.io/badge/License-MIT-yellow)

A RESTful bookstore API built with **ASP.NET Core 10 Minimal API**, **Dapper**, and **PostgreSQL**. It features a CLI-driven database seeder, interactive OpenAPI documentation, and full CRUD operations over a books catalogue.

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 10 Minimal API |
| ORM | Dapper |
| Database | PostgreSQL via Npgsql |
| Seed Data | Bogus (fake data generation) |
| API Docs | Scalar + Microsoft.AspNetCore.OpenApi |

## Project Structure

```
BookStoreApi/
├── Data/               # IDbConnectionFactory & DatabaseSeeder (Bogus + SemaphoreSlim)
├── Dtos/               # Request/response record types
├── Endpoints/          # Minimal API endpoint definitions (MapBookEndpoints)
├── Entities/           # Book entity
├── Extensions/         # App extension methods (seed CLI handling)
├── Repositories/       # IBookRepository & BookRepository
├── Program.cs          # Application entry point & DI registrations
└── appsettings.json    # Configuration (connection string, etc.)
```

## Features

- Full **CRUD** for books (`GET`, `POST`, `PUT`, `DELETE`)
- **Repository Pattern** with `IBookRepository` abstraction
- **`IDbConnectionFactory`** singleton for safe, efficient connection management
- **CLI database seeder** — run with `--seed` flag using Bogus for realistic fake data
  - Concurrent batch inserts controlled via `SemaphoreSlim`
- **`CancellationToken` support** on all async endpoints
- **Scalar UI** for interactive API exploration (Development only)
- **OpenAPI** schema generation via native ASP.NET Core 10 support
- **Nullable reference types** and `ImplicitUsings` enabled throughout

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A PostgreSQL database (e.g. [Supabase](https://supabase.com) free tier)

### Configuration

Copy the connection string for your PostgreSQL database and update `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=...;Database=...;Username=...;Password=..."
  }
}
```

### Run the API

```bash
dotnet run
```

The API will be available at `https://localhost:{port}`. In Development mode, Scalar UI is accessible at `/scalar`.

### Seed the Database

```bash
dotnet run -- --seed
```

This uses the **Bogus** library to generate realistic book records and inserts them in concurrent batches. The process exits with code `0` on success and `1` on failure.

## API Endpoints

| Method | Route | Description |
|---|---|---|
| `GET` | `/api/books` | Retrieve all books |
| `GET` | `/api/books/{id}` | Retrieve a book by ID |
| `POST` | `/api/books` | Create a new book |
| `PUT` | `/api/books/{id}` | Update an existing book |
| `DELETE` | `/api/books/{id}` | Delete a book |

## Dependencies

```xml
<PackageReference Include="Bogus" Version="35.6.5" />
<PackageReference Include="Dapper" Version="2.1.79" />
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.1" />
<PackageReference Include="Npgsql" Version="10.0.3" />
<PackageReference Include="Scalar.AspNetCore" Version="2.16.3" />
```

## Licence

This project is licensed under the [MIT License](LICENSE).