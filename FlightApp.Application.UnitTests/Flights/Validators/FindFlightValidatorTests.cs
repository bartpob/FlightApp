using FlightApp.Application.Flights.DeleteFlight;
using FlightApp.Application.Flights.FindFlight;
using FlightApp.Domain.Airports;
using FlightApp.Domain.Flights;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.UnitTests.Flights.Validators
{
    public class FindFlightValidatorTests
    {
        private readonly Mock<IAirportRepository> _airportRepositoryMock;
        private readonly FindFlightValidator _validator;

        public FindFlightValidatorTests()
        {
            _airportRepositoryMock = new();
            _validator = new(_airportRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_Have_Error_WhenDepartureNotExists()
        {
            var command = new FindFlightQuery("departure", "destination", DateTime.Now);
            _airportRepositoryMock.Setup(f => f.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<Airport>());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(c => c.Departure);
        }

        [Fact]
        public async Task Should_NotHave_Error_WhenDepartureExists()
        {
            var command = new FindFlightQuery("departure", "destination", DateTime.Now);
            _airportRepositoryMock.Setup(f => f.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveValidationErrorFor(c => c.Departure);
        }

        [Fact]
        public async Task Should_Have_Error_WhenDestinationNotExists()
        {
            var command = new FindFlightQuery("departure", "destination", DateTime.Now);
            _airportRepositoryMock.Setup(f => f.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<Airport>());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(c => c.Destination);
        }

        [Fact]
        public async Task Should_NotHave_Error_WhenDestinationExists()
        {
            var command = new FindFlightQuery("departure", "destination", DateTime.Now);
            _airportRepositoryMock.Setup(f => f.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveValidationErrorFor(c => c.Destination);
        }
    }
}
