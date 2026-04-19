namespace FlightManagementSystem.Application.Flights.DTO.Requests;

public class UpdateFlightRequest
{
    public int Id { get; set; }
    public int DepartureAirportId { get; set; }
    public int DestinationAirportId { get; set; }
    public int AircraftId { get; set; }
}