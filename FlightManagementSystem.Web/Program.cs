using FlightManagementSystem.Application.Flights.Commands.CreateFlight;
using FlightManagementSystem.Application.Flights.Commands.UpdateFlight;
using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Application.Flights.Queries.GetAircraft;
using FlightManagementSystem.Application.Flights.Queries.GetAirports;
using FlightManagementSystem.Application.Flights.Queries.GetFlightById;
using FlightManagementSystem.Application.Flights.Queries.GetFlightReport;
using FlightManagementSystem.Application.Flights.Queries.GetFlights;
using FlightManagementSystem.Application.Flights.Services;
using FlightManagementSystem.Infrastructure.Persistence;
using FlightManagementSystem.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Application services
builder.Services.AddScoped<FlightCalculator>();

// Repositories
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IAirportRepository, AirportRepository>();
builder.Services.AddScoped<IAircraftRepository, AircraftRepository>();

// Handlers
builder.Services.AddScoped<CreateFlightHandler>();
builder.Services.AddScoped<UpdateFlightHandler>();
builder.Services.AddScoped<GetFlightsHandler>();
builder.Services.AddScoped<GetFlightByIdHandler>();
builder.Services.AddScoped<GetFlightReportHandler>();
builder.Services.AddScoped<GetAirportsHandler>();
builder.Services.AddScoped<GetAircraftHandler>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Seeding
builder.Services.AddScoped<IDataSeeder, AirportSeeder>();
builder.Services.AddScoped<IDataSeeder, AircraftSeeder>();
builder.Services.AddScoped<DatabaseSeeder>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Flight}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await seeder.SeedAsync(context);
}

app.Run();