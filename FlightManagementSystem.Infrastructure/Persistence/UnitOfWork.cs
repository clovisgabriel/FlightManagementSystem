using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Infrastructure.Persistence;

/// <summary>
/// Implements the Unit of Work pattern for coordinating database operations.
/// Ensures that all changes are saved as a single atomic transaction.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="db">The application's database context.</param>
    public UnitOfWork(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Persists all changes made in the current DbContext to the database.
    /// </summary>
    /// <returns>A task representing the asynchronous save operation.</returns>
    public Task SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}