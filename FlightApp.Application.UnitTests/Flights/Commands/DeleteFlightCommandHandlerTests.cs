using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.CreateFlight;
using FlightApp.Domain.AirplaneTypes;
using FlightApp.Domain.Airports;
using FlightApp.Domain.Flights;
using FluentValidation.Results;
using FluentValidation;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightApp.Application.Flights.DeleteFlight;

namespace FlightApp.Application.UnitTests.Flights.Commands
{
    public class DeleteFlightCommandHandlerTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly Mock<IValidator<DeleteFlightCommand>> _validatorMock;

        public DeleteFlightCommandHandlerTests()
        {
            _flightRepositoryMock = new();
            _validatorMock = new();
        }

        [Fact]
        public async Task Handle_OnInvalidCommand_ReturnsFailedResult()
        {
            //Arrange
            var command = new DeleteFlightCommand(Guid.NewGuid());

            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("Property1", "Error message 1.")
            };

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<DeleteFlightCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(validationErrors));

            var handler = new DeleteFlightCommandHandler(_flightRepositoryMock.Object, _validatorMock.Object);

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
            var command = new DeleteFlightCommand(Guid.NewGuid());

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<DeleteFlightCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var handler = new DeleteFlightCommandHandler(_flightRepositoryMock.Object, _validatorMock.Object);

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
            var command = new DeleteFlightCommand(Guid.NewGuid());

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<DeleteFlightCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var handler = new DeleteFlightCommandHandler(_flightRepositoryMock.Object, _validatorMock.Object);

            //Act
            Result result = await handler.Handle(command, default);

            //Assert
            _flightRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
