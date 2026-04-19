using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FlightManagementSystem.Application.Flights.Queries.GetAirports;
using FlightManagementSystem.Application.Flights.Interfaces;
using FlightManagementSystem.Application.Flights.DTO.Responses;
using FlightManagementSystem.Domain.Entities;

namespace FlightManagementSystem.Tests.Application.Flights.Queries
{
    public class GetAirportsHandlerTests
    {
        private readonly Mock<IAirportRepository> _repo = new();

        private GetAirportsHandler CreateSUT()
        {
            return new GetAirportsHandler(_repo.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnEmptyList_WhenNoAirportsExist()
        {
            // Arrange
            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(new List<Airport>());

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnMappedAirports()
        {
            // Arrange
            var airports = new List<Airport>
            {
                new Airport { Id = 1, Name = "Lisbon" },
                new Airport { Id = 2, Name = "Porto" }
            };

            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(airports);

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            Assert.Equal(2, result.Count);

            Assert.Contains(result, a => a.Id == 1 && a.Name == "Lisbon");
            Assert.Contains(result, a => a.Id == 2 && a.Name == "Porto");
        }

        [Fact]
        public async Task HandleAsync_ShouldMapFieldsCorrectly()
        {
            // Arrange
            var airports = new List<Airport>
            {
                new Airport { Id = 10, Name = "Test Airport" }
            };

            _repo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(airports);

            var handler = CreateSUT();

            // Act
            var result = await handler.HandleAsync();

            // Assert
            var item = result.First();

            Assert.Equal(10, item.Id);
            Assert.Equal("Test Airport", item.Name);
        }
    }
}