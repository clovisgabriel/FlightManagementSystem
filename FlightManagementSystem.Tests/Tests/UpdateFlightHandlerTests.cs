using Xunit;
using Moq;
using System.Threading.Tasks;
using FlightManagementSystem.Application.Flights.Commands.UpdateFlight;
using FlightManagementSystem.Application.Flights.DTO.Requests;
using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Application.Flights.Services;
using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Tests.Application.Flights.Commands
{
    public class UpdateFlightHandlerTests
    {
        private readonly Mock<IFlightRepository> _flightRepo = new();
        private readonly Mock<IAirportRepository> _airportRepo = new();
        private readonly Mock<IAircraftRepository> _aircraftRepo = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        private readonly FlightCalculator _calculator = new();

        private UpdateFlightHandler CreateSUT()
        {
            return new UpdateFlightHandler(
                _flightRepo.Object,
                _airportRepo.Object,
                _aircraftRepo.Object,
                _calculator,
                _unitOfWork.Object
            );
        }

        [Fact]
        public async Task HandleAsync_ShouldThrow_WhenFlightNotFound()
        {
            // Arrange
            var request = new UpdateFlightRequest
            {
                Id = 1,
                DepartureAirportId = 1,
                DestinationAirportId = 2,
                AircraftId = 3
            };

            _flightRepo.Setup(r => r.GetByIdAsync(1))
                       .ReturnsAsync((Flight)null);

            var handler = CreateSUT();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.HandleAsync(request));
        }

        [Fact]
        public async Task HandleAsync_ShouldThrow_WhenDataIsInvalid()
        {
            // Arrange
            var request = new UpdateFlightRequest
            {
                Id = 1,
                DepartureAirportId = 1,
                DestinationAirportId = 2,
                AircraftId = 3
            };

            var existing = new Flight { Id = 1 };

            _flightRepo.Setup(r => r.GetByIdAsync(1))
                       .ReturnsAsync(existing);

            _airportRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                        .ReturnsAsync((Airport)null);

            _aircraftRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync(new Aircraft());

            var handler = CreateSUT();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.HandleAsync(request));
        }

        [Fact]
        public async Task HandleAsync_ShouldUpdateFlight_WhenDataIsValid()
        {
            // Arrange
            var request = new UpdateFlightRequest
            {
                Id = 1,
                DepartureAirportId = 1,
                DestinationAirportId = 2,
                AircraftId = 3
            };

            var existing = new Flight
            {
                Id = 1,
                DepartureAirportId = 10,
                DestinationAirportId = 20,
                AircraftId = 30
            };

            var from = new Airport { Id = 1, Latitude = 38, Longitude = -9 };
            var to = new Airport { Id = 2, Latitude = 41, Longitude = -8 };

            var aircraft = new Aircraft
            {
                Id = 3,
                TakeoffFuel = 500,
                FuelConsumptionPerKm = 2
            };

            _flightRepo.Setup(r => r.GetByIdAsync(1))
                       .ReturnsAsync(existing);

            _airportRepo.Setup(r => r.GetByIdAsync(1))
                        .ReturnsAsync(from);

            _airportRepo.Setup(r => r.GetByIdAsync(2))
                        .ReturnsAsync(to);

            _aircraftRepo.Setup(r => r.GetByIdAsync(3))
                         .ReturnsAsync(aircraft);

            _unitOfWork.Setup(u => u.SaveChangesAsync())
                       .Returns(Task.CompletedTask);

            var handler = CreateSUT();

            // Act
            await handler.HandleAsync(request);

            // Assert
            Assert.Equal(1, existing.DepartureAirportId);
            Assert.Equal(2, existing.DestinationAirportId);
            Assert.Equal(3, existing.AircraftId);

            Assert.True(existing.DistanceKm > 0);
            Assert.True(existing.FuelRequired > 0);

            _unitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}