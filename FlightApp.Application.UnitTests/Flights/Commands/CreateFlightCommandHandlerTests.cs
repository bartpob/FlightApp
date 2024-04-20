using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.CreateFlight;
using FlightApp.Domain.AirplaneTypes;
using FlightApp.Domain.Airports;
using FlightApp.Domain.Flights;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.UnitTests.Flights.Commands
{
    public class CreateFlightCommandHandlerTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly Mock<IAirplaneTypeRepository> _airplaneTypeRepositoryMock;
        private readonly Mock<IAirportRepository> _airportRepositoryMock;
        private readonly Mock<IValidator<CreateFlightCommand>> _validatorMock;

        public CreateFlightCommandHandlerTests()
        {
            _flightRepositoryMock = new();
            _airplaneTypeRepositoryMock = new();
            _airportRepositoryMock = new();
            _validatorMock = new();
        }

        [Fact]
        public async Task Handle_OnInvalidCommand_ReturnsFailedResult()
        {
            //Arrange
            var command = new CreateFlightCommand("EEEE", DateTime.Now, "dep", "dest", "airbus");

            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("Property1", "Error message 1."),
                new ValidationFailure("Property2", "Error message 2.")
            };

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<CreateFlightCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(validationErrors));

            var handler = new CreateFlightCommandHandler(
                _flightRepositoryMock.Object, 
                _validatorMock.Object, 
                _airplaneTypeRepositoryMock.Object,
                _airportRepositoryMock.Object);

            //Act
            Result result = await handler.Handle(command, default);
            //Assert
            result.IsFailed.Should().BeTrue();
            result.IsSucceeded.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_OnValidCommand_ReturnsSucceededResult()
        {
            //Arrange
            var command = new CreateFlightCommand("EEEE", DateTime.Now, "dep", "dest", "airbus");

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<CreateFlightCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var handler = new CreateFlightCommandHandler(
                _flightRepositoryMock.Object,
                _validatorMock.Object,
                _airplaneTypeRepositoryMock.Object,
                _airportRepositoryMock.Object);

            //Act
            Result result = await handler.Handle(command, default);
            //Assert
            result.IsFailed.Should().BeFalse();
            result.IsSucceeded.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_OnValidCommand_CallsFlightRepositoryCreateAsync()
        {
            //Arrange
            var command = new CreateFlightCommand("EEEE", DateTime.Now, "dep", "dest", "airbus");

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<CreateFlightCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var handler = new CreateFlightCommandHandler(
                _flightRepositoryMock.Object,
                _validatorMock.Object,
                _airplaneTypeRepositoryMock.Object,
                _airportRepositoryMock.Object);

            //Act
            Result result = await handler.Handle(command, default);
            //Assert
            _flightRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Flight>()), Times.Once);
        }
    }
}
 