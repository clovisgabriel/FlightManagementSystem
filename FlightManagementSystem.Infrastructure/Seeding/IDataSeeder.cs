using FlightManagementSystem.Infrastructure.Persistence;

namespace FlightManagementSystem.Infrastructure.Seeding;

public interface IDataSeeder
{
    Task SeedAsync(AppDbContext context);
}