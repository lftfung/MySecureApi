# MySecureApi - Secure Financial Management Web API

A robust **ASP.NET Core Web API** built with **.NET 8** and **Clean Architecture**.  
This project demonstrates my transition from Unity/C# game development to professional backend engineering, focusing on **maintainability, testability, security, and scalability**.

## Technical Highlights

- **Clean Architecture** – Fully decoupled layers (API, Application, Domain, Infrastructure)
- **Repository Pattern** + **Dependency Injection** – Clean separation of concerns
- **Entity Framework Core** – Secure data persistence with PostgreSQL / SQL Server
- **DTO Mapping** – Prevents over-posting and protects internal entities
- **Automated Unit Testing** – Comprehensive tests using **xUnit + Moq** (behavior & state verification)
- **Docker Support** – Ready for containerized deployment

## Project Structure
```bash
MySecureApi/
├── Api/                          # Controllers, Middleware, Program.cs
├── Application/                  # Services, DTOs, Business Logic, Validators
├── Domain/                       # Entities, Repository Interfaces (most stable layer)
├── Infrastructure/               # EF Core DbContext, Repository Implementations
├── MyFinanceApi.Tests/           # xUnit + Moq unit tests
├── docker-compose.yml            # Local development with PostgreSQL
└── README.md
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- Docker (recommended)

### 1. Clone the repository
```bash
git clone https://github.com/lftfung/MySecureApi.git
cd MySecureApi
```
### 2. Restore dependencies
```bash
dotnet restore
```
### 3. Run with Docker (recommended)
```bash
docker-compose up --build
```
### 4. Run Unit Tests
```bash
dotnet test
```

## Testing
The project includes a dedicated test project with behavior verification for key services (e.g. TransactionService).

xUnit + Moq for mocking repositories
Tests cover CRUD operations, validation, and edge cases

## Tech Stack

Framework: .NET 8 + ASP.NET Core Web API
ORM: Entity Framework Core
Architecture: Clean Architecture + Repository Pattern
Testing: xUnit, Moq
Database: PostgreSQL / SQL Server
Containerization: Docker + docker-compose
Other: AutoMapper / DTOs, FluentValidation (in progress)

## Screenshots
### Swagger UI - API Endpoints

![Swagger UI](https://github.com/user-attachments/assets/29d80876-84cd-49c0-8c7d-a8bf3e072c5b)
<img width="372" height="333" alt="image" src="https://github.com/user-attachments/assets/75529a5e-2c61-4b1d-912e-a7c3ceb074a7" />

### Transaction Endpoint Example
![Create Transaction](https://github.com/user-attachments/assets/d74befe9-313c-413a-9019-9de3ddac7dc8)

### Unit Test Results
![Unit Test Results](https://github.com/user-attachments/assets/b6472ef7-4812-4cc6-ab45-40e2b9ce8ec6)


## Purpose
This project serves as a bridge between my previous experience in complex C# systems (Unity game development) and modern enterprise backend development. It showcases my ability to design scalable, testable, and production-ready APIs
