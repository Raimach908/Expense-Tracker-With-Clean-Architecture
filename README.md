# Expense Tracker With Clean Architecture

## Table of Contents
1. [Overview](#overview)
2. [Features](#features)
3. [Technologies Used](#technologies-used)
4. [Architecture](#architecture)
5. [Setup and Installation](#setup-and-installation)
6. [Usage](#usage)
7. [Project Structure](#project-structure)
8. [Contributing](#contributing)
9. [License](#license)

---

## Overview

**Expense Tracker With Clean Architecture** is a web-based application that allows users to track their expenses, manage budgets, and view financial reports. The application is built following the Clean Architecture principles to ensure modularity, scalability, and maintainability.

---

## Features

- **User Authentication**: Users can register, log in, and manage their profiles.
- **Expense Management**: Add, edit, and delete expenses with detailed descriptions.
- **Income Management**: Track multiple income sources and manage budgets efficiently.
- **Reporting**: Generate daily, monthly, and yearly financial reports.
- **Profile Management**: Update user details and profile picture functionality.
- **Clean Architecture**: Separation of concerns with distinct layers for business logic, infrastructure, and UI.

---

## Technologies Used

- **Frontend**:
  - ASP.NET MVC
  - HTML5, CSS3, Bootstrap
  - JavaScript

- **Backend**:
  - ASP.NET Core
  - C#

- **Database**:
  - SQL Server (MDF/LDF Files)

- **Dependency Injection**:
  - Built-in Dependency Injection in ASP.NET Core

- **Unit Testing**:
  - MSTest or NUnit (optional for extending the project)

---

## Architecture

This project is built using **Clean Architecture**, which separates the application into distinct layers:
- **Core Layer**:
  - Contains the business logic and domain entities.
  - Interfaces for repositories are defined here.

- **Application Layer**:
  - Contains use cases that implement business logic.
  - Coordinates between the UI and Core layers.

- **Infrastructure Layer**:
  - Implements data access using repositories.
  - Connects the application to external services like databases.

- **Web Layer**:
  - The UI layer implemented using ASP.NET MVC.

---

## Project Structure

The solution is structured into the following projects:

### 1. **ExpenseTracker.Core**
   - **Entities**: Contains the business entities like `UserEntity`, `ExpenseEntity`, etc.
   - **Interfaces**: Defines the interfaces for repositories and services.

### 2. **ExpenseTracker.Application**
   - **Use Cases**: Contains the application logic implemented as use cases.
   - **Model Binding**: Handles model binding between application layers.

### 3. **ExpenseTracker.Infrastructure**
   - **Repositories**: Implementation of repository interfaces using database access logic.

### 4. **MyExpenseTracker.Web**
   - **Controllers**: Handles HTTP requests and connects them to the respective use cases.
   - **Views**: Contains the Razor Views for the frontend.
   - **Models**: Defines the models used in the MVC pattern.
   - **wwwroot**: Contains static files like CSS, JS, and images.
   - **appsettings.json**: Configuration settings for the application.
   - **Program.cs**: Entry point of the application.

---

## Setup and Installation

### Prerequisites

- **Visual Studio 2022** or later
- **.NET 6.0 SDK** or later
- **SQL Server** (LocalDB or full version)

---

### Steps

#### 1. Clone the repository:

```bash
git clone https://github.com/Raimach908/Expense-Tracker-With-Clean-Architecture.git
cd Expense-Tracker-With-Clean-Architecture
