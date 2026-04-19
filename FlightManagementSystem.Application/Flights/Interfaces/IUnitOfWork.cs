namespace FlightManagementSystem.Application.Flights.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}