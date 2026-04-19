using FlightManagementSystem.Infrastructure.Persistence;

namespace FlightManagementSystem.Infrastructure.Seeding;

public class DatabaseSeeder
{
    private readonly IEnumerable<IDataSeeder> _seeders;

    public DatabaseSeeder(IEnumerable<IDataSeeder> seeders)
    {
        _seeders = seeders;
    }

    public async Task SeedAsync(AppDbContext context)
    {
        foreach (var seeder in _seeders)
        {
            await seeder.SeedAsync(context);    
        }
    }
}