using FlightManagementSystem.Application.Flights.DTO.Requests;
using FlightManagementSystem.Application.Flights.Services;
using FlightManagementSystem.Application.Flights.Interfaces;

namespace FlightManagementSystem.Application.Flights.Commands.UpdateFlight;

/// <summary>
/// Handles updating an existing Flight entity.
/// </summary>
public class UpdateFlightHandler
{
    private readonly IFlightRepository _flightRepo;
    private readonly IAirportRepository _airportRepo;
    private readonly IAircraftRepository _aircraftRepo;
    private readonly FlightCalculator _calculator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFlightHandler(
        IFlightRepository flightRepo,
        IAirportRepository airportRepo,
        IAircraftRepository aircraftRepo,
        FlightCalculator calculator,
        IUnitOfWork unitOfWork)
    {
        _flightRepo = flightRepo;
        _airportRepo = airportRepo;
        _aircraftRepo = aircraftRepo;
        _calculator = calculator;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Updates an existing flight with new data.
    /// </summary>
    /// <param name="flight">The update request containing modified flight data.</param>
    public async Task HandleAsync(UpdateFlightRequest flight)
    {
        var existing = await _flightRepo.GetByIdAsync(flight.Id);

        if (existing == null)
            throw new Exception("Flight not found");

        var from = await _airportRepo.GetByIdAsync(flight.DepartureAirportId);
        var to = await _airportRepo.GetByIdAsync(flight.DestinationAirportId);
        var aircraft = await _aircraftRepo.GetByIdAsync(flight.AircraftId);

        if (from == null || to == null || aircraft == null)
            throw new Exception("Invalid flight data");

        var distance = _calculator.CalculateDistance(from, to);
        var fuel = _calculator.CalculateFuel(distance, aircraft);

        existing.DepartureAirportId = flight.DepartureAirportId;
        existing.DestinationAirportId = flight.DestinationAirportId;
        existing.AircraftId = flight.AircraftId;

        existing.DistanceKm = distance;
        existing.FuelRequired = fuel;

        await _unitOfWork.SaveChangesAsync();
    }
}