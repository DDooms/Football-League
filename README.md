Football League Project Documentation

1. Overview

This project is a football league management system implemented as a RESTful Web API using .NET 8 and Entity Framework Core, with MS SQL Server for data persistence (running in a Docker container).

It allows managing teams and their played matches, calculates rankings dynamically based on match results, and exposes endpoints for CRUD operations on teams and matches.

Matches API
This API is responsible for managing "Matches" resources. 

`GET /api/Matches: This endpoint retrieves a list of all available matches. 
POST /api/Matches: This endpoint is used to create a new match. 
GET /api/Matches/{id}: This endpoint retrieves a specific match by its unique identifier (id). 
PUT /api/Matches/{id}: This endpoint is used to update an existing match identified by its id. 
DELETE /api/Matches/{id}: This endpoint deletes a specific match identified by its id. `

Ranking API
This API deals with the league table.

`GET /api/Ranking: This endpoint retrieves the current league table, where teams are ranked by points.`

Teams API
This API manages "Teams" resources.

`GET /api/Teams: This endpoint retrieves a list of all teams. 
POST /api/Teams: This endpoint is used to create a new team. 
GET /api/Teams/{id}: This endpoint retrieves a specific team by its unique identifier (id). 
PUT /api/Teams/{id}: This endpoint is used to update an existing team identified by its id. 
DELETE /api/Teams/{id}: This endpoint deletes a specific team identified by its id. `

2. Architecture & Components

2.1 Entities

Team: Represents a football team with statistics (wins, losses, draws, points, goals, etc.).

Match: Represents a played match between two teams, including scores and match type.

2.2 Enums

MatchType: Enum with values:

`League = 1`

`Friendly = 2`

2.3 Data Access Layer

ApplicationDbContext: Entity Framework Core DbContext managing entities and the database connection.

Repositories:

ITeamRepository, IMatchRepository: Interfaces for abstracting data access.

Concrete implementations handle communication with the database.

2.4 Business Logic Layer

Services:

TeamService, MatchService, RankingService: Contain the business logic.

Strategy Pattern:

LeagueMatchStrategy: Updates team stats for league matches.

FriendlyMatchStrategy: Does not affect team stats.

RankingService:

Calculates team rankings based only on league matches.

2.5 API Layer

Controllers: Provide RESTful endpoints for managing teams and matches.

DTOs: Abstractions for API communication (MatchResponseDTO, CreateMatchDTO, RankingDTO, etc.).

3. Design Patterns Used

Strategy Pattern: Handles different match processing logic by match type.

Builder Pattern: Used for clean and modular DTO creation (e.g. RankingDTOBuilder).

Repository Pattern: Separates database access logic from business logic.

4. Rankings Calculation Logic

Only League matches (MatchType.League) are considered.

Rankings are calculated based on:

Points: 3 for win, 1 for draw, 0 for loss.

Goal difference = goals scored − goals conceded.

Matches played, wins, draws, losses, goals.

Friendly matches are ignored in the ranking calculation.

Results are ordered by points, then goal difference.

5. Exception Handling

Implemented in the service layer using try-catch blocks to manage:

Invalid team or match references.

Business rule violations (e.g., team does not exist).

Returns appropriate HTTP status codes and messages to the controller layer.

6. How to Run the Project
6.1 Prerequisites
   
.NET 8 SDK

Docker Desktop

DataGrip for DB UI

6.2 Setup Instructions
Clone the repository:

Install dependencies:
`Microsoft.EntityFrameworkCore.SqlServer`

`Microsoft.EntityFrameworkCore.Tools`

`Microsoft.EntityFrameworkCore`

Start SQL Server in Docker (make sure Docker is running) (cmd command):

`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YOUR_PASSWORD" -p 1433:1433 --name sqlcontainer -d mcr.microsoft.com/mssql/server:2022-latest`

Update appsettings.json with the connection string:

`"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=YOUR_DATABASE_NAME;User Id=sa;Password=YOUR_PASSWORD;"
}`

Create and apply migrations:

`dotnet ef create migrations FirstMigration
dotnet ef database update`

Run the project (UI Swagger should open):

`dotnet run`

Swagger UI for API testing:

I have run the project using Http (Kestrel)

When started, Swagger UI opens automatically in your browser.

You can use Swagger to test all available endpoints—no need for Postman.

7. Unit Testing Project

Located in a separate test project and a reference is added to it of the main project.

Install dependencies:
`MSTest`

`Moq`

`FluentAssertions`

8. Summary

This football league system is a cleanly architected Web API using modern .NET practices. It follows SOLID principles, uses relevant design patterns, and separates concerns across layers. With rankings auto-updated based on league match results, Swagger integration for testing, and a Dockerized SQL Server, it's easily deployable, testable, and maintainable.
