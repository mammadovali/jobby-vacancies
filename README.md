# Jobby (Backend)

Jobby is a backend API for a vacancies / recruitment platform. It provides job listings, applicant flows, tests, dashboards and admin capabilities. The codebase follows a layered, testable architecture with clear separation between presentation, application logic and persistence.

## Table of Contents
- [Project aim](#project-aim)
- [Features](#features)
- [Technologies](#technologies)
- [Architecture & Design Patterns](#architecture--design-patterns)
- [Getting started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Configuration](#configuration)
  - [Database (migrations & seeding)](#database-migrations--seeding)
  - [Run the API](#run-the-api)
- [Project structure](#project-structure)
  - [Presentation (WebApi)](#presentation-webapi)
  - [Application / Core](#application--core)
  - [Infrastructure / Persistence](#infrastructure--persistence)
  - [Shared / Common](#shared--common)
- [Notable implementation details](#notable-implementation-details)
- [API docs & auth](#api-docs--auth)
- [Contributing](#contributing)

---

## Project aim
Provide a robust, extensible backend API for managing job vacancies, applicants, and recruitment workflows. Focus areas:
- Secure JWT-based authentication (cookie support)
- Clean separation of concerns (controllers, commands, repositories)
- Robust persistence via EF Core with migrations and seed data
- Observability: HTTP logging and Swagger documentation
- Designed for production (large request handling)

---

## Features
- JWT authentication with cookie fallback (`X-Access-Token`)
- API documentation via Swagger UI at `/swagger`
- EF Core-backed persistence with automatic migrations on startup
- Application seed data
- CORS policy allowing cross-origin requests (named `AllowAllOrigins`)
- File upload limits increased to support large multipart bodies
- Command/query style handlers for application flows (e.g., test start/finish)

---

## Technologies
- .NET 8, C# 12
- ASP.NET Core Web API
- Entity Framework Core (migrations & DbContext)
- JWT + Cookie authentication (Microsoft.AspNetCore.Authentication.JwtBearer)
- Swashbuckle (Swagger)
- Dependency Injection via Microsoft.Extensions.DependencyInjection
- (Project contains CQRS-style command handlers — typically used with MediatR)

---

## Architecture & Design Patterns
- Layered/Clean architecture: `Presentation` (Web API) → `Application`/`Core` (business rules & handlers) → `Infrastructure`/`Persistence` (EF + repositories)
- CQRS-style command handlers (e.g., `StartTestCommandHandler`, `FinishTestCommandHandler`) to separate writes/commands from reads/queries.
- Repository pattern for persistence abstraction (read/write repositories).
- Dependency Injection used pervasively for testability and modularity.
- Centralized middleware pipeline (custom middlewares wired in `Program.cs`).

---

## Getting started

### Prerequisites
- .NET 8 SDK
- A supported database (e.g.,PostgreSQL) configured in connection string (For a small period of time, project uses a database published to cloud)