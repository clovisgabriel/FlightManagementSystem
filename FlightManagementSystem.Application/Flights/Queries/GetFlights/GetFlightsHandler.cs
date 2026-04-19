using FlightManagementSystem.Application.Flights.DTO.Responses;
using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Application.Flights.Queries.GetFlights;

/// <summary>
/// Handles retrieval of all flights from the system.
/// Maps domain Flight entities into FlightResponse DTOs for presentation.
/// </summary>
public class GetFlightsHandler
{
    private readonly IFlightRepository _repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFlightsHandler"/> class.
    /// </summary>
    /// <param name="repo">Repository used to access Flight data.</param>
    public GetFlightsHandler(IFlightRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Retrieves all flights and maps them into a list of response DTOs.
    /// </summary>
    /// <returns>A list of <see cref="FlightResponse"/> objects.</returns>
    public async Task<List<FlightResponse>> HandleAsync()
    {
        var flights = await _repo.GetAllAsync();

        return flights.Select(f => new FlightResponse
        {
            Id = f.Id,
            DepartureAirportId = f.DepartureAirportId,
            DepartureAirportName = f.DepartureAirport.Name,
            DepartureAirportCode = f.DepartureAirport.IcaoCode,
            DestinationAirportId = f.DestinationAirportId,
            DestinationAirportName = f.DestinationAirport.Name,
            DestinationAirportCode = f.DestinationAirport.IcaoCode,
            AircraftId = f.AircraftId,
            AircraftModel = f.Aircraft.Model,
            DistanceKm = f.DistanceKm,
            FuelRequired = f.FuelRequired
        }).ToList();
    }
}