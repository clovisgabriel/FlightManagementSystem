namespace FlightManagementSystem.Domain.Entities;

/// <summary>
/// Represents an aircraft used in flight operations.
/// </summary>
public class Aircraft
{
    /// <summary>
    /// Unique identifier of the aircraft.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Model name of the aircraft (e.g., Airbus A320, Boeing 737).
    /// </summary>
    public string Model { get; set; } = null!;

    /// <summary>
    /// Fuel consumption rate per kilometer (liters per km).
    /// Used in flight fuel calculation.
    /// </summary>
    public double FuelConsumptionPerKm { get; set; }

    /// <summary>
    /// Fixed amount of fuel required for takeoff.
    /// This value is always added regardless of flight distance.
    /// </summary>
    public double TakeoffFuel { get; set; }
}