# Advanced Task Management System

System zarządzania projektami i zadaniami przygotowany w dwóch wariantach backendu ASP.NET Core:

- **CRUD** – kontrolery, serwisy aplikacyjne i repozytoria,
- **CQRS** – komendy, zapytania i handlery obsługiwane za pomocą biblioteki MediatR.

Oba warianty udostępniają analogiczny interfejs REST API oraz realizują porównywalny zakres funkcjonalny. Projekt został przygotowany jako przykład do porównania architektury CRUD i CQRS.

## Technologie

- .NET 8,
- C#,
- ASP.NET Core Web API,
- Entity Framework Core 8,
- Microsoft SQL Server,
- MediatR,
- AutoMapper,
- FluentValidation,
- JWT Bearer Authentication,
- Swagger / OpenAPI,
- xUnit.

## Wymagania

- .NET SDK 8.x,
- Microsoft SQL Server, SQL Server Express albo LocalDB,
- Visual Studio 2022, Visual Studio Code lub inne środowisko obsługujące .NET 8.

## Struktura rozwiązania

Rozwiązanie znajduje się w pliku `AdvancedTaskManagementSystem.sln`.

### Wariant CRUD

- `AdvancedTaskManagementSystemCRUD.Domain` – encje i stałe domenowe,
- `AdvancedTaskManagementSystemCRUD.Application` – DTO, serwisy, walidacja i interfejsy repozytoriów,
- `AdvancedTaskManagementSystemCRUD.Infrastructure` – Entity Framework Core, repozytoria i obsługa bazy danych,
- `AdvancedTaskManagementSystemCRUD.WebApi` – kontrolery REST API i konfiguracja aplikacji,
- `AdvancedTaskManagementSystemCRUD.Tests` – testy jednostkowe,
- `AdvancedTaskManagementSystemCRUD.IntegrationTests` – testy integracyjne API i warstwy dostępu do danych.

### Wariant CQRS

- `AdvancedTaskManagementSystemCQRS.Domain` – encje i stałe domenowe,
- `AdvancedTaskManagementSystemCQRS.Application` – komendy, zapytania, handlery, DTO i walidacja,
- `AdvancedTaskManagementSystemCQRS.Infrastructure` – Entity Framework Core, konfiguracja bazy i obsługa trwałości danych,
- `AdvancedTaskManagementSystemCQRS.WebApi` – kontrolery REST API i konfiguracja aplikacji,
- `AdvancedTaskManagementSystemCQRS.Tests` – testy jednostkowe,
- `AdvancedTaskManagementSystemCQRS.IntegrationTests` – testy integracyjne API i warstwy dostępu do danych.

## Najważniejsze funkcjonalności

- rejestracja i logowanie użytkowników,
- uwierzytelnianie za pomocą JWT Bearer,
- role `Administrator`, `ProjectManager` i `TeamMember`,
- tworzenie, edycja i usuwanie projektów,
- zarządzanie członkami projektów,
- tworzenie i edycja zadań,
- przypisywanie zadań użytkownikom lub grupom,
- zmiana statusu, priorytetu i typu zadania,
- określanie terminów realizacji,
- filtrowanie i sortowanie zadań,
- komentarze,
- etykiety,
- załączniki,
- historia zmian zadań,
- raporty dotyczące zadań, projektów i obciążenia użytkowników.

## Zasady dostępu

Endpointy aplikacji wymagają uwierzytelnienia, z wyjątkiem endpointów rejestracji i logowania.

Edycja zadania jest dostępna dla:

- aktywnego członka projektu,
- administratora, niezależnie od członkostwa w projekcie.

Użytkownik, który nie należy do projektu, nie może edytować jego zadań. Uprawnienia są sprawdzane w warstwie aplikacji, niezależnie od interfejsu użytkownika.

## Konfiguracja

Konfiguracja aplikacji znajduje się w plikach `appsettings.json` projektów Web API.

### CRUD

Plik:

```text
AdvancedTaskManagementSystemCRUD.WebApi/appsettings.json
```

Wykorzystywane ustawienia:

- `Authentication:JwtKey` – klucz używany do obsługi tokenów JWT,
- `Authentication:JwtExpireDays` – czas ważności tokenu w dniach,
- `Authentication:JwtIssuer` – wystawca tokenu,
- `ConnectionStrings:TaskManagerCRUD` – połączenie z bazą danych CRUD.

### CQRS

Plik:

```text
AdvancedTaskManagementSystemCQRS.WebApi/appsettings.json
```

Wykorzystywane ustawienia:

- `Authentication:JwtKey` – klucz używany do obsługi tokenów JWT,
- `Authentication:JwtExpireDays` – czas ważności tokenu w dniach,
- `Authentication:JwtIssuer` – wystawca tokenu,
- `ConnectionStrings:TaskManagerCQRS` – połączenie z bazą danych CQRS.

Przed uruchomieniem należy sprawdzić, czy wartość `Data Source` w odpowiednim connection stringu wskazuje właściwą instancję SQL Server. Oba warianty korzystają z oddzielnych baz danych.

## Przygotowanie bazy danych

Jeżeli narzędzie Entity Framework Core CLI nie jest zainstalowane, można je dodać poleceniem:

```powershell
dotnet tool install --global dotnet-ef --version 8.*
```

Aktualizację bazy danych wykonuje się z katalogu głównego rozwiązania:

```powershell
dotnet ef database update --project .\AdvancedTaskManagementSystemCRUD.Infrastructure --startup-project .\AdvancedTaskManagementSystemCRUD.WebApi
```

```powershell
dotnet ef database update --project .\AdvancedTaskManagementSystemCQRS.Infrastructure --startup-project .\AdvancedTaskManagementSystemCQRS.WebApi
```

## Uruchomienie

Z katalogu głównego rozwiązania można uruchomić wybrany wariant:

```powershell
dotnet run --project .\AdvancedTaskManagementSystemCRUD.WebApi
```

```powershell
dotnet run --project .\AdvancedTaskManagementSystemCQRS.WebApi
```

Oba warianty mają domyślnie ustawione te same porty w plikach `launchSettings.json`. Przy jednoczesnym uruchamianiu można nadać im różne adresy:

```powershell
dotnet run --project .\AdvancedTaskManagementSystemCRUD.WebApi --urls "https://localhost:7075;http://localhost:5218"
dotnet run --project .\AdvancedTaskManagementSystemCQRS.WebApi --urls "https://localhost:7076;http://localhost:5219"
```

Swagger / OpenAPI jest dostępny w środowisku `Development` pod adresem:

```text
https://localhost:<port>/swagger
```

## Migracje

Migracje Entity Framework Core znajdują się w projektach infrastruktury obu wariantów. Po zmianie modelu danych nową migrację należy utworzyć dla odpowiedniego wariantu, na przykład:

```powershell
dotnet ef migrations add NazwaMigracji --project .\AdvancedTaskManagementSystemCRUD.Infrastructure --startup-project .\AdvancedTaskManagementSystemCRUD.WebApi
```

Dla wariantu CQRS należy użyć projektów `AdvancedTaskManagementSystemCQRS.Infrastructure` oraz `AdvancedTaskManagementSystemCQRS.WebApi`.

## Testy

Uruchomienie wszystkich testów rozwiązania:

```powershell
dotnet test .\AdvancedTaskManagementSystem.sln
```

Testy jednostkowe CRUD:

```powershell
dotnet test .\AdvancedTaskManagementSystemCRUD.Tests
```

Testy jednostkowe CQRS:

```powershell
dotnet test .\AdvancedTaskManagementSystemCQRS.Tests
```

Testy integracyjne CRUD:

```powershell
dotnet test .\AdvancedTaskManagementSystemCRUD.IntegrationTests
```

Testy integracyjne CQRS:

```powershell
dotnet test .\AdvancedTaskManagementSystemCQRS.IntegrationTests
```

Testy obejmują między innymi walidację danych, serwisy, handlery, mechanizmy autoryzacji, kontrolery API oraz działanie modelu danych. Testy integracyjne korzystają z testowej konfiguracji uwierzytelniania i bazy EF Core InMemory.

## Charakterystyka wariantów

W wariancie CRUD przepływ obsługi żądania przebiega przez kontroler, serwis aplikacyjny i repozytorium.

W wariancie CQRS kontroler przekazuje żądanie do MediatR, który kieruje je do odpowiedniego command handlera albo query handlera.

Wariant CQRS wykorzystuje logiczny podział operacji odczytu i zapisu. Oba warianty korzystają z relacyjnej bazy danych Microsoft SQL Server.

