using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementSystem.Infrastructure.Persistence;

/// <summary>
/// Repository implementation for accessing Aircraft data from the database.
/// </summary>
public class AircraftRepository : IAircraftRepository
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="AircraftRepository"/> class.
    /// </summary>
    /// <param name="db">Database context used for data access.</param>
    public AircraftRepository(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Retrieves an aircraft by its unique identifier.
    /// </summary>
    /// <param name="id">Aircraft identifier.</param>
    /// <returns>The aircraft if found, otherwise null.</returns>
    public Task<Aircraft?> GetByIdAsync(int id)
    {
        return _db.Aircraft.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Retrieves all aircraft records from the database.
    /// </summary>
    /// <returns>A list of all aircraft.</returns>
    public Task<List<Aircraft>> GetAllAsync()
    {
        return _db.Aircraft.ToListAsync();
    }
}