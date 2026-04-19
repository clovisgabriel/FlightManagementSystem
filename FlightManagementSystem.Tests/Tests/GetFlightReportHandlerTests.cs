using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManagementSystem.Application.Flights.Queries.GetFlightReport;
using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Domain.Entities;
using FlightManagementSystem.Application.Flights.DTO.Responses;

namespace FlightManagementSystem.Tests.Application.Flights.Queries
{
    public class GetFlightReportHandlerTests
    {
        private readonly Mock<IFlightRepository> _repo = new();

        private GetFlightReportHandler CreateSUT()
        {
            return new GetFlightReportHandler(_repo.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnEmptyReport_WhenNoFlightsExist()
        {
            // Arrange
            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(new List<Flight>());

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.TotalFlights);
        }

        [Fact]
        public async Task HandleAsync_ShouldCalculateBasicTotalsCorrectly()
        {
            // Arrange
            var flights = new List<Flight>
            {
                new Flight
                {
                    DistanceKm = 100,
                    FuelRequired = 50,
                    Aircraft = new Aircraft { Model = "A320" },
                    DepartureAirport = new Airport { Name = "Lisbon" },
                    DestinationAirport = new Airport { Name = "Porto" }
                },
                new Flight
                {
                    DistanceKm = 200,
                    FuelRequired = 80,
                    Aircraft = new Aircraft { Model = "A320" },
                    DepartureAirport = new Airport { Name = "Lisbon" },
                    DestinationAirport = new Airport { Name = "Madrid" }
                }
            };

            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(flights);

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            Assert.Equal(2, result.TotalFlights);
            Assert.Equal(300, result.TotalDistanceKm);
            Assert.Equal(130, result.TotalFuelRequired);
            Assert.Equal(150, result.AverageDistanceKm);

            Assert.Equal(200, result.LongestFlightKm);
            Assert.Equal(100, result.ShortestFlightKm);
        }

        [Fact]
        public async Task HandleAsync_ShouldCalculateMostUsedAircraftAndAirports()
        {
            // Arrange
            var flights = new List<Flight>
            {
                new Flight
                {
                    AircraftId = 1,
                    Aircraft = new Aircraft { Model = "A320" },
                    DepartureAirportId = 1,
                    DepartureAirport = new Airport { Name = "Lisbon" },
                    DestinationAirportId = 2,
                    DestinationAirport = new Airport { Name = "Porto" },
                    DistanceKm = 100,
                    FuelRequired = 50
                },
                new Flight
                {
                    AircraftId = 1,
                    Aircraft = new Aircraft { Model = "A320" },
                    DepartureAirportId = 1,
                    DepartureAirport = new Airport { Name = "Lisbon" },
                    DestinationAirportId = 3,
                    DestinationAirport = new Airport { Name = "Madrid" },
                    DistanceKm = 200,
                    FuelRequired = 80
                }
            };

            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(flights);

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            Assert.Equal("A320", result.MostUsedAircraft);
            Assert.Equal("Lisbon", result.MostUsedDepartureAirport);
        }
    }
}