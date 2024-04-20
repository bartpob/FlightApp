using FlightApp.Application.Abstractions;
using FlightApp.Application.Flights.CreateFlight;
using FlightApp.Application.Flights.DeleteFlight;
using FlightApp.Application.Flights.FindFlight;
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

namespace FlightApp.Application.UnitTests.Flights.Queires
{
    public class FindFlightQueryHandlerTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly Mock<IAirportRepository> _airportRepositoryMock;
        private readonly Mock<IValidator<FindFlightQuery>> _validatorMock;

        public FindFlightQueryHandlerTests()
        {
            _flightRepositoryMock = new();
            _airportRepositoryMock = new();
            _validatorMock = new();
        }
        [Fact]
        public async Task Handle_OnInvalidCommand_ReturnsFailedResult()
        {
            //Arrange
            var command = new FindFlightQuery("departure", "destination", DateTime.Now);

            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("Property1", "Error message 1.")
            };

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<FindFlightQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(validationErrors));

            var handler = new FindFlightQueryHandler(_flightRepositoryMock.Object, _validatorMock.Object, _airportRepositoryMock.Object);

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
            var command = new FindFlightQuery("departure", "destination", DateTime.Now);

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<FindFlightQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var handler = new FindFlightQueryHandler(_flightRepositoryMock.Object, _validatorMock.Object, _airportRepositoryMock.Object);
            _flightRepositoryMock.Setup(f => f.GetAllAsync()).ReturnsAsync(new List<Flight>());
            _airportRepositoryMock.Setup(a => a.GetByIataAsync(It.IsAny<string>())).ReturnsAsync(new Airport());
            //Act
            Result result = await handler.Handle(command, default);
            //Assert
            result.IsFailed.Should().BeFalse();
            result.IsSucceeded.Should().BeTrue();
        }
    }
}
