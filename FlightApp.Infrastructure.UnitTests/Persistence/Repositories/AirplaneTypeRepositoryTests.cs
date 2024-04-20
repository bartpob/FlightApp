using FlightApp.Infrastructure.Persistence.Repositories;
using FlightApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightApp.Domain.Flights;
using Microsoft.EntityFrameworkCore;
using FlightApp.Domain.AirplaneTypes;

namespace FlightApp.Infrastructure.UnitTests.Persistence.Repositories
{
    public class AirplaneTypeRepositoryTests
    {
        private readonly AirplaneTypeRepository _airplaneTypeRepository;
        private readonly FlightAppDbContext _dbContext;

        public AirplaneTypeRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<FlightAppDbContext>()
                .UseInMemoryDatabase(databaseName: "FlightAppTest")
                .Options;

            _dbContext = new FlightAppDbContext(options);
            _airplaneTypeRepository = new AirplaneTypeRepository(_dbContext);

            var airplane = AirplaneType.Create("BOEING");

            _dbContext.AirplaneTypes.Add(airplane);

            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetByAirplaneNameAsync_ShouldReturn_NotNullValue()
        {
            //Arrange
            var name = "BOEING";

            //Act
            var result = await _airplaneTypeRepository.GetByAirplaneNameAsync(name);

            //Assert

            Assert.NotNull(result);
            Assert.Equal(name, result.Airplane);
        }

        [Fact]
        public async Task GetByAirplaneNameAsync_ShouldReturn_NullValue()
        {
            //Arrange
            var name = "AIRBUS";

            //Act
            var result = await _airplaneTypeRepository.GetByAirplaneNameAsync(name);

            //Assert

            Assert.Null(result);
        }
    }
}
