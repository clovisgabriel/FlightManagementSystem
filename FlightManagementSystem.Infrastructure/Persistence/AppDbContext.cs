using FlightManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementSystem.Infrastructure.Persistence;

/// <summary>
/// Represents the application's database context.
/// Responsible for configuring entity sets and relationships using Entity Framework Core.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </summary>
    /// <param name="options">Database context configuration options.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Represents the Flights table in the database.
    /// </summary>
    public DbSet<Flight> Flights => Set<Flight>();

    /// <summary>
    /// Represents the Airports table in the database.
    /// </summary>
    public DbSet<Airport> Airports => Set<Airport>();

    /// <summary>
    /// Represents the Aircraft table in the database.
    /// </summary>
    public DbSet<Aircraft> Aircraft => Set<Aircraft>();

    /// <summary>
    /// Configures entity relationships and database constraints.
    /// </summary>
    /// <param name="modelBuilder">The builder used to configure entity mappings.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Flight>()
            .HasOne(f => f.DepartureAirport)
            .WithMany()
            .HasForeignKey(f => f.DepartureAirportId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Flight>()
            .HasOne(f => f.DestinationAirport)
            .WithMany()
            .HasForeignKey(f => f.DestinationAirportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}