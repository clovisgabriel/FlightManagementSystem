using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Application.Flights.Services;

/// <summary>
/// Provides calculation utilities for flight operations,
/// including distance between airports and fuel consumption estimation.
/// </summary>
public class FlightCalculator
{
    /// <summary>
    /// Earth's radius in kilometers used for Haversine distance calculation.
    /// </summary>
    private const double EarthRadiusKm = 6371;

    /// <summary>
    /// Calculates the great-circle distance between two airports using the Haversine formula.
    /// </summary>
    /// <param name="from">Origin airport.</param>
    /// <param name="to">Destination airport.</param>
    /// <returns>Distance in kilometers between the two airports.</returns>
    public double CalculateDistance(Airport from, Airport to)
    {
        double dLat = ToRad(to.Latitude - from.Latitude);
        double dLon = ToRad(to.Longitude - from.Longitude);

        double lat1 = ToRad(from.Latitude);
        double lat2 = ToRad(to.Latitude);

        double a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(lat1) * Math.Cos(lat2) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return EarthRadiusKm * c;
    }

    /// <summary>
    /// Calculates total fuel required for a flight.
    /// Includes aircraft takeoff fuel plus consumption based on distance.
    /// </summary>
    /// <param name="distanceKm">Flight distance in kilometers.</param>
    /// <param name="aircraft">Aircraft used for the flight.</param>
    /// <returns>Total fuel required.</returns>
    public double CalculateFuel(double distanceKm, Aircraft aircraft)
    {
        return aircraft.TakeoffFuel +
               distanceKm * aircraft.FuelConsumptionPerKm;
    }

    /// <summary>
    /// Converts degrees to radians.
    /// </summary>
    /// <param name="deg">Angle in degrees.</param>
    /// <returns>Angle in radians.</returns>
    private double ToRad(double deg) => deg * Math.PI / 180;
}