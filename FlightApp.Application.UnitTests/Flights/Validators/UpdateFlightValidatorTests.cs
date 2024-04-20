using FlightApp.Application.Flights.FindFlight;
using FlightApp.Application.Flights.UpdateFlight;
using FlightApp.Domain.AirplaneTypes;
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
    public class UpdateFlightValidatorTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly Mock<IAirportRepository> _airportRepositoryMock;
        private readonly Mock<IAirplaneTypeRepository> _airplaneTypeRepositoryMock;
        private readonly UpdateFlightValidator _validator;

        public UpdateFlightValidatorTests()
        {
            _flightRepositoryMock = new();
            _airportRepositoryMock = new();
            _airplaneTypeRepositoryMock = new();
            _validator = new(_flightRepositoryMock.Object,
                _airportRepositoryMock.Object,
                _airplaneTypeRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_Have_Error_WhenFlightNotExists()
        {
            var command = new UpdateFlightCommand(Guid.NewGuid(), "flightNumber", DateTime.Now, "departure", "destination", "AirplaneType");

            _flightRepositoryMock.Setup(f => f.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(It.IsAny<Flight>());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public async Task Should_NotHave_Error_WhenFlightExists()
        {
            var command = new UpdateFlightCommand(Guid.NewGuid(), "flightNumber", DateTime.Now, "departure", "destination", "AirplaneType");
            _flightRepositoryMock.Setup(f => f.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Flight());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public async Task Should_Have_Error_WhenDepartureNotExists()
        {
            var command = new UpdateFlightCommand(Guid.NewGuid(), "flightNumber", DateTime.Now, "departure", "destination", "AirplaneType");

            _airportRepositoryMock.Setup(f => f.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<Airport>());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(c => c.Departure);
        }

        [Fact]
        public async Task Should_NotHave_Error_WhenDepartureExists()
        {
            var command = new UpdateFlightCommand(Guid.NewGuid(), "flightNumber", DateTime.Now, "departure", "destination", "AirplaneType");
            _airportRepositoryMock.Setup(f => f.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveValidationErrorFor(c => c.Departure);
        }


        [Fact]
        public async Task Should_Have_Error_WhenDestinationNotExists()
        {
            var command = new UpdateFlightCommand(Guid.NewGuid(), "flightNumber", DateTime.Now, "departure", "destination", "AirplaneType");

            _airportRepositoryMock.Setup(f => f.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<Airport>());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(c => c.Destination);
        }

        [Fact]
        public async Task Should_NotHave_Error_WhenDestinationExists()
        {
            var command = new UpdateFlightCommand(Guid.NewGuid(), "flightNumber", DateTime.Now, "departure", "destination", "AirplaneType");
            _airportRepositoryMock.Setup(f => f.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveValidationErrorFor(c => c.Destination);
        }

        [Fact]
        public async Task Should_Have_Error_WhenAirplaneTypeNotExists()
        {
            var command = new UpdateFlightCommand(Guid.NewGuid(), "flightNumber", DateTime.Now, "departure", "destination", "AirplaneType");

            _airplaneTypeRepositoryMock.Setup(f => f.GetByAirplaneNameAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<AirplaneType>());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(c => c.AirplaneType);
        }

        [Fact]
        public async Task Should_NotHave_Error_WhenAirplaneTypeExists()
        {
            var command = new UpdateFlightCommand(Guid.NewGuid(), "flightNumber", DateTime.Now, "departure", "destination", "AirplaneType");
            _airplaneTypeRepositoryMock.Setup(f => f.GetByAirplaneNameAsync(It.IsAny<string>())).ReturnsAsync(new AirplaneType());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveValidationErrorFor(c => c.AirplaneType);
        }
    }
}
