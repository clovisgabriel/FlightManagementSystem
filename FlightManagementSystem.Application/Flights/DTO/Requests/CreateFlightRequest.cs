namespace FlightManagementSystem.Application.Flights.DTO.Requests;

public class CreateFlightRequest
{
    public int DepartureAirportId { get; set; }
    public int DestinationAirportId { get; set; }
    public int AircraftId { get; set; }
}