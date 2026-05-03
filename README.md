# 🛠️ E-Commerce Tools Shop

A scalable and production-ready **full-stack e-commerce platform** for buying tools, built with **.NET Clean Architecture** and **Angular 21** using modern frontend and backend practices.

---

## 🚀 Overview

This project demonstrates how to build a **real-world e-commerce system** with:

- Clean Architecture (separation of concerns)
- Scalable backend design
- Modern Angular frontend (Signals + Standalone APIs)
- High-performance caching using Redis
- Secure authentication using HTTP-only cookies

  ## ✨ Features

### 🔐 Authentication & Security
- Cookie-based authentication (HTTP-only cookies)
- Protection against XSS (no tokens stored in localStorage)
- Role-ready structure for future RBAC support

### 🛒 Shopping Experience
- Product listing with filtering, sorting, and pagination
- Add/remove/update items in shopping cart
- Persistent cart using Redis caching
- Optimized performance with reduced database calls

### 🧠 Backend Capabilities
- Clean Architecture implementation
- Specification Pattern for dynamic queries
- Fluent API for entity configurations
- DTO validation using FluentValidation

### 🎨 Frontend Capabilities
- Angular 21 with Standalone Components
- Reactive state using Signals
- Feature-based structure
- HTTP interceptors and route guards
- Tailwind CSS + Angular Material UI

  ## 🏗️ Architecture

Core (Domain Layer)
│── Entities
│── Value Objects
│── Interfaces
│── Specifications

Application Layer
│── DTOs
│── Validators
│── Use Cases

Infrastructure Layer
│── DbContext (EF Core)
│── Configurations (Fluent API)
│── Repositories
│── Redis Caching
│── Authentication (Cookies)

API Layer
│── Controllers
│── Middleware
│── Dependency Injection

## 🛠️ Tech Stack

### Backend
- .NET (latest)
- ASP.NET Core Web API
- Entity Framework Core
- Redis
- FluentValidation

### Frontend
- Angular 21
- Angular Material
- Tailwind CSS
- RxJS & Signals

  ## ⚡ Performance Optimizations

- Redis caching for shopping cart
- Efficient querying using Specification Pattern
- DTO projections to reduce over-fetching

  ## 🔮 Future Improvements

- Payment integration (Stripe / Moyasar / PayTabs)
- Order management system
- Admin dashboard
- Multi-language support
- Unit & Integration testing
- Docker support

  ## ▶️ Getting Started

### Clone the repository
git clone https://github.com/e-ahmed-mahmoud/E_CommereToolsShop.git
cd E_CommereToolsShop

### Run Backend
cd API
dotnet run

### Run Frontend
cd client
npm install
ng serve
