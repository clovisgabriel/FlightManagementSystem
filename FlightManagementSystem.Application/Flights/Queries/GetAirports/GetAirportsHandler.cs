using FlightManagementSystem.Application.Flights.DTO.Responses;
using FlightManagementSystem.Application.Flights.Interfaces;

namespace FlightManagementSystem.Application.Flights.Queries.GetAirports;

public class GetAirportsHandler
{
    private readonly IAirportRepository _repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAircraftHandler"/> class.
    /// </summary>
    /// <param name="repo">Repository used to access Aircraft data.</param>
    public GetAirportsHandler(IAirportRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Retrieves all aircraft from the repository and maps them into response DTOs.
    /// </summary>
    /// <returns>A list of <see cref="AircraftResponse"/> objects.</returns>
    public async Task<List<AirportResponse>> HandleAsync()
    {
        var airports = await _repo.GetAllAsync();

        return airports.Select(a => new AirportResponse
        {
            Id = a.Id,
            Name = a.Name
        }).ToList();
    }
}