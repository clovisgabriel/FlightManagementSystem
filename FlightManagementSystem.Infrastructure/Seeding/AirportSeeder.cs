using FlightManagementSystem.Domain.Entities;
using FlightManagementSystem.Infrastructure.Persistence;

namespace FlightManagementSystem.Infrastructure.Seeding;

public class AirportSeeder : IDataSeeder
{
    public async Task SeedAsync(AppDbContext context)
    {
        if (context.Airports.Any())
            return;

        var airports = new List<Airport>
        {
            new() { IcaoCode = "LIS", Name = "Lisbon Airport", Latitude = 38.7742, Longitude = -9.1342 },
            new() { IcaoCode = "LHR", Name = "Heathrow Airport", Latitude = 51.4700, Longitude = -0.4543 },
            new() { IcaoCode = "JFK", Name = "John F. Kennedy Airport", Latitude = 40.6413, Longitude = -73.7781 },
            new() { IcaoCode = "CDG", Name = "Charles de Gaulle Airport", Latitude = 49.0097, Longitude = 2.5479 },
            new() { IcaoCode = "FRA", Name = "Frankfurt Airport", Latitude = 50.0379, Longitude = 8.5622 },

            new() { IcaoCode = "MAD", Name = "Madrid Barajas Airport", Latitude = 40.4983, Longitude = -3.5676 },
            new() { IcaoCode = "BCN", Name = "Barcelona El Prat Airport", Latitude = 41.2974, Longitude = 2.0833 },
            new() { IcaoCode = "AMS", Name = "Amsterdam Schiphol Airport", Latitude = 52.3105, Longitude = 4.7683 },
            new() { IcaoCode = "FCO", Name = "Rome Fiumicino Airport", Latitude = 41.8003, Longitude = 12.2389 },
            new() { IcaoCode = "ZRH", Name = "Zurich Airport", Latitude = 47.4582, Longitude = 8.5555 },

            new() { IcaoCode = "DXB", Name = "Dubai International Airport", Latitude = 25.2532, Longitude = 55.3657 },
            new() { IcaoCode = "SIN", Name = "Singapore Changi Airport", Latitude = 1.3644, Longitude = 103.9915 },
            new() { IcaoCode = "HND", Name = "Tokyo Haneda Airport", Latitude = 35.5494, Longitude = 139.7798 },
            new() { IcaoCode = "LAX", Name = "Los Angeles Airport", Latitude = 33.9416, Longitude = -118.4085 },
            new() { IcaoCode = "GRU", Name = "São Paulo Guarulhos Airport", Latitude = -23.4356, Longitude = -46.4731 }
        };

        context.Airports.AddRange(airports);
        await context.SaveChangesAsync();
    }
}