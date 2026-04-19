# Flight Management System

## Overview

This is a full-stack ASP.NET Core MVC application that manages flights between airports. Users can create, edit, view, and analyze flight data. The system calculates flight distance and fuel consumption based on airport GPS coordinates and aircraft specifications.

The project is designed with a clean and maintainable architecture, focusing on production-ready code practices.

---

## Features

- Create new flights (departure airport, destination airport, aircraft)
- Edit existing flights
- View all flights
- Automatic calculations:
  - Distance between airports (Haversine formula)
  - Fuel consumption per flight
- Flight report with aggregated statistics:
  - Total flights
  - Total and average distance
  - Fuel usage and estimated cost
  - Most used aircraft and airports
  - Longest and shortest flights

---

## Architecture

The solution follows a layered / clean architecture approach:

- **Web Layer (ASP.NET Core MVC)** → UI and controllers  
- **Application Layer** → Business logic (handlers, services)  
- **Domain Layer** → Core entities  
- **Infrastructure Layer** → Data access (Entity Framework Core)

---

## Technologies

- ASP.NET Core MVC
- C#
- Entity Framework Core
- LINQ
- SQL Server 

---

## Business Logic

- Distance is calculated using the Haversine formula based on airport coordinates
- Fuel consumption is calculated using:
  - Aircraft fuel consumption per km
  - Takeoff fuel requirement

---
## How to Run

1. Clone the repository
2. Open the solution in Visual Studio
3. Set `FlightManagementSystem.Web` as the startup project
4. Configure the database connection in `appsettings.json`
5. Run Entity Framework migrations (if not already applied): dotnet ef database update


<img width="1916" height="1017" alt="image" src="https://github.com/user-attachments/assets/01d49bd6-0245-497c-b7c1-817a970ab933" />

<img width="1918" height="1014" alt="image" src="https://github.com/user-attachments/assets/5ee338bc-0302-4fa4-a883-346c917e02dc" />

<img width="1919" height="1012" alt="image" src="https://github.com/user-attachments/assets/856d758c-872d-49f1-941d-c7eefb7142df" />

<img width="1534" height="852" alt="image" src="https://github.com/user-attachments/assets/885b1acf-1155-4b3d-8c4d-f9f1177a0836" />

