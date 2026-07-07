# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Layout
The solution file (`StudentIndex.sln`) and `global.json` live at the repo root. `ClientApp/` (Angular) and `StudentIndex.Server/` (ASP.NET) are siblings — never nest one inside the other.

## Commands

### Backend (StudentIndex.Server)
```bash
dotnet build    # or: dotnet build StudentIndex.sln from the repo root
dotnet run
```
The backend starts on `https://localhost:7185`. In development it proxies Angular via SPA middleware.

### Frontend (ClientApp)
```bash
npm install
npm start        # ng serve on http://localhost:4200
npm run build    # production build → dist/client-app/browser
npm test         # Karma/Jasmine unit tests
```

### Database Migrations
```bash
dotnet ef migrations add <Name>
dotnet ef database update
```
Migrations live in `Infrastructure/Migrations/`. The DbContext is `StudentAplikacijaContext`.

## Architecture

### Stack
- **Backend**: ASP.NET Core 8.0, EF Core 9, ASP.NET Identity, JWT Bearer
- **Frontend**: Angular 17 standalone components (no NgModules), RxJS, `@auth0/angular-jwt`
- **Database**: SQL Server (`StudentAplikacija` database, Windows Auth)

### Backend Layers
```
WebApi/          → Controllers (thin, no try/catch) + GlobalExceptionHandler
Application/     → Services (business logic) + Interfaces + DTOs/ + Exceptions/
Infrastructure/  → Repositories (EF Core queries) + Data/ + Identity/
Domain/          → Entities (Etities/) and Constants/
Extensions/      → ServiceCollectionExtensions.cs (all DI registration lives here),
                   ClaimsPrincipalExtensions (User.GetStudentId()), WebApplicationExtensions (role seeding)
```

Request flow: `Controller → IService → IRepository → StudentAplikacijaContext → SQL Server`

All services and repositories are registered as `AddScoped` in `Extensions/ServiceCollectionExtensions.cs`. **Do not add registrations directly in `Program.cs`** — use the extension methods there (`AddDatabase`, `AddJwtAuthentication`, `AddApplicationServices`, `AddCorsPolicy`, `AddSwaggerWithJwt`).

### Error Handling
Services throw typed exceptions from `Application/Exceptions/` (`NotFoundException` → 404, `ConflictException` → 409, `UnauthorizedException` → 401). `WebApi/GlobalExceptionHandler.cs` maps them to ProblemDetails responses. **Do not add try/catch to controllers** — throw the appropriate exception in the service instead.

Application layer never references Identity/EF types directly: it talks to `IIdentityService`, `ITokenGenerator`, and `IUnitOfWork` (implementations in `Infrastructure/`). Keep it that way.

### Frontend Structure
```
core/guards/        → authGuard (functional, protects routes)
core/interceptors/  → authInterceptor (functional, attaches JWT to every request)
core/services/      → API services (auth, student, predmet, ispitiPrijava)
features/           → Page components (login, pocetna, moji-predmeti, prijava-ispita, ispitni-rokovi)
```
Routes are lazy-loaded in `app.routes.ts`. DI is configured in `app.config.ts`.

### Dev vs Production Hosting
- **Dev**: Angular runs separately (`ng serve` on :4200); `proxy.conf.json` forwards `/api` to `:7185`; ASP.NET SPA middleware proxies back.
- **Production**: `dotnet publish` serves the pre-built Angular files from `../ClientApp/dist/client-app/browser` as static files via `UseSpaStaticFiles`.

### Authentication
- ASP.NET Identity (`ApplicationUser` extends `IdentityUser`) backed by the same `StudentAplikacijaContext`.
- **JWT secret is NOT in appsettings.json** — it lives in user-secrets (`dotnet user-secrets set "JwtSettings:Secret" "<value>"`). Issuer/audience/lifetimes are in the `JwtSettings` section and are validated.
- Login issues an access token (1h) + a rotating refresh token (7 days, SHA-256 hash stored in `RefreshTokens` table). `/api/auth/refresh` rotates the pair; `/api/auth/logout` revokes.
- Registration always assigns the `Student` role (client cannot choose); roles are seeded at startup via `app.SeedIdentityAsync()`.
- Controllers extract the student identity via `User.GetStudentId()` (throws `UnauthorizedException` if missing).
- Angular stores both tokens in `localStorage`; `authInterceptor` attaches the access token and on 401 transparently refreshes and retries (shared in-flight refresh).
- `[Authorize(Roles = Roles.Student)]` guards backend endpoints (constants in `Domain/Constants/Roles.cs`).

### Data Transfer
DTOs in `Application/DTOs/` are mapped **manually** inside services — AutoMapper is referenced in the `.csproj` but not configured. Do not add AutoMapper wiring without discussing it first.

### Database Drift Warning
The DB and migration history have drifted in the past (migrations applied to the DB whose files were deleted). Known remaining drift:
- The `PokušajiIspita` table does NOT exist in the DB, but the `PokušajiIspitum` entity is still mapped in the model — any query touching it (or including `StudentIspiti.PokušajiIspita`) will fail at runtime.
- The `IspitPreduslovi` table exists in the DB but has no entity in the model (harmless).

Verify against the real schema before relying on the model.

### Entity Naming Note
Domain entity files are in `Domain/Etities/` (typo in folder name — do not rename, migrations depend on the assembly paths).
