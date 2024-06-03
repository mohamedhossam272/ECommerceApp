# ECommerceApp

ECommerceApp is an ASP.NET Core MVC project for an e-commerce website. This project includes user authentication, role management, product management, and search functionality.

## Features

- User registration, login, logout, and password recovery
- Role-based authorization
- CRUD operations for products
- Search functionality for products
- Seed data for roles and an admin user

## Technologies

- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Core Identity
- SQL Server
- Bootstrap

## Project Structure

- **ECommerceApp (Presentation Layer - PL)**
  - Controllers
  - Views
    - Products
    - Account
    - Home
    - Role
  - wwwroot
  - Program.cs
  - Startup.cs

- **ECommerceApp.BLL (Business Logic Layer - BLL)**
  - Services
    - Implementations
      - ProductService.cs
    - Interfaces
      - IProductService.cs

- **ECommerceApp.DAL (Data Access Layer - DAL)**
  - Context
    - ApplicationDbContext.cs
  - Entities
    - Product.cs
    - Order.cs
    - OrderItem.cs
  - Repositories
    - Implementations
      - ProductRepository.cs
    - Interfaces
      - IProductRepository.cs

## Setup Instructions

### Prerequisites

- .NET 6 SDK
- SQL Server
- Visual Studio or any preferred IDE

### Steps to Run the Project

1. **Clone the Repository**

   ```sh
   git clone https://github.com/yourusername/ECommerceApp.git
   cd ECommerceApp
