# Drsfan - E-commerce Platform

Drsfan is a modern e-commerce platform built with ASP.NET Core 8.0, featuring a clean architecture and comprehensive product management system.

## Admin Account
- admin@gmail.com
- pass: Admin123*

## Features

- 🛍️ **Product Management**
  - CRUD operations for products
  - Multiple image upload support
  - Category management
  - Price and discount management

- 👤 **User Management**
  - User registration and authentication
  - Role-based authorization (Admin, Staff, Customer)
  - User profile management

- 🛒 **Shopping Cart**
  - Add/remove items
  - Update quantities
  - Price calculations
  - Session management

- 💳 **Payment Integration**
  - Stripe payment processing
  - Secure checkout
  - Order management

- 🔐 **Authentication**
  - Local authentication
  - Facebook login
  - Microsoft login

## Tech Stack

- **Backend**: ASP.NET Core 8.0
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Frontend**: 
  - Bootstrap 5
  - jQuery
  - TinyMCE
- **Payment**: Stripe
- **Authentication**: ASP.NET Core Identity
- **Deployment**: Docker, Render

## Prerequisites

- .NET 8.0 SDK
- PostgreSQL 16 (for deploy)
- Docker (optional)
- Visual Studio 2022 or VS Code

## Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/trongnp0706/Drsfan.git
   cd Drsfan
   ```

2. **Configure Database**
   - Install PostgreSQL
   - Create a database named "Drsfan"
   - Update connection string in `appsettings.Development.json`

3. **Run Migrations**
   ```bash
   dotnet ef database update --project Drsfan.DataAccess
   ```

4. **Run the Application**
   ```bash
   dotnet run --project DrsfanWebApp --environment Development
   ```

## Development Setup

1. **Install Dependencies**
   ```bash
   dotnet restore
   ```

2. **Configure Environment Variables**
   - Copy `appsettings.Development.json` to `appsettings.Development.json`
   - Update connection strings and API keys

3. **Run Tests**
   ```bash
   dotnet test
   ```

## Deployment

### Local Docker Deployment

1. **Build and Run with Docker Compose**
   ```bash
   docker-compose up -d
   ```

2. **Access the Application**
   - Web: http://localhost:10000
   - Database: localhost:5432

### Render Deployment

1. **Deploy Link**
   - Web: https://drsfan.onrender.com/


## Project Structure

```
Drsfan/
├── DrsfanWebApp/           # Web Application
├── Drsfan.DataAccess/      # Data Access Layer
├── Drsfan.Models/          # Domain Models
└── Drsfan.Utility/         # Utility Classes
```


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.


## Acknowledgments

- [Bootstrap](https://getbootstrap.com/)
- [Stripe](https://stripe.com/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
