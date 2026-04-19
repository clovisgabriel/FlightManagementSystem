using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementSystem.Infrastructure.Persistence;

/// <summary>
/// Repository implementation for accessing Airport data from the database.
/// </summary>
public class AirportRepository : IAirportRepository
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="AirportRepository"/> class.
    /// </summary>
    /// <param name="db">Database context used for data access.</param>
    public AirportRepository(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Retrieves an airport by its unique identifier.
    /// </summary>
    /// <param name="id">Airport ID.</param>
    /// <returns>The airport if found; otherwise null.</returns>
    public Task<Airport?> GetByIdAsync(int id)
    {
        return _db.Airports.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Retrieves all airport records from the database.
    /// </summary>
    /// <returns>A list of all airports.</returns>
    public Task<List<Airport>> GetAllAsync()
    {
        return _db.Airports.ToListAsync();
    }
}