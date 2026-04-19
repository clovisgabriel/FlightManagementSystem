using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Application.Flights.Interfaces;

public interface IFlightRepository
{
    Task<Flight?> GetByIdAsync(int id);
    Task<List<Flight>> GetAllAsync();
    Task AddAsync(Flight flight);
    Task UpdateAsync(Flight flight);
}