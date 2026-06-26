# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**YaLlegaBack** is an ASP.NET Core 8.0 REST API backend for a food delivery platform called "YaLlega". It serves an Angular frontend running on `localhost:4200`.

## Common Commands

```bash
# Build
dotnet build

# Run (HTTP, port 5198 — Swagger UI opens automatically in dev mode)
dotnet run

# Run with HTTPS profile
dotnet run --launch-profile https

# Apply EF Core migrations
dotnet ef database update

# Add a new migration
dotnet ef migrations add <MigrationName>
```

There are no test projects in this repository.

## Architecture

The project follows a strict 3-layer pattern:

```
Controllers → Services → Repositories → EF Core DbContext → SQLite (YaLlegaBack.db)
```

**Domain entities** (`/Entities`): `User`, `Restaurant`, `Category`, `Product`, `Cart`

**Key relationships:**
- `User` ↔ `Restaurant` — 1:1 with cascade delete
- `Restaurant` → `Category` — 1:many
- `Category` ↔ `Product` — many:many
- `Cart` ↔ `Product` — many:many

**DTOs** live in `/Models`. `ServiceResult` is the generic wrapper returned from all services to controllers.

## Authentication

JWT Bearer authentication using HS256. Config lives in `appsettings.json` under `Authentication` (key, issuer, audience). Tokens expire after 1 hour and carry the user ID as the `sub` claim. The `AuthenticationController` handles token issuance.

## Key Configuration

- **Connection string**: `appsettings.json` → `ConnectionStrings.YaLlegaBackDBConnectionString` → `YaLlegaBack.db` (SQLite file at project root)
- **CORS**: Hardcoded to allow `http://localhost:4200`
- **Swagger UI**: Available at `/swagger` in Development mode
- **DI registration**: All services and repositories are registered in `Program.cs`
