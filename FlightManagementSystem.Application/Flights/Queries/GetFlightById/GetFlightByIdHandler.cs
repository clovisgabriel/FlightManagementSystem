using FlightManagementSystem.Application.Flights.DTO.Responses;
using FlightManagementSystem.Application.Flights.Interfaces;

namespace FlightManagementSystem.Application.Flights.Queries.GetFlightById;

/// <summary>
/// Handles retrieval of a single Flight by its identifier.
/// </summary>
public class GetFlightByIdHandler
{
    private readonly IFlightRepository _repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFlightByIdHandler"/> class.
    /// </summary>
    /// <param name="repo">Repository used to access Flight data.</param>
    public GetFlightByIdHandler(IFlightRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Retrieves a flight by its unique identifier and maps it to a response DTO.
    /// </summary>
    /// <param name="id">The identifier of the flight to retrieve.</param>
    /// <returns>
    /// A <see cref="FlightResponse"/> object if the flight exists; otherwise null.
    /// </returns>
    public async Task<FlightResponse?> HandleAsync(int id)
    {
        var flight = await _repo.GetByIdAsync(id);

        if (flight == null)
            return null;

        return new FlightResponse
        {
            Id = flight.Id,
            DepartureAirportId = flight.DepartureAirportId,
            DepartureAirportName = flight.DepartureAirport.Name,
            DepartureAirportCode = flight.DepartureAirport.IcaoCode,
            DestinationAirportId = flight.DestinationAirportId,
            DestinationAirportCode = flight.DestinationAirport.IcaoCode,
            AircraftId = flight.AircraftId,
            DistanceKm = flight.DistanceKm,
            FuelRequired = flight.FuelRequired
        };
    }
}