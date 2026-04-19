namespace FlightManagementSystem.Domain.Entities;

/// <summary>
/// Represents an airport used in flight operations.
/// </summary>
public class Airport
{
    /// <summary>
    /// Unique identifier of the airport.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Code of the airport
    /// </summary>
    public string IcaoCode { get; set; } = null!;

    /// <summary>
    /// Human-readable name of the airport.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Geographic latitude of the airport location.
    /// Used for distance calculation between airports.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Geographic longitude of the airport location.
    /// Used for distance calculation between airports.
    /// </summary>
    public double Longitude { get; set; }
}