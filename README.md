# 🛠 E-Commerce Tools Shop

A modern full-stack e-commerce platform for buying tools and equipment, built with **ASP.NET Core (Clean Architecture)** and **Angular 21 (Standalone + Signals)**.

---

## 🚀 Overview

This project demonstrates a scalable and maintainable e-commerce system with a strong focus on:

* Clean Architecture (Backend)
* Feature-Based Architecture (Frontend)
* Secure Cookie-Based Authentication
* Scalable querying using Specification Pattern
* Modern UI development with TailwindCSS & Angular Material

---

## ✨ Features

### 🛍 Product & Catalog

* Browse tools by category
* Product filtering and searching using Specification Pattern
* Pagination & optimized queries

### 🛒 Shopping Experience

* Add to cart
* Manage cart items
* Order creation workflow

### 👤 Authentication & Security

* Cookie-based authentication (HttpOnly cookies)
* ASP.NET Core Identity integration
* Role-Based Access Control (RBAC)

---

## 🏗 Architecture

### 🔹 Backend (ASP.NET Core)

**Core**

* Domain Entities & Value Objects
* Application DTOs
* Interfaces & Business Logic
* Validation (FluentValidation)
* Specification Pattern for flexible querying

**Infrastructure**

* Entity Framework Core (DbContext)
* Identity & Cookie Authentication
* Repository + Specification implementation

**API**

* Controllers
* Middleware
* Dependency Injection

---

### 🔹 Frontend (Angular 21)

* Standalone Components (no NgModules)
* Signals for state management
* Feature-based folder structure
* Angular Material for UI components
* TailwindCSS for responsive and utility-first styling
* HTTP Interceptors with `withCredentials`

---

## 🔐 Authentication Flow

* User logs in → server issues secure HttpOnly cookie
* Browser automatically sends cookie with requests
* Server validates session using ASP.NET Identity

---

## 🛠 Technologies

### Backend

* ASP.NET Core (.NET 10)
* Entity Framework Core
* SQL Server
* Identity (Cookie Authentication)
* Specification Pattern
* FluentValidation
* Mapster

### Frontend

* Angular 21
* Angular Material
* TailwindCSS
* RxJS
* Signals API

---

## 🔥 Work In Progress

* 💳 Payment Integration
* 📦 Order tracking enhancements
* 🧾 Invoice generation

---

## 🧠 Design Highlights

* Clean Architecture for maintainability
* Specification Pattern for reusable and composable queries
* Cookie-based authentication for enhanced security
* Hybrid UI approach using TailwindCSS + Angular Material
* Feature-based frontend architecture

---

## 👨‍💻 Author

Ahmed Mahmoud

contact: ahmed.mahmoud.6618@gmail.com
