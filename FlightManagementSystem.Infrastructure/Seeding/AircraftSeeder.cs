using FlightManagementSystem.Domain.Entities;
using FlightManagementSystem.Infrastructure.Persistence;

namespace FlightManagementSystem.Infrastructure.Seeding;

public class AircraftSeeder : IDataSeeder
{
    public async Task SeedAsync(AppDbContext context)
    {
        if (context.Aircraft.Any())
            return;

        var aircraft = new List<Aircraft>
        {
            new() { Model = "Boeing 737-800", FuelConsumptionPerKm = 5.2, TakeoffFuel = 120 },
            new() { Model = "Boeing 747-400", FuelConsumptionPerKm = 10.5, TakeoffFuel = 300 },
            new() { Model = "Boeing 777-300ER", FuelConsumptionPerKm = 9.1, TakeoffFuel = 280 },
            new() { Model = "Boeing 787 Dreamliner", FuelConsumptionPerKm = 7.8, TakeoffFuel = 250 },

            new() { Model = "Airbus A320", FuelConsumptionPerKm = 4.8, TakeoffFuel = 110 },
            new() { Model = "Airbus A321", FuelConsumptionPerKm = 5.1, TakeoffFuel = 115 },
            new() { Model = "Airbus A330", FuelConsumptionPerKm = 8.2, TakeoffFuel = 240 },
            new() { Model = "Airbus A350", FuelConsumptionPerKm = 7.5, TakeoffFuel = 230 },

            new() { Model = "Embraer E190", FuelConsumptionPerKm = 3.2, TakeoffFuel = 80 },
            new() { Model = "Embraer E195-E2", FuelConsumptionPerKm = 3.5, TakeoffFuel = 85 }
        };

        context.Aircraft.AddRange(aircraft);
        await context.SaveChangesAsync();
    }
}