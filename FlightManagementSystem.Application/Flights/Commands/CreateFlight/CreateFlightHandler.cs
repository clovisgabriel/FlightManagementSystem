using FlightManagementSystem.Application.Flights.DTO.Requests;
using FlightManagementSystem.Application.Flights.Services;
using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Application.Flights.Commands.CreateFlight;

/// <summary>
/// Handles the creation of a new Flight entity.
/// </summary>
public class CreateFlightHandler
{
    private readonly IFlightRepository _flightRepo;
    private readonly IAirportRepository _airportRepo;
    private readonly IAircraftRepository _aircraftRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly FlightCalculator _calculator;

    public CreateFlightHandler(
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
    /// Initializes a new instance of the <see cref="CreateFlightHandler"/> class.
    /// </summary>
    /// <param name="flightRepo">Repository for managing Flight entities.</param>
    /// <param name="airportRepo">Repository for accessing Airport data.</param>
    /// <param name="aircraftRepo">Repository for accessing Aircraft data.</param>
    /// <param name="calculator">Service responsible for distance and fuel calculations.</param>
    /// <param name="unitOfWork">Unit of Work abstraction for committing changes.</param>
    public async Task HandleAsync(CreateFlightRequest createFlightRequest)
    {
        var from = await _airportRepo.GetByIdAsync(createFlightRequest.DepartureAirportId);
        var to = await _airportRepo.GetByIdAsync(createFlightRequest.DestinationAirportId);
        var aircraft = await _aircraftRepo.GetByIdAsync(createFlightRequest.AircraftId);

        if (from == null || to == null || aircraft == null)
            throw new Exception("Invalid flight data");

        var distance = _calculator.CalculateDistance(from, to);
        var fuel = _calculator.CalculateFuel(distance, aircraft);

        var flight = new Flight
        {
            DepartureAirportId = createFlightRequest.DepartureAirportId,
            DestinationAirportId = createFlightRequest.DestinationAirportId,
            AircraftId = createFlightRequest.AircraftId,
            DistanceKm = distance,
            FuelRequired = fuel
        };

        await _flightRepo.AddAsync(flight); 
        await _unitOfWork.SaveChangesAsync();
    }
}