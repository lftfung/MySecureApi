# MySecureApi - Secure Financial Management Web API

[![.NET CI/CD Pipeline](https://github.com/lftfung/MySecureApi/actions/workflows/dotnet.yml/badge.svg)](https://github.com/lftfung/MySecureApi/actions/workflows/dotnet.yml)

A robust **ASP.NET Core Web API** built with **.NET 8** and **Clean Architecture**.  
This project demonstrates my transition from Unity/C# game development to professional backend engineering, focusing on **maintainability, testability, security, and scalability**.

## Technical Highlights

- **Clean Architecture** – Fully decoupled layers (API, Application, Domain, Infrastructure)
- **Repository Pattern** + **Dependency Injection** – Clean separation of concerns
- **Entity Framework Core** – Secure data persistence with PostgreSQL / SQL Server
- **DTO Mapping** – Prevents over-posting and protects internal entities
- **Automated Unit Testing** – Comprehensive tests using **xUnit + Moq** (behavior & state verification)
- **Professional CI/CD** – GitHub Actions with automated build, XPlat coverage, rich test reporting & publish artifacts
- **Docker Support** – Ready for containerized deployment

## Project Structure
```bash
MySecureApi/
├── .github/
│   └── workflows/
│       └── dotnet.yml            # CI/CD pipeline: build + test (coverlet XPlat Code Coverage) + HTML coverage report + publish
├── Api/                          # Controllers, Middleware, Program.cs
├── Application/                  # Services, DTOs, Business Logic, Validators
├── Domain/                       # Entities, Repository Interfaces (most stable layer)
├── Infrastructure/               # EF Core DbContext, Repository Implementations
├── MySecureApi.Tests/            # xUnit + Moq unit tests (coverlet.collector already configured)
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
### Unit Test Results
![Unit Test Results](screenshots/unit-test.png)

*Comprehensive unit tests using xUnit and Moq (all tests passing).*

### Swagger UI - API Endpoints
![Swagger UI](screenshots/swagger-ui.png)

*Shows Authentication (Register & Login) and full Transaction CRUD endpoints.*

### Transaction Endpoint Example
![Create Transaction](screenshots/transaction-create.png)

*Example of POST /api/Transaction with request body.*

## Testing
The project includes a dedicated test project with behavior verification for key services (e.g. TransactionService).

xUnit + Moq for mocking repositories
Tests cover CRUD operations, validation, and edge cases

## CI/CD Pipeline

This repository now includes a **professional CI/CD pipeline** — a key differentiator for production-ready projects and technical interviews.

**What the pipeline delivers on every push and pull request to `main`:**

- Builds the full solution in Release mode with NuGet caching
- Executes **all unit tests** using `dotnet test --collect:"XPlat Code Coverage"` (leverages the existing `coverlet.collector` package)
- Publishes **detailed, collapsible test result reports** directly in GitHub PR checks and commit status (using EnricoMi publish action)
- Generates a full **HTML coverage report** (plus TextSummary) via ReportGenerator — downloadable as artifact
- Publishes the `Api` project output as a ready-to-deploy artifact

**How to inspect results (highly recommended for interviews):**

1. Go to the **Actions** tab in this repository
2. Click on the latest workflow run
3. Download these artifacts:
   - `test-results` — Raw `.trx` logs + `coverage.cobertura.xml`
   - `coverage-report` — Open `index.html` in browser to see beautiful coverage breakdown by file/class
   - `api-publish` — The published API (can be used for further Docker deployment)

This setup demonstrates strong DevOps practices, automated quality gates, and attention to test visibility — exactly what hiring managers look for in a .NET backend role.

## Tech Stack
Framework: .NET 8 + ASP.NET Core Web API
ORM: Entity Framework Core
Architecture: Clean Architecture + Repository Pattern
Testing: xUnit, Moq, coverlet (XPlat Code Coverage)
**CI/CD:** GitHub Actions (build, test, coverage reporting, PR test results, artifact publishing)
Database: PostgreSQL / SQL Server
Containerization: Docker + docker-compose
Other: AutoMapper / DTOs, FluentValidation (in progress)

## Screenshots

### 1. User Registration
![Register Request](screenshots/register-request.png)  
*POST /api/Auth/register – Successfully creates a new user*

![Register Response](screenshots/register-response.png)  
*Response message + user saved in database*

### 2. User Login (JWT Authentication)
![Login Request](screenshots/login-request.png)  
*POST /api/Auth/login – Returns JWT token*

![Login Response](screenshots/login-response.png)  
*JWT token received successfully*

### 3. Transaction CRUD Operations
![Create Transaction](screenshots/transaction-request.png)  
*POST /api/Transaction (Protected by JWT)*

![Transaction Success](screenshots/transaction-response.png)  
*Transaction created successfully*

**All CRUD operations are fully implemented and working:**
- ✅ Create (POST)
- ✅ Read All (GET)
- ✅ Read by ID (GET)
- ✅ Update (PUT)
- ✅ Delete (DELETE)

All endpoints are protected by JWT authentication.

## Purpose
This project serves as a bridge between my previous experience in complex C# systems (Unity game development) and modern enterprise backend development. It showcases my ability to design scalable, testable, and production-ready APIs
