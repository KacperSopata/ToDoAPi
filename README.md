# 📝 ToDoApi (.NET 8 + MySQL)

A simple REST API for managing ToDo tasks, built with ASP.NET Core 8, Entity Framework Core, and MySQL. The project includes full CRUD functionality, task filtering, percent completion, unit tests, and optional Docker support.

## ✅ Features

- Get all ToDo items (`GET /api/ToDo`)
- Get a specific ToDo by ID
- Get incoming ToDos:
  - for today
  - for tomorrow
  - for the current week
- Create new ToDo
- Update existing ToDo
- Set percent completion
- Mark as done
- Delete ToDo

## ⚙️ Technologies

- ASP.NET Core 8 (Web API)
- Entity Framework Core
- MySQL / MariaDB
- xUnit & Moq (unit testing)
- Swagger (OpenAPI)
- Docker support (optional)
- RESTful architecture

## 📁 Project Structure

📦 ToDoApi
 ┣ 📁 Controllers
 ┣ 📁 Models
 ┣ 📁 Repositories
 ┣ 📁 Services
 ┣ 📁 Data
 ┣ 📁 Tests
 ┣ 🐳 Dockerfile (optional)
 ┣ appsettings.json
 ┣ Program.cs
