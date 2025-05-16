Football League Project Documentation
1. Overview
This project is a football league management system implemented as a RESTful Web API using .NET 8 and Entity Framework Core, with MS SQL Server for data persistence (running in a Docker container).
It allows managing teams and their played matches, calculates rankings dynamically based on match results, and exposes endpoints for CRUD operations on teams and matches.

2. Architecture & Components
2.1 Entities
Team: Represents a football team with statistics (wins, losses, draws, points, goals, etc.).
Match: Represents a played match between two teams, including scores and match type.

2.2 Enums
MatchType: Enum with values:
`League = 1
Friendly = 2`

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
Microsoft.EntityFrameworkCore.SqlServer

Microsoft.EntityFrameworkCore.Tools

Microsoft.EntityFrameworkCore

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

7. Unit Testing Project
Located in a separate test project and a reference is added to it of the main project.

Uses:
MSTest as the test framework.
Moq for mocking dependencies.
FluentAssertions for readable assertions.

8. Summary
This football league system is a cleanly architected Web API using modern .NET practices. It follows SOLID principles, uses relevant design patterns, and separates concerns across layers.
With rankings auto-updated based on league match results and Dockerized SQL Server setup, it's easily deployable and testable.
