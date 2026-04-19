namespace FlightManagementSystem.Application.Flights.DTO.Responses;

public class FlightResponse
{
    public int Id { get; set; }

    public int DepartureAirportId { get; set; }
    public string DepartureAirportName { get; set; } = string.Empty;
    public string DepartureAirportCode { get; set; } = string.Empty;

    public int DestinationAirportId { get; set; }
    public string DestinationAirportName { get; set; } = string.Empty;
    public string DestinationAirportCode { get; set; } = string.Empty;

    public int AircraftId { get; set; }
    public string AircraftModel { get; set; } = string.Empty;

    public double DistanceKm { get; set; }
    public double FuelRequired { get; set; }
}