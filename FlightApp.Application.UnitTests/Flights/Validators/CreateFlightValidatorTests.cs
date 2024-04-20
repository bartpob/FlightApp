using FlightApp.Application.Flights.CreateFlight;
using FlightApp.Domain.AirplaneTypes;
using FlightApp.Domain.Airports;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace FlightApp.Application.UnitTests.Flights.Validators
{
    public class CreateFlightValidatorTests
    {
        private readonly CreateFlightValidator _validator;
        private readonly Mock<IAirportRepository> _airportRepository;
        private readonly Mock<IAirplaneTypeRepository> _airplaneTypeRepository;
        public CreateFlightValidatorTests()
        {
            _airportRepository = new();
            _airplaneTypeRepository = new();
            _validator = new(_airportRepository.Object, _airplaneTypeRepository.Object);
        }

        [Fact]
        public async Task Should_Have_Erorr_When_FlightNumberIsEmpty()
        {
            var CreateFlightCommand = new CreateFlightCommand("", DateTime.Now, "aaa", "bbbb", "aaaa");
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldHaveValidationErrorFor(f => f.FlightNumber);
        }

        [Fact]
        public async Task Should_NotHave_Erorr_When_FlightNumberIsNotEmpty()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldNotHaveValidationErrorFor(f => f.FlightNumber);
        }

        [Fact]
        public async Task Should_Have_Erorr_When_FlightDateIsEmpty()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", default, "aaa", "bbbb", "aaaa");
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldHaveValidationErrorFor(f => f.FlightDate);
        }

        [Fact]
        public async Task Should_NotHave_Erorr_When_FlightDateIsNotEmpty()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldNotHaveValidationErrorFor(f => f.FlightDate);
        }

        [Fact]
        public async Task Should_Have_Erorr_When_DestinationIsEmpty()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", default, "aaaaa", "", "aaaa");
            _airportRepository.Setup(a => a.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);
            result.ShouldHaveValidationErrorFor(f => f.Destination);
        }

        [Fact]
        public async Task Should_NotHave_Erorr_When_DestinationIsNotEmpty()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            _airportRepository.Setup(a => a.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldNotHaveValidationErrorFor(f => f.Destination);
        }

        [Fact]
        public async Task Should_Have_Erorr_When_DepartureIsEmpty()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", default, "", "aaaa", "aaaa");
            _airportRepository.Setup(a => a.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldHaveValidationErrorFor(f => f.Departure);
        }

        [Fact]
        public async Task Should_NotHave_Erorr_When_DepartureIsNotEmpty()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            _airportRepository.Setup(a => a.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldNotHaveValidationErrorFor(f => f.Departure);
        }

        [Fact]
        public async Task Should_Have_Erorr_When_AirplaneTypeIsEmpty()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", default, "aaa", "bbbb", "");
            _airplaneTypeRepository.Setup(a => a.GetByAirplaneNameAsync(It.IsAny<string>())).ReturnsAsync(new AirplaneType());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldHaveValidationErrorFor(f => f.AirplaneType);
        }

        [Fact]
        public async Task Should_NotHave_Erorr_When_AirplaneTypeIsNotEmpty()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            _airplaneTypeRepository.Setup(a => a.GetByAirplaneNameAsync(It.IsAny<string>())).ReturnsAsync(new AirplaneType());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldNotHaveValidationErrorFor(f => f.AirplaneType);
        }

        [Fact]
        public async Task Should_Have_Error_When_DepartureDestinationAreTheSame()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "aaa", "aaaa");
            _airportRepository.Setup(a => a.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldHaveValidationErrorFor(f => f.Destination);
            result.ShouldHaveValidationErrorFor(f => f.Departure);
        }

        [Fact]
        public async Task Should_NotHave_Error_When_DepartureDestinationAreNotTheSame()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            _airportRepository.Setup(a => a.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldNotHaveValidationErrorFor(f => f.Destination);
            result.ShouldNotHaveValidationErrorFor(f => f.Departure);
        }

        [Fact]
        public async Task Should_Have_Error_When_DepartureNotExists()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            _airportRepository.Setup(a => a.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<Airport>());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldHaveValidationErrorFor(f => f.Departure);
        }

        [Fact]
        public async Task Should_NotHave_Error_When_DepartureExists()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            _airportRepository.Setup(a => a.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldNotHaveValidationErrorFor(f => f.Departure);
        }

        [Fact]
        public async Task Should_Have_Error_When_DestinationNotExists()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            _airportRepository.Setup(a => a.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<Airport>());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldHaveValidationErrorFor(f => f.Destination);
        }

        [Fact]
        public async Task Should_NotHave_Error_When_DestinationExists()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            _airportRepository.Setup(a => a.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldNotHaveValidationErrorFor(f => f.Destination);
        }

        [Fact]
        public async Task Should_Have_Error_When_AirplaneTypeNotExists()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            _airplaneTypeRepository.Setup(a => a.GetByAirplaneNameAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<AirplaneType>());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldHaveValidationErrorFor(f => f.AirplaneType);
        }

        [Fact]
        public async Task Should_NotHave_Error_When_AirplaneTypeExists()
        {
            var CreateFlightCommand = new CreateFlightCommand("aaaa", DateTime.Now, "aaa", "bbbb", "aaaa");
            _airplaneTypeRepository.Setup(a => a.GetByAirplaneNameAsync(It.IsAny<string>())).ReturnsAsync(new AirplaneType());
            var result = await _validator.TestValidateAsync(CreateFlightCommand);

            result.ShouldNotHaveValidationErrorFor(f => f.AirplaneType);
        }
    }
}
