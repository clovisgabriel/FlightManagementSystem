using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FlightManagementSystem.Application.Flights.Queries.GetAircraft;
using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Application.Flights.DTO.Responses;
using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Tests.Application.Flights.Queries
{
    public class GetAircraftHandlerTests
    {
        private readonly Mock<IAircraftRepository> _repo = new();

        private GetAircraftHandler CreateSUT()
        {
            return new GetAircraftHandler(_repo.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnEmptyList_WhenNoAircraftExist()
        {
            // Arrange
            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(new List<Aircraft>());

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnMappedAircraft()
        {
            // Arrange
            var aircraftList = new List<Aircraft>
            {
                new Aircraft { Id = 1, Model = "Boeing 737" },
                new Aircraft { Id = 2, Model = "Airbus A320" }
            };

            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(aircraftList);

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            Assert.Equal(2, result.Count);

            Assert.Contains(result, a => a.Id == 1 && a.Model == "Boeing 737");
            Assert.Contains(result, a => a.Id == 2 && a.Model == "Airbus A320");
        }

        [Fact]
        public async Task HandleAsync_ShouldMapFieldsCorrectly()
        {
            // Arrange
            var aircraftList = new List<Aircraft>
            {
                new Aircraft { Id = 99, Model = "Test Model X" }
            };

            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(aircraftList);

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            var item = result.First();

            Assert.Equal(99, item.Id);
            Assert.Equal("Test Model X", item.Model);
        }
    }
}