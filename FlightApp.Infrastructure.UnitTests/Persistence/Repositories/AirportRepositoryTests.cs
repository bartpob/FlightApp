using FlightApp.Domain.AirplaneTypes;
using FlightApp.Infrastructure.Persistence.Repositories;
using FlightApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightApp.Domain.Airports;

namespace FlightApp.Infrastructure.UnitTests.Persistence.Repositories
{
    public class AirportRepositoryTests
    {
        private readonly AirportRepository _airportRepository;
        private readonly FlightAppDbContext _dbContext;

        public AirportRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<FlightAppDbContext>()
                .UseInMemoryDatabase(databaseName: "FlightAppTest")
                .Options;

            _dbContext = new FlightAppDbContext(options);
            _airportRepository = new AirportRepository(_dbContext);

            var airport = Airport.Create("WAW", "Warsaw", "Poland");

            _dbContext.Airports.Add(airport);

            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetByIataAsync_ShouldReturn_NotNullValue()
        {
            //Arrange
            var iata = "WAW";

            //Act
            var result = await _airportRepository.GetByIataAsync(iata);

            //Assert

            Assert.NotNull(result);
            Assert.Equal(iata, result.IATA);
        }

        [Fact]
        public async Task GetByIataAsync_ShouldReturn_NullValue()
        {
            //Arrange
            var iata = "WRO";

            //Act
            var result = await _airportRepository.GetByIataAsync(iata);

            //Assert

            Assert.Null(result);
        }
    }
}
