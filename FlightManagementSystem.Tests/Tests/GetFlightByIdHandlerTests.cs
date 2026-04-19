using Xunit;
using Moq;
using System.Threading.Tasks;
using FlightManagementSystem.Application.Flights.Queries.GetFlightById;
using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Application.Flights.DTO.Responses;
using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Tests.Application.Flights.Queries
{
    public class GetFlightByIdHandlerTests
    {
        private readonly Mock<IFlightRepository> _repo = new();

        private GetFlightByIdHandler CreateSUT()
        {
            return new GetFlightByIdHandler(_repo.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnNull_WhenFlightNotFound()
        {
            // Arrange
            _repo.Setup(r => r.GetByIdAsync(1))
                 .ReturnsAsync((Flight)null);

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnMappedFlight_WhenExists()
        {
            // Arrange
            var flight = new Flight
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
                Aircraft = new Aircraft()
            };

            _repo.Setup(r => r.GetByIdAsync(1))
                 .ReturnsAsync(flight);

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync(1);

            // Assert
            Assert.NotNull(result);

            Assert.Equal(1, result!.Id);

            Assert.Equal(10, result.DepartureAirportId);
            Assert.Equal("Lisbon", result.DepartureAirportName);
            Assert.Equal("LPPT", result.DepartureAirportCode);

            Assert.Equal(20, result.DestinationAirportId);
            Assert.Equal("LPPR", result.DestinationAirportCode);

            Assert.Equal(30, result.AircraftId);

            Assert.Equal(150, result.DistanceKm);
            Assert.Equal(75, result.FuelRequired);
        }
    }
}