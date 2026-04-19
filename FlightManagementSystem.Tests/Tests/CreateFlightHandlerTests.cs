using Xunit;
using Moq;
using System.Threading.Tasks;
using FlightManagementSystem.Application.Flights.Commands.CreateFlight;
using FlightManagementSystem.Application.Flights.DTO.Requests;
using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Application.Flights.Services;
using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Tests.Application.Flights.Commands
{
    public class CreateFlightHandlerTests
    {
        private readonly Mock<IFlightRepository> _flightRepo = new();
        private readonly Mock<IAirportRepository> _airportRepo = new();
        private readonly Mock<IAircraftRepository> _aircraftRepo = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        private readonly FlightCalculator _calculator = new FlightCalculator();

        private CreateFlightHandler CreateSUT()
        {
            return new CreateFlightHandler(
                _flightRepo.Object,
                _airportRepo.Object,
                _aircraftRepo.Object,
                _calculator,
                _unitOfWork.Object
            );
        }

        [Fact]
        public async Task HandleAsync_ShouldCreateFlight_WhenDataIsValid()
        {
            // Arrange
            var request = new CreateFlightRequest
            {
                DepartureAirportId = 1,
                DestinationAirportId = 2,
                AircraftId = 3
            };

            var from = new Airport { Id = 1, Latitude = 38, Longitude = -9 };
            var to = new Airport { Id = 2, Latitude = 41, Longitude = -8 };

            var aircraft = new Aircraft
            {
                Id = 3,
                TakeoffFuel = 500,
                FuelConsumptionPerKm = 2
            };

            _airportRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(from);
            _airportRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(to);
            _aircraftRepo.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(aircraft);

            Flight? savedFlight = null;

            _flightRepo
                .Setup(r => r.AddAsync(It.IsAny<Flight>()))
                .Callback<Flight>(f => savedFlight = f)
                .Returns(Task.CompletedTask);

            _unitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

            var handler = CreateSUT();

            // Act
            await handler.HandleAsync(request);

            // Assert
            Assert.NotNull(savedFlight);
            Assert.Equal(1, savedFlight!.DepartureAirportId);
            Assert.Equal(2, savedFlight.DestinationAirportId);
            Assert.Equal(3, savedFlight.AircraftId);
            Assert.True(savedFlight.DistanceKm > 0);
            Assert.True(savedFlight.FuelRequired > 0);

            _flightRepo.Verify(r => r.AddAsync(It.IsAny<Flight>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ShouldThrowException_WhenAirportIsInvalid()
        {
            // Arrange
            var request = new CreateFlightRequest
            {
                DepartureAirportId = 1,
                DestinationAirportId = 2,
                AircraftId = 3
            };

            _airportRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                        .ReturnsAsync((Airport)null);

            _aircraftRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync(new Aircraft());

            var handler = CreateSUT();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.HandleAsync(request));
        }
    }
}