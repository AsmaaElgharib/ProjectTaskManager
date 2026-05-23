# Project Task Manager API

A scalable **Project & Task Management REST API** built with **.NET 9**, **ASP.NET Core**, **Clean Architecture**, **CQRS with MediatR**, and **JWT Authentication**.


---

## Architecture Overview

This project follows **Clean Architecture** principles with a strict dependency rule: outer layers depend on inner layers, never the reverse.

```
┌─────────────────────────────────────────┐
│              API Layer                  │  ← Controllers, Middleware, Filters
│         (ProjectTaskManager.API)        │
├─────────────────────────────────────────┤
│           Application Layer            │  ← CQRS Commands/Queries, Validators
│      (ProjectTaskManager.Application)  │     MediatR Handlers, DTOs, Interfaces
├─────────────────────────────────────────┤
│            Domain Layer                │  ← Entities, Enums, Domain Interfaces
│        (ProjectTaskManager.Domain)     │     (NO external dependencies)
├─────────────────────────────────────────┤
│         Infrastructure Layer           │  ← EF Core, JWT, Bcrypt, Repositories
│    (ProjectTaskManager.Infrastructure) │
└─────────────────────────────────────────┘
```


---

## Tech Stack

| Concern          | Technology                          |
|------------------|-------------------------------------|
| Framework        | ASP.NET Core 9 Web API              |
| ORM              | Entity Framework Core 9             |
| Database         | SQL Server 2022                     |
| Authentication   | JWT Bearer Tokens                   |
| CQRS             | MediatR 12                         |
| Validation       | FluentValidation 11                 |
| Password Hashing | BCrypt.Net                          |
| API Docs         | Swagger / Swashbuckle               | 
| Containerization | Docker + Docker Compose             |

---


## Project Structure

```
ProjectTaskManager/
├── src/
│   ├── ProjectTaskManager.Domain/          # Enterprise business rules
│   │   ├── Common/BaseEntity.cs
│   │   ├── Entities/                       # User, Project, ProjectTask
│   │   ├── Enums/                          # TaskStatus, TaskPriority
│   │   └── Interfaces/                     # IProjectRepository, ITaskRepository, IUserRepository
│   │
│   ├── ProjectTaskManager.Application/     # Application business rules
│   │   ├── Common/
│   │   │   ├── Behaviors/ValidationBehavior.cs   # MediatR pipeline
│   │   │   ├── Exceptions/                        # NotFoundException, UnauthorizedException, etc.
│   │   │   ├── Interfaces/                        # IJwtService, IPasswordHasher, ICurrentUserService
│   │   │   └── Models/ApiResponse.cs              # Generic response wrapper
│   │   └── Features/
│   │       ├── Auth/Commands/              # RegisterCommand, LoginCommand
│   │       ├── Projects/Commands/          # CreateProject, UpdateProject, DeleteProject
│   │       ├── Projects/Queries/           # GetAllProjects, GetProjectById
│   │       ├── Tasks/Commands/             # CreateTask, UpdateTask, DeleteTask
│   │       └── Tasks/Queries/              # GetTasksByProject
│   │
│   ├── ProjectTaskManager.Infrastructure/  # Frameworks & drivers
│   │   ├── Data/
│   │   │   ├── ApplicationDbContext.cs
│   │   │   ├── Configurations/             # EF Fluent API config
│   │   │   └── Migrations/                 # EF migration files
│   │   ├── Repositories/                   # Repository implementations
│   │   └── Services/                       # JwtService, PasswordHasher, CurrentUserService
│   │
│   └── ProjectTaskManager.API/             # Presentation layer
│       ├── Controllers/                    # AuthController, ProjectsController, TasksController
│       ├── Middleware/                     # GlobalExceptionHandlingMiddleware
│       ├── Extensions/                     # SwaggerExtensions
│       └── Program.cs

