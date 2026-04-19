using Xunit;
using FlightManagementSystem.Application.Flights.Services;
using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Tests.Application.Flights.Services
{
    public class FlightCalculatorTests
    {
        private readonly FlightCalculator _calculator = new FlightCalculator();

        [Fact]
        public void CalculateDistance_ShouldReturnZero_WhenSameAirport()
        {
            // Arrange
            var airport = new Airport
            {
                Latitude = 41.0,
                Longitude = -8.0
            };

            // Act
            var result = _calculator.CalculateDistance(airport, airport);

            // Assert
            Assert.Equal(0, result, 2); // precision 2 decimals
        }

        [Fact]
        public void CalculateDistance_ShouldReturnPositiveValue_WhenDifferentAirports()
        {
            // Arrange (Lisbon -> Porto approx)
            var from = new Airport
            {
                Latitude = 38.7223,
                Longitude = -9.1393
            };

            var to = new Airport
            {
                Latitude = 41.1579,
                Longitude = -8.6291
            };

            // Act
            var result = _calculator.CalculateDistance(from, to);

            // Assert
            Assert.True(result > 200);   // Lisbon–Porto ~313 km
            Assert.True(result < 400);
        }

        [Fact]
        public void CalculateFuel_ShouldCalculateCorrectly()
        {
            // Arrange
            var aircraft = new Aircraft
            {
                TakeoffFuel = 500,
                FuelConsumptionPerKm = 2
            };

            double distance = 100;

            // Act
            var result = _calculator.CalculateFuel(distance, aircraft);

            // Expected: 500 + (100 * 2) = 700
            // Assert
            Assert.Equal(700, result);
        }

        [Fact]
        public void CalculateFuel_ShouldReturnOnlyTakeoffFuel_WhenDistanceIsZero()
        {
            // Arrange
            var aircraft = new Aircraft
            {
                TakeoffFuel = 400,
                FuelConsumptionPerKm = 3
            };

            // Act
            var result = _calculator.CalculateFuel(0, aircraft);

            // Assert
            Assert.Equal(400, result);
        }
    }
}