using FlightApp.Domain.AirplaneTypes;
using FlightApp.Domain.Airports;
using FlightApp.Domain.Flights;
using FlightApp.Infrastructure.Persistence;
using FlightApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.UnitTests.Persistence.Repositories
{
    public class FlightRepositoryTests
    {
        private readonly FlightRepository _flightRepository;
        private readonly FlightAppDbContext _dbContext;

        public FlightRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<FlightAppDbContext>()
                .UseInMemoryDatabase(databaseName: "FlightAppTest")
                .Options;

            _dbContext = new FlightAppDbContext(options);
            _flightRepository = new FlightRepository(_dbContext);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddFlightToDbContext()
        {
            // Arrange
            var flight = Flight.Create("EA775", DateTime.Now, 
                Airport.Create("aaaa", "bbbb", "ccc"), Airport.Create("bbbbb", "aaaa", "dddd"),
                AirplaneType.Create("Boeing"));

            // Act
            await _flightRepository.CreateAsync(flight);

            // Assert
            var addedFlight = await _dbContext.Flights.FindAsync(flight.Id);
            Assert.NotNull(addedFlight);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveFlightFromDbContext()
        {
            // Arrange
            var flight = Flight.Create("EA775", DateTime.Now,
                 Airport.Create("aaaa", "bbbb", "ccc"), Airport.Create("bbbbb", "aaaa", "dddd"),
                 AirplaneType.Create("Boeing"));
            await _dbContext.Flights.AddAsync(flight);
            await _dbContext.SaveChangesAsync();

            // Act
            await _flightRepository.DeleteAsync(flight.Id);
            await _dbContext.SaveChangesAsync();
            // Assert
            var deletedFlight = await _dbContext.Flights.FindAsync(flight.Id);
            Assert.Null(deletedFlight);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfFlights()
        {
            // Arrange
            _dbContext.Flights.RemoveRange(_dbContext.Flights.ToList());
            await _dbContext.SaveChangesAsync();
            var flightsData = new List<Flight> {
                Flight.Create("EA775", DateTime.Now,
                 Airport.Create("aaaa", "bbbb", "ccc"), Airport.Create("bbbbb", "aaaa", "dddd"),
                 AirplaneType.Create("Boeing"))
        };
            await _dbContext.Flights.AddRangeAsync(flightsData);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _flightRepository.GetAllAsync();

            // Assert
            Assert.Equal(flightsData.Count, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectFlight()
        {
            // Arrange
            var flight = Flight.Create("EA775", DateTime.Now,
                Airport.Create("aaaa", "bbbb", "ccc"), Airport.Create("bbbbb", "aaaa", "dddd"),
                AirplaneType.Create("Boeing"));
            await _dbContext.Flights.AddAsync(flight);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _flightRepository.GetByIdAsync(flight.Id);

            // Assert
            Assert.Equal(flight, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCorrectFlight()
        {
            _dbContext.Flights.RemoveRange(_dbContext.Flights.ToList());
            await _dbContext.SaveChangesAsync();
            var flight = Flight.Create("EA775", DateTime.Now,
                Airport.Create("aaaa", "bbbb", "ccc"), Airport.Create("bbbbb", "aaaa", "dddd"),
                AirplaneType.Create("Boeing"));
            await _dbContext.Flights.AddAsync(flight);
            await _dbContext.SaveChangesAsync();

            Flight getFlight = await _dbContext.Flights.FindAsync(flight.Id);
            Assert.NotNull(getFlight);
            getFlight.Update("EA888", DateTime.Now, getFlight.Destination, getFlight.Departure, getFlight.AirplaneType);
            await _flightRepository.UpdateAsync(getFlight);

            var updatedFlight = await _flightRepository.GetByIdAsync(flight.Id);

            Assert.Equal(getFlight, updatedFlight);
        }
    }
}
