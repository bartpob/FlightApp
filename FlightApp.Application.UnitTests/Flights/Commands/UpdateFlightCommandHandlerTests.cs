using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.CreateFlight;
using FlightApp.Application.Flights.UpdateFlight;
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
    public class UpdateFlightCommandHandlerTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly Mock<IAirplaneTypeRepository> _airplaneTypeRepositoryMock;
        private readonly Mock<IAirportRepository> _airportRepositoryMock;
        private readonly Mock<IValidator<UpdateFlightCommand>> _validatorMock;

        public UpdateFlightCommandHandlerTests()
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
            var command = new UpdateFlightCommand(Guid.NewGuid(), "ee", DateTime.Now, "dep", "dest", "airbus");

            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("Property1", "Error message 1."),
                new ValidationFailure("Property2", "Error message 2.")
            };

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<UpdateFlightCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(validationErrors));

            var handler = new UpdateFlightCommandHandler(
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
            var command = new UpdateFlightCommand(Guid.NewGuid(), "EEEE", DateTime.Now, "dep", "dest", "airbus");

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<UpdateFlightCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var handler = new UpdateFlightCommandHandler(
                _flightRepositoryMock.Object,
                _validatorMock.Object,
                _airplaneTypeRepositoryMock.Object,
                _airportRepositoryMock.Object);
            _flightRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Flight());
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
            var command = new UpdateFlightCommand(Guid.NewGuid(), "EEEE", DateTime.Now, "dep", "dest", "airbus");

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<UpdateFlightCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var handler = new UpdateFlightCommandHandler(
                _flightRepositoryMock.Object,
                _validatorMock.Object,
                _airplaneTypeRepositoryMock.Object,
                _airportRepositoryMock.Object);

            _flightRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Flight());

            //Act
            Result result = await handler.Handle(command, default);
            //Assert
            _flightRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Flight>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidCommand_UpdatesFlight()
        {
            var flightNumber = "FA123";
            var flightDate = new DateTime(2024, 04, 20);
            var departureIata = "JFK";
            var destinationIata = "LAX";
            var airplaneTypeName = "Boeing";
            var flight = Flight.Create("flightNumber", DateTime.Now, new Airport(), new Airport(), AirplaneType.Create("airbus"));
            Guid flightId = flight.Id;
            var command = new UpdateFlightCommand(flightId, flightNumber, flightDate, departureIata, destinationIata, airplaneTypeName);

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<UpdateFlightCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _flightRepositoryMock.Setup(repo => repo.GetByIdAsync(flightId)).ReturnsAsync(flight);
            _airportRepositoryMock.Setup(repo => repo.GetByIataAsync(departureIata.ToUpper())).ReturnsAsync(Airport.Create(departureIata, "city", "country"));
            _airportRepositoryMock.Setup(repo => repo.GetByIataAsync(destinationIata.ToUpper())).ReturnsAsync(Airport.Create(destinationIata, "city", "country"));
            _airplaneTypeRepositoryMock.Setup(repo => repo.GetByAirplaneNameAsync(airplaneTypeName.ToUpper())).ReturnsAsync(AirplaneType.Create(airplaneTypeName));

            var handler = new UpdateFlightCommandHandler(
                _flightRepositoryMock.Object,
                _validatorMock.Object,
                _airplaneTypeRepositoryMock.Object,
                _airportRepositoryMock.Object
            );

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsSucceeded);
            Assert.Equal(flightNumber, flight.FlightNumber);
            Assert.Equal(flightDate, flight.FlightDate);
            Assert.Equal(departureIata, flight.Departure.IATA);
            Assert.Equal(destinationIata, flight.Destination.IATA);
            Assert.Equal(airplaneTypeName, flight.AirplaneType.Airplane);
        }
    }
}
