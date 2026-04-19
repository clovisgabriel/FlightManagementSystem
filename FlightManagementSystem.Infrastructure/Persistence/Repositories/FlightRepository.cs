using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementSystem.Infrastructure.Persistence;

/// <summary>
/// Repository implementation for accessing Flight data from the database.
/// </summary>
public class FlightRepository : IFlightRepository
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightRepository"/> class.
    /// </summary>
    /// <param name="db">Database context used for data access.</param>
    public FlightRepository(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Adds a new flight to the database context.
    /// Note: Changes are not persisted until SaveChangesAsync is called.
    /// </summary>
    /// <param name="flight">The flight entity to add.</param>
    public async Task AddAsync(Flight flight)
    {
        await _db.Flights.AddAsync(flight);
    }

    /// <summary>
    /// Updates an existing flight entity with new values.
    /// </summary>
    /// <param name="flight">The flight entity containing updated data.</param>
    /// <exception cref="Exception">Thrown when the flight does not exist.</exception>
    public async Task UpdateAsync(Flight flight)
    {
        var existing = await _db.Flights.FindAsync(flight.Id);

        if (existing == null)
            throw new Exception("Flight not found");

        existing.DepartureAirportId = flight.DepartureAirportId;
        existing.DestinationAirportId = flight.DestinationAirportId;
        existing.AircraftId = flight.AircraftId;
        existing.DistanceKm = flight.DistanceKm;
        existing.FuelRequired = flight.FuelRequired;
    }

    /// <summary>
    /// Retrieves all flights including related navigation properties.
    /// </summary>
    /// <returns>A list of flights with related data loaded.</returns>
    public Task<List<Flight>> GetAllAsync()
    {
        return _db.Flights
            .Include(x => x.DepartureAirport)
            .Include(x => x.DestinationAirport)
            .Include(x => x.Aircraft)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a flight by its ID including related airports.
    /// </summary>
    /// <param name="id">Flight identifier.</param>
    /// <returns>The flight if found, otherwise null.</returns>
    public Task<Flight?> GetByIdAsync(int id)
    {
        return _db.Flights
            .Include(f => f.DepartureAirport)
            .Include(f => f.DestinationAirport)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}