using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Application.Flights.Interfaces;

public interface IAirportRepository
{
    Task<Airport?> GetByIdAsync(int id);
    Task<List<Airport>> GetAllAsync();
}
