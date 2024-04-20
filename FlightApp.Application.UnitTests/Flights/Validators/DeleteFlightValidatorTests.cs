using FlightApp.Application.Flights.CreateFlight;
using FlightApp.Application.Flights.DeleteFlight;
using FlightApp.Domain.AirplaneTypes;
using FlightApp.Domain.Airports;
using FlightApp.Domain.Flights;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
namespace FlightApp.Application.UnitTests.Flights.Validators
{
    public class DeleteFlightValidatorTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly DeleteFlightValidator _validator;
        public DeleteFlightValidatorTests()
        {
            _flightRepositoryMock = new();
            _validator = new(_flightRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_NotHave_Erorr_WhenIdIsNotEmpty()
        {
            var command = new DeleteFlightCommand(Guid.NewGuid());
            _flightRepositoryMock.Setup(f => f.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Flight());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public async Task Should_Have_Erorr_WhenIdIsEmpty()
        {
            var command = new DeleteFlightCommand(Guid.Empty);
            _flightRepositoryMock.Setup(f => f.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Flight());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public async Task Should_Have_Error_WhenIdNotExists()
        {
            var command = new DeleteFlightCommand(Guid.NewGuid());
            _flightRepositoryMock.Setup(f => f.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(It.IsAny<Flight>());

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(c => c.Id);
        }
    }
}
