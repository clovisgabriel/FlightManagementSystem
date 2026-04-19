using FlightManagementSystem.Application.Flights.Commands.CreateFlight;
using FlightManagementSystem.Application.Flights.Commands.UpdateFlight;
using FlightManagementSystem.Application.Flights.DTO.Requests;
using FlightManagementSystem.Application.Flights.Queries.GetAircraft;
using FlightManagementSystem.Application.Flights.Queries.GetAirports;
using FlightManagementSystem.Application.Flights.Queries.GetFlightById;
using FlightManagementSystem.Application.Flights.Queries.GetFlightReport;
using FlightManagementSystem.Application.Flights.Queries.GetFlights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManagementSystem.Web.Controllers;

/// <summary>
/// MVC controller responsible for managing Flight-related operations.
/// </summary>
public class FlightsController : Controller
{
    private readonly CreateFlightHandler _createFlight;
    private readonly UpdateFlightHandler _updateFlight;
    private readonly GetFlightsHandler _getFlights;
    private readonly GetFlightByIdHandler _getById;
    private readonly GetFlightReportHandler _getReport;
    private readonly GetAirportsHandler _getAirports;
    private readonly GetAircraftHandler _getAircraft;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightsController"/> class.
    /// </summary>
    public FlightsController(
        CreateFlightHandler createFlight,
        UpdateFlightHandler updateFlight,
        GetFlightsHandler getFlights,
        GetFlightByIdHandler getById,
        GetFlightReportHandler getReport,
        GetAirportsHandler getAirports,
        GetAircraftHandler getAircraft)
    {
        _createFlight = createFlight;
        _updateFlight = updateFlight;
        _getFlights = getFlights;
        _getById = getById;
        _getReport = getReport;
        _getAirports = getAirports;
        _getAircraft = getAircraft;
    }

    /// <summary>
    /// Displays all flights in the system.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var flights = await _getFlights.HandleAsync();
        return View(flights);
    }

    /// <summary>
    /// Displays the flight creation form.
    /// Loads Airports and Aircraft data for dropdown selection.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var airports = await _getAirports.HandleAsync();  
        var aircraft = await _getAircraft.HandleAsync(); 

        ViewBag.Airports = airports.Select(a => new SelectListItem
        {
            Value = a.Id.ToString(),
            Text = a.Name
        }).ToList();

        ViewBag.Aircraft = aircraft.Select(a => new SelectListItem
        {
            Value = a.Id.ToString(),
            Text = a.Model
        }).ToList();

        return View();
    }

    /// <summary>
    /// Handles flight creation submission.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateFlightRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        await _createFlight.HandleAsync(request);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Displays the flight edit form.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var flight = await _getById.HandleAsync(id);

        if (flight == null)
            return NotFound();

        var airports = await _getAirports.HandleAsync();
        var aircraft = await _getAircraft.HandleAsync();

        ViewBag.Airports = airports.Select(a => new SelectListItem
        {
            Value = a.Id.ToString(),
            Text = a.Name
        }).ToList();

        ViewBag.Aircraft = aircraft.Select(a => new SelectListItem
        {
            Value = a.Id.ToString(),
            Text = a.Model
        }).ToList();

        var request = new UpdateFlightRequest
        {
            Id = flight.Id,
            DepartureAirportId = flight.DepartureAirportId,
            DestinationAirportId = flight.DestinationAirportId,
            AircraftId = flight.AircraftId
        };

        return View(request);
    }

    /// <summary>
    /// Handles flight update submission.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateFlightRequest request)
    {
        if (!ModelState.IsValid)
            return View(request);

        await _updateFlight.HandleAsync(request);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Displays details of a specific flight.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var flight = await _getById.HandleAsync(id);

        if (flight == null)
            return NotFound();

        return View(flight);
    }

    /// <summary>
    /// Displays aggregated flight statistics report.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Report()
    {
        var report = await _getReport.HandleAsync();
        return View(report);
    }
}