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
