using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FlightManagementSystem.Application.Flights.Queries.GetFlights;
using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Application.Flights.DTO.Responses;
using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Tests.Application.Flights.Queries
{
    public class GetFlightsHandlerTests
    {
        private readonly Mock<IFlightRepository> _repo = new();

        private GetFlightsHandler CreateSUT()
        {
            return new GetFlightsHandler(_repo.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnEmptyList_WhenNoFlightsExist()
        {
            // Arrange
            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(new List<Flight>());

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task HandleAsync_ShouldMapFlightCorrectly()
        {
            // Arrange
            var flights = new List<Flight>
            {
                new Flight
                {
                    Id = 1,
                    DepartureAirportId = 10,
                    DestinationAirportId = 20,
                    AircraftId = 30,
                    DistanceKm = 150,
                    FuelRequired = 75,

                    DepartureAirport = new Airport
                    {
                        Name = "Lisbon",
                        IcaoCode = "LPPT"
                    },
                    DestinationAirport = new Airport
                    {
                        Name = "Porto",
                        IcaoCode = "LPPR"
                    },
                    Aircraft = new Aircraft
                    {
                        Model = "A320"
                    }
                }
            };

            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(flights);

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            var flight = result.First();

            Assert.Equal(1, flight.Id);

            Assert.Equal(10, flight.DepartureAirportId);
            Assert.Equal("Lisbon", flight.DepartureAirportName);
            Assert.Equal("LPPT", flight.DepartureAirportCode);

            Assert.Equal(20, flight.DestinationAirportId);
            Assert.Equal("Porto", flight.DestinationAirportName);
            Assert.Equal("LPPR", flight.DestinationAirportCode);

            Assert.Equal(30, flight.AircraftId);
            Assert.Equal("A320", flight.AircraftModel);

            Assert.Equal(150, flight.DistanceKm);
            Assert.Equal(75, flight.FuelRequired);
        }

        [Fact]
        public async Task HandleAsync_ShouldMapMultipleFlights()
        {
            // Arrange
            var flights = new List<Flight>
            {
                new Flight
                {
                    Id = 1,
                    DepartureAirport = new Airport { Name = "Lisbon", IcaoCode = "LPPT" },
                    DestinationAirport = new Airport { Name = "Porto", IcaoCode = "LPPR" },
                    Aircraft = new Aircraft { Model = "A320" },
                    DistanceKm = 100,
                    FuelRequired = 50
                },
                new Flight
                {
                    Id = 2,
                    DepartureAirport = new Airport { Name = "Madrid", IcaoCode = "LEMD" },
                    DestinationAirport = new Airport { Name = "Paris", IcaoCode = "LFPG" },
                    Aircraft = new Aircraft { Model = "B737" },
                    DistanceKm = 200,
                    FuelRequired = 90
                }
            };

            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(flights);

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, f => f.AircraftModel == "A320");
            Assert.Contains(result, f => f.AircraftModel == "B737");
        }
    }
}