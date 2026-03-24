# KitaTrackDemo.Api

**KitaTrackDemo.Api** is a backend Web API built with modern .NET 10.0. It follows a decoupled N-Tier (Layered) Architecture, serving as a professional demo for managing e-wallet transactions (such as GCash or Maya). This project emphasizes security, data isolation, and maintainability through modern design patterns. The API is designed for demo and educational purposes.

## Live Demo

The API is currently hosted and accessible on MonsterASP:  
**[http://kitatrack-demo-api.runasp.net/](http://kitatrack-demo-api.runasp.net/)**

> **Note:** The SQL Server database is also hosted at MonsterASP and is pre-loaded with sample data for testing. The **Swagger UI** is enabled at the root for immediate testing of all endpoints. Use the **Authorize** button to input your JWT token.

### Demo Account Credentials
To test the authorized endpoints, use the following credentials in the `/api/Auth/Login` endpoint to receive a JWT token:
```text
Email: rubatay.dev@gmail.com
Password: 12345678Qw!
```
The Transactions table is already pre-populated with historical records for this specific account to demonstrate the filtering/pagination features and other transaction endpoints.


## Tech Stack & Architecture

### Core Technologies
- Framework: .NET 10.0 (ASP.NET Core Web API)
- Language: C#
- Database: SQL Server (Hosted on MonsterASP)
- ORM: Entity Framework Core
- Data Querying: LINQ
- CI/CD: GitHub Actions

### Engineering Patterns
- **Layered Separation**: Distinct separation between Controllers (Presentation), Services (Business Logic) and Repositories (Data Access).
- **Dependency Injection**: Extensively used to decouple components, allowing for easy swapping of database providers (e.g., SQL Server to In-Memory) without changing business logic.
- **Result Pattern**: Business logic returns a generic Result<T> object instead of throwing exceptions, ensuring clean and predictable flow control.
- **Repository Pattern**: Decouples data access from business logic, utilizing Dependency Injection to allow for easy swapping of database providers (SQL Server, In-Memory, etc.).
- **DTO Pattern**: Uses Data Transfer Objects to prevent over-posting and ensure the internal database schema is never exposed directly to the client.

## Features

- **User Authentication**: Register, login with JWT tokens and BCrypt password hashing
- **Transaction Management**: Create, read, update, delete e-wallet transactions
- **Filtering & Pagination**: Filter by date range, type, reference with paginated results
- **Data Isolation**: Users only access their own transactions via UserId claims

## Security & Data Isolation

### Identity & Password Security
- **BCrypt Hashing**: User passwords are never stored in plain text. They are hashed using the BCrypt.Net library.
- **Secure Validation**: Login validation uses BCrypt.Verify to compare incoming passwords against stored hashes.

### Access Control
- **JWT Authentication**: Secure endpoints protected via Bearer tokens.
- **Claims-Based Authorization**: The API extracts the NameIdentifier claim from the JWT.
- **Strict Data Isolation**: Every database query is scoped to the UserId extracted from the token. This ensures that users can only view, edit, or delete their own data, preventing unauthorized cross-user access.
- **Validated Controllers**: Uses a centralized validation helper (IsUserClaimValid) to ensure required claims are present before executing any logic.

## CI/CD & Deployment
- **GitHub Actions**: Controlled via .github/workflows/deploy.yml.
- **Automated Workflow**: On every push to the main branch, the runner performs a restore, build and publish.
- **Secure Deployment**: Sensitive environment variables (Server credentials, DB strings, JWT Keys) are managed via GitHub Secrets.

## API Endpoints

| Route | Method | Description | Auth Required |
|------|--------|------------|--------------|
| /api/Auth/Register | POST | Register a new user account | No |
| /api/Auth/Login | POST | Authenticate and receive a JWT token | No |
| /api/Transaction | GET | Get paginated transaction history (Filtered) | Yes |
| /api/Transaction/{id} | GET | Retrieve a specific transaction by ID | Yes |
| /api/Transaction | POST | Create a new e-wallet transaction | Yes |
| /api/Transaction | PUT | Update an existing transaction | Yes |
| /api/Transaction/{id} | DELETE | Remove a transaction record | Yes |
| /api/TransactionType/GetAll | GET | Get all available transaction types | Yes |

## Project Structure
| Path | Responsibility |
| :--- | :--- |
| Controllers/ | Presentation: API endpoints (Auth, Transactions) |
| Services/ | Application: Business logic |
| Repositories/ | Infrastructure: Data access & Repository pattern |
| Models/Entities/ | Core business objects (User, Transaction) |
| Models/DTOs/ | Request/Response objects for API contracts |
| Interfaces/ | Abstractions: Dependency Injection contracts |
| Data/ | Persistence: DbContext & Entity configurations |
| Extensions/ | Configuration: DI setup (Auth, Database, Swagger) |
| Common/ | Shared: Result<T>, Middleware and Pagination |
| Migrations/ | Database: EF Core version history |
| Program.cs | Entry Point: App startup & middleware pipeline |

## Installation & Local Setup

### Prerequisites
- [.NET 10.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (local or remote)
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssms)
- **EF Core CLI Tools**: Install globally in terminal using:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Steps

#### 1. Clone the repository:
```bash
git clone https://github.com/rubatay-dev/KitaTrackDemo.Api.git
cd KitaTrackDemo.Api
```

#### 2. Setup User Secrets:
This project uses .NET Secret Manager to keep sensitive strings out of source control.

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your-connection-string>"
dotnet user-secrets set "Jwt:Key" "<your-jwt-key>"
dotnet user-secrets set "Jwt:Issuer" "<your-issuer>"
dotnet user-secrets set "Jwt:Audience" "<your-audience>"
dotnet user-secrets set "Jwt:ExpiryInMinutes" "<your-ExpiryInMinutes>"
```

#### 3. Restore and Build:
```bash
dotnet restore
```

#### 4. Run Migrations:
Ensure your SQL Server instance is running before executing this:
```bash
dotnet ef database update
```

#### 5. Launch Application:
```bash
dotnet run
```

## License

This project is for demo and educational purposes.

Developed by Roberto Ubatay Jr.