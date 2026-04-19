using FlightManagementSystem.Application.Flights.DTO.Responses;
using FlightManagementSystem.Application.Flights.Interfaces;

namespace FlightManagementSystem.Application.Flights.Queries.GetFlightReport;

/// <summary>
/// Handles generation of aggregated flight reports.
/// </summary>
public class GetFlightReportHandler
{
    private readonly IFlightRepository _flightRepo;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFlightReportHandler"/> class.
    /// </summary>
    /// <param name="flightRepo">Repository used to access Flight data.</param>
    public GetFlightReportHandler(IFlightRepository flightRepo)
    {
        _flightRepo = flightRepo;
    }

    /// <summary>
    /// Generates a complete flight report based on all stored flights.
    /// </summary>
    /// <remarks>
    /// The report includes:
    /// - Total flights
    /// - Total and average distance
    /// - Total fuel consumption and estimated cost
    /// - Most used aircraft and airports
    /// - Longest and shortest flights
    /// </remarks>
    /// <returns>A <see cref="FlightReportResponse"/> containing aggregated statistics.</returns>
    public async Task<FlightReportResponse> HandleAsync()
    {
        // Retrieve all flights from repository
        var flights = await _flightRepo.GetAllAsync();

        // Return empty report if no flights exist
        if (!flights.Any())
            return new FlightReportResponse();

        // Calculate total distance of all flights
        var totalDistance = flights.Sum(f => f.DistanceKm);

        // Calculate total fuel consumption
        var totalFuel = flights.Sum(f => f.FuelRequired);

        // Determine most used aircraft based on usage frequency
        var mostUsedAircraft = flights
            .GroupBy(f => new { f.AircraftId, f.Aircraft.Model })
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key.Model)
            .FirstOrDefault();

        // Determine most used departure airport
        var mostUsedDeparture = flights
            .GroupBy(f => new { f.DepartureAirportId, f.DepartureAirport.Name })
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key.Name)
            .FirstOrDefault();

        // Determine most used destination airport
        var mostUsedDestination = flights
            .GroupBy(f => new { f.DestinationAirportId, f.DestinationAirport.Name })
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key.Name)
            .FirstOrDefault();

        // Find longest flight distance
        var longest = flights.Max(f => f.DistanceKm);

        // Find shortest flight distance
        var shortest = flights.Min(f => f.DistanceKm);

        // Build and return final report DTO
        return new FlightReportResponse
        {
            TotalFlights = flights.Count(),
            TotalDistanceKm = totalDistance,
            TotalFuelRequired = totalFuel,
            AverageDistanceKm = totalDistance / flights.Count(),

            MostUsedAircraft = mostUsedAircraft.ToString(),
            MostUsedDepartureAirport = mostUsedDeparture.ToString(),
            MostUsedDestinationAirport = mostUsedDestination.ToString(),

            LongestFlightKm = longest,
            ShortestFlightKm = shortest,

            TotalFuelCost = totalFuel * 1.75
        };
    }
}