# 🎮 Senso Game - ASP.NET Core Web API + Razor Pages

This project is a web-based version of the **Senso game**, built with:

- ASP.NET Core Web API (C#)
- Razor Pages for interactive UI
- Swagger for API testing

Clean 3-layer design
Presentation Layer:
  Razor Pages, JavaScript

API Layer:
  Controllers (pure HTTP)

Business Logic Layer:
  IGameService + GameService (game rules)

Data Access Layer:
  IGameRepository + InMemoryGameRepository (storage)
  
Players can:
✅ Register  
✅ Start a new game  
✅ Watch the animated sequence playback  
✅ Click the colored quadrants to repeat the sequence  
✅ See their score

## ✨ Features

- REST API endpoints for managing players and game sessions
- Simple in-memory repository for game state
- Swagger UI (`/swagger`) for API exploration
- Visual game board with interactive colored quadrants (`/VisualGame`)
- Animated playback of sequences
- Score tracking

## 🚀 Getting Started

### Prerequisites

[.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later

### Running the Application

Clone the repository
dotnet restore
dotnet run
https://localhost:5001/swagger
https://localhost:5001/Game
https://localhost:5001/VisualGame

**🛠️ API Endpoints**
POST /api/user/register

POST /api/game/start

GET /api/game/{sessionId}/play-sequence

POST /api/game/{sessionId}/input

GET /api/game/{sessionId}/score

GET /api/color
