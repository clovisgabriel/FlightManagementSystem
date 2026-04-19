namespace FlightManagementSystem.Domain.Entities;

/// <summary>
/// Represents a flight between two airports using a specific aircraft.
/// Contains route information, calculated metrics, and navigation properties.
/// </summary>
public class Flight
{
    /// <summary>
    /// Unique identifier of the flight.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Foreign key referencing the departure airport.
    /// </summary>
    public int DepartureAirportId { get; set; }

    /// <summary>
    /// Navigation property for the departure airport.
    /// </summary>
    public Airport DepartureAirport { get; set; } = null!;

    /// <summary>
    /// Foreign key referencing the destination airport.
    /// </summary>
    public int DestinationAirportId { get; set; }

    /// <summary>
    /// Navigation property for the destination airport.
    /// </summary>
    public Airport DestinationAirport { get; set; } = null!;

    /// <summary>
    /// Foreign key referencing the aircraft used for the flight.
    /// </summary>
    public int AircraftId { get; set; }

    /// <summary>
    /// Navigation property for the aircraft used in the flight.
    /// </summary>
    public Aircraft Aircraft { get; set; } = null!;

    /// <summary>
    /// Total distance of the flight in kilometers.
    /// Calculated using the FlightCalculator service.
    /// </summary>
    public double DistanceKm { get; set; }

    /// <summary>
    /// Total fuel required for the flight.
    /// Includes takeoff fuel and distance-based consumption.
    /// </summary>
    public double FuelRequired { get; set; }

    /// <summary>
    /// Timestamp indicating when the flight was created.
    /// Stored in UTC.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}