# MySecureApi - Backend Financial System

A robust ASP.NET Core Web API demonstrating high-level software engineering principles. Designed to showcase my transition from **Unity/C# Game Development**  to professional **.NET Backend Architecture**.

## Technical Highlights
- **Clean Architecture**: Decoupled layers (API, Application, Domain, Infrastructure) for maximum maintainability.
- **Repository Pattern**: Abstracted data access layer to isolate database logic.
- **Automated Testing**: 100% core logic validation using **xUnit** and **Moq** for behavior verification.
- **Data Integrity**: Used **Entity Framework Core** and **DTOs** to ensure secure data flow and prevent over-posting.

## Project Structure & Layers
- **Domain**: Contains core entities and repository interfaces (The stablest layer).
- **Application**: Holds business logic, services, and DTO mappings.
- **Infrastructure**: Implements data access using EF Core and SQL Server/PostgreSQL.
- **Api**: Handles HTTP requests, controllers, and API configurations.

## Getting Started
1. Clone the repo.
2. Restore dependencies: `dotnet restore`.
3. Run Unit Tests: `dotnet test`.

<img width="1045" height="384" alt="image" src="https://github.com/user-attachments/assets/e18a0730-4be1-4d16-aef5-ac9f4e1f357e" />

This project bridges my experience in **complex C# systems (Unity)** and **cloud automation (AWS)** into a modern backend framework, focusing on building stable, testable, and enterprise-ready APIs.
