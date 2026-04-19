using FlightManagementSystem.Application.Flights.DTO.Responses;
using FlightManagementSystem.Application.Flights.Interfaces;

namespace FlightManagementSystem.Application.Flights.Queries.GetAircraft;

/// <summary>
/// Handles retrieval of all Aircraft records.
/// </summary>
public class GetAircraftHandler
{
    private readonly IAircraftRepository _repo;

    public GetAircraftHandler(IAircraftRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<AircraftResponse>> HandleAsync()
    {
        var aircraft = await _repo.GetAllAsync();

        return aircraft.Select(a => new AircraftResponse
        {
            Id = a.Id,
            Model = a.Model
        }).ToList();
    }
}