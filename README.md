# YetiMunch - Food Ordering System

YetiMunch is a modern food ordering system built using ASP.NET Core MVC following clean architecture principles and best practices.

## 🏗️ Architecture

The solution follows a clean architecture pattern with three main projects:

1. **YetiMunch (Presentation Layer)**
   - ASP.NET Core MVC application
   - Handles HTTP requests and user interface
   - Contains Views, Controllers, and web-related configurations

2. **YetiMunch.Domain**
   - Core business logic
   - Contains:
     - Entities
     - Models
     - Interface definitions (IRepository)
     - Business services
     - Domain-specific logic

3. **YetiMunch.Infrastructure**
   - Implementation of data access and external services
   - Contains:
     - Repository implementations
     - Database context and configurations
     - Data migrations
     - External service integrations

## 🚀 Features

- Clean Architecture implementation
- Domain-Driven Design (DDD) principles
- Repository Pattern for data access
- Entity Framework Core for database operations
- ASP.NET Core MVC for the web interface
- Dependency Injection
- Database migrations
- Separation of concerns

## 🛠️ Technical Stack

- **Framework**: ASP.NET Core MVC
- **ORM**: Entity Framework Core
- **Database**: SQL Server (configured through migrations)
- **Architecture Pattern**: Clean Architecture
- **Design Patterns**:
  - Repository Pattern
  - Dependency Injection
  - MVC Pattern

## 📁 Project Structure

```
YetiMunch/
├── YetiMunch/                 # Main Web Application
│   ├── Controllers/           # MVC Controllers
│   ├── Views/                 # Razor Views
│   ├── wwwroot/              # Static files (CSS, JS, images)
│   ├── Migrations/           # Database migrations
│   └── Program.cs            # Application entry point and configuration
│
├── YetiMunch.Domain/         # Business Logic Layer
│   ├── Entities/             # Domain entities
│   ├── Models/               # Domain models
│   ├── IRepository/          # Repository interfaces
│   └── Services/             # Business services
│
└── YetiMunch.Infrastructure/ # Data Access Layer
    ├── Data/                 # Database context and configurations
    ├── Repository/           # Repository implementations
    └── Migrations/           # Infrastructure-specific migrations
```

## 🔧 Setup and Configuration

1. Ensure you have the following prerequisites:
   - .NET Core SDK
   - SQL Server
   - Visual Studio 2022 or later

2. Clone the repository
3. Update the connection string in `appsettings.json`
4. Run database migrations
5. Build and run the application

## 🌟 Best Practices Implemented

- Clean Code principles
- SOLID principles
- Separation of Concerns
- Domain-Driven Design
- Repository Pattern for data access abstraction
- Proper dependency injection
- Code organization and modularity

## 🔄 Development Workflow

1. Domain entities and interfaces are defined in the Domain project
2. Infrastructure project implements the interfaces
3. Main project (YetiMunch) uses the implementations through dependency injection

## 📚 Additional Features

- Database migrations for version control of database schema
- Structured error handling
- Configuration management through appsettings.json
- Environment-specific settings (Development/Production)

## 🛡️ Security

- Proper configuration management
- Secure connection strings
- Environment-based configurations

 