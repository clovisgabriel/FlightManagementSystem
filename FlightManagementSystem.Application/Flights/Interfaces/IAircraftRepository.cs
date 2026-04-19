using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Application.Flights.Interfaces;

public interface IAircraftRepository
{
    Task<Aircraft?> GetByIdAsync(int id);
    Task<List<Aircraft>> GetAllAsync();
}