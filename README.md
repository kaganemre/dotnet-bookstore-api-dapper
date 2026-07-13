# BookStore API

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=white)
![Dapper](https://img.shields.io/badge/Dapper-2.1.79-ff69b4)
![Bogus](https://img.shields.io/badge/Bogus-35.6.5-orange)
![Scalar](https://img.shields.io/badge/Scalar-API%20Docs-000000)
![License](https://img.shields.io/badge/License-MIT-yellow)

A RESTful bookstore API built with **ASP.NET Core 10 Minimal API**, **Dapper**, and **PostgreSQL**, following **N-Layer Architecture** principles. It features full CRUD operations, a CLI-driven database seeder, and interactive OpenAPI documentation via Scalar.

## Architecture

This project follows N-Layer Architecture with strict layer separation. Each layer has a single responsibility and only depends on the layer directly below it.

```
┌─────────────────────────────────────┐
│           BookStoreApi.Api          │  Minimal API endpoints, DI registration
│     (Endpoints, Extensions)         │
└────────────────┬────────────────────┘
                 │ depends on
┌────────────────▼────────────────────┐
│        BookStoreApi.Business        │  Business logic, Entity → DTO mapping
│     (IBookService, BookService)     │
└────────────────┬────────────────────┘
                 │ depends on
┌────────────────▼────────────────────┐
│        BookStoreApi.DataAccess      │  Dapper queries, connection management
│  (IBookRepository, BookRepository,  │
│   IDbConnectionFactory, Seeder)     │
└──────────┬─────────────┬────────────┘
           │             │
┌──────────▼───┐  ┌──────▼──────────────┐
│BookStoreApi  │  │  BookStoreApi.Shared │
│  .Entities   │  │  (DTOs: Request /   │
│ (Book entity)│  │   Response records) │
└──────────────┘  └─────────────────────┘
```

**Key design decisions:**

- `DataAccess` returns `Book` entities — it has no knowledge of DTOs
- `Business` handles all Entity → DTO mapping, keeping the API layer clean
- `Api` registers concrete implementations via extension methods, but only references `Business` — not `DataAccess` directly
- `Shared` is a dependency-free project containing only DTO records, usable across layers without creating circular references
- Dapper maps SQL results directly to `Book` entities; the `Entities` project is retained to support future domain logic

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 10 Minimal API |
| Data Access | Dapper + Npgsql |
| Database | PostgreSQL (Supabase) |
| Seed Data | Bogus + SemaphoreSlim |
| API Docs | Scalar + Microsoft.AspNetCore.OpenApi |

## Project Structure

```
src/
├── BookStoreApi.Api/
│   ├── Endpoints/          # Minimal API route definitions
│   ├── Extensions/         # DI registration, database seeding CLI
│   └── Program.cs
├── BookStoreApi.Business/
│   ├── Interfaces/         # IBookService
│   └── Services/           # BookService (mapping + orchestration)
├── BookStoreApi.DataAccess/
│   ├── Context/            # IDbConnectionFactory, DbConnectionFactory
│   ├── Extensions/         # DataAccessServiceExtensions
│   ├── Repositories/       # IBookRepository, BookRepository
│   └── Seed/               # IDatabaseSeeder, DatabaseSeeder
├── BookStoreApi.Entities/
│   └── Book.cs             # Domain entity
└── BookStoreApi.Shared/
    └── Dtos/               # CreateBookRequest, UpdateBookRequest, BookResponse, CreatedResponse
```

## Features

- Full **CRUD** for books (`GET`, `POST`, `PUT`, `DELETE`)
- **N-Layer Architecture** with strict layer boundaries
- **Repository Pattern** with `IBookRepository` abstraction
- **`IDbConnectionFactory`** for safe, reusable connection management
- **`IDatabaseSeeder`** interface — concrete implementation hidden from the API layer
- **CLI database seeder** — run with `--seed` flag; generates realistic fake data via Bogus
  - Concurrent batch inserts controlled via `SemaphoreSlim(10)`
- **`CancellationToken` support** on all async operations
- **Scalar UI** for interactive API exploration (Development only)
- **OpenAPI** schema generation via native ASP.NET Core 10 support
- **Nullable reference types** enabled throughout

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A PostgreSQL database (e.g. [Supabase](https://supabase.com) free tier)

### Configuration

Update `src/BookStoreApi.Api/appsettings.json` with your PostgreSQL connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=...;Database=...;Username=...;Password=..."
  }
}
```

### Run the API

```bash
dotnet run --project src/BookStoreApi.Api
```

The API will be available at `https://localhost:{port}`. In Development mode, Scalar UI is accessible at `/scalar`.

### Seed the Database

```bash
dotnet run --project src/BookStoreApi.Api -- --seed
```

Generates 1,000 realistic book records using Bogus and inserts them in concurrent batches. Exits with code `0` on success, `1` on failure.

## API Endpoints

| Method | Route | Description |
|---|---|---|
| `GET` | `/api/books` | Retrieve all books |
| `GET` | `/api/books/{id}` | Retrieve a book by ID |
| `POST` | `/api/books` | Create a new book |
| `PUT` | `/api/books/{id}` | Update an existing book |
| `DELETE` | `/api/books/{id}` | Delete a book |

## Licence

This project is licensed under the [MIT License](LICENSE).