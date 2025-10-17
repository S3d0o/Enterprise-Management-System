# Enterprise Management System (EMS)

A **full-stack ASP.NET Core MVC application** built using **Clean Architecture**, **SOLID principles**, and **enterprise-grade design patterns**.  
This project demonstrates how to design, build, and scale a modular system for managing employees, departments, users, and roles in a professional .NET environment.

---

## Overview

The **Enterprise Management System (EMS)** is developed to showcase clean, maintainable software design that mirrors real-world enterprise projects.  
It combines front-end interactivity with a robust back-end and emphasizes code organization, scalability, and security.

The project implements a **multi-layered architecture** to ensure separation of concerns, testability, and easy extensibility.

---

## Architecture Overview

EnterpriseManagementSystem
│
├── EMS.Presentation # MVC Layer: Controllers, Views, ViewModels, Static Assets
│
├── EMS.BusinessLogic # Application Core: Services, DTOs, Factories, Mappings, Interfaces
│
├── EMS.DataAccess # Infrastructure: EF Core, Repositories, Unit of Work, Context, Migrations
    │
    └── Models # Domain Models: Employees, Departments, Identity, Shared Entities

Each layer communicates via abstractions, ensuring **flexibility** and **loose coupling**.

---

## Key Features

### Employee & Department Management
- Full CRUD operations for Employees and Departments  
- Department–Employee relationships  
- AJAX-powered search and live filtering  
- File and image uploads via Attachment Service  

### Authentication & Authorization
- Integrated **ASP.NET Identity** with custom `ApplicationUser` and `ApplicationRole`  
- Secure registration and login  
- Role-based access control (Admin / User)  
- Email confirmation and password reset via SMTP  

### User Interface & Experience
- **Bootstrap 5** + **jQuery** for responsive, modern UI  
- Dynamic AJAX operations for seamless interactivity  
- Modals and Toast notifications for user feedback  
- Validation using Data Annotations and client-side checks  

### Email System
- SMTP-based email confirmation and notifications  
- Reusable, template-driven email service  

## Core Infrastructure
- Repository & Unit of Work patterns for clean data access  
- AutoMapper for mapping between Entities, DTOs, and ViewModels  
- Dependency Injection for modular service management  
- Configurable settings through `appsettings.json`  

---

## Tech Stack

| Category | Technology |
|-----------|-------------|
| Language | C# |
| Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Front-End | HTML, CSS, JavaScript, Bootstrap 5, jQuery |
| Authentication | ASP.NET Identity |
| Mapping | AutoMapper |
| Architecture | Clean Architecture, SOLID, Repository & Unit of Work |
| Email | SMTP (Gmail) |
| Design Patterns | Factory, DTO, Repository, Unit of Work, Dependency Injection |

---

## Security Highlights

- **Password Hashing:** Secure credential storage  
- **Email Verification:** Activation and password reset  
- **Input Validation:** Protection from SQL Injection & XSS  
- **Role-Based Access:** Multi-level permissions  
- **Session Management:** Secure user state handling  

---

## Design Principles & Patterns

- **SOLID Principles** — maintainable and extensible architecture  
- **Separation of Concerns** — modular layer responsibilities  
- **Dependency Injection** — flexibility and testability  
- **Repository Pattern** — abstracted data access  
- **Unit of Work Pattern** — transaction consistency  
- **DTOs + AutoMapper** — clean data flow  
- **Service Abstraction** — independent logic layers  

---

🧑‍💻 Author

Saad Mohamed
 Physics & Computer Science Student — Faculty of Science
 Backend Developer (C# / .NET / SQL)
📧 Saadmohamedd001@gamil.com
🔗 www.linkedin.com/in/saad-mohamed-li

“A great architecture doesn’t just organize code — it defines the developer’s mindset.”


