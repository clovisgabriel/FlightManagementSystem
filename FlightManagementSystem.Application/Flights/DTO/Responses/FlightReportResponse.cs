namespace FlightManagementSystem.Application.Flights.DTO.Responses;

public class FlightReportResponse
{
    public int TotalFlights { get; set; }
    public double TotalDistanceKm { get; set; }
    public double TotalFuelRequired { get; set; }
    public double AverageDistanceKm { get; set; }

    public string MostUsedAircraft { get; set; }

    public string MostUsedDepartureAirport { get; set; }
    public string MostUsedDestinationAirport { get; set; }

    public double LongestFlightKm { get; set; }
    public double ShortestFlightKm { get; set; }

    public double TotalFuelCost { get; set; }
}