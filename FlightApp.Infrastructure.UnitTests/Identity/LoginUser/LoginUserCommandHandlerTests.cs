using FlightApp.Application.Abstractions;
using FlightApp.Infrastructure.Authentication;
using FlightApp.Infrastructure.Identity.LoginUser;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.UnitTests.Identity.LoginUser
{
    public class FakeUserManager 
        : UserManager<IdentityUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<IdentityUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<IdentityUser>>().Object,
                new IUserValidator<IdentityUser>[0],
                new IPasswordValidator<IdentityUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<IdentityUser>>>().Object)
        {
        }
    }

    public class FakeUserManagerBuilder
    {
        private Mock<FakeUserManager> _mock = new Mock<FakeUserManager>();

        public FakeUserManagerBuilder With(Action<Mock<FakeUserManager>> mock)
        {
            mock(_mock);
            return this;
        }

        public Mock<FakeUserManager> Build()
        {
            return _mock;
        }
    }
    public class LoginUserCommandHandlerTests
    {
        private readonly Mock<IAuthenticationService> _authenticationServiceMock;
        private readonly Mock<IValidator<LoginUserCommand>> _validatorMock;
        public LoginUserCommandHandlerTests()
        {
            _authenticationServiceMock = new();
            _validatorMock = new();
        }

        [Fact]
        public async Task Handle_OnInvalidCommand_ReturnsFailedResult()
        {
            //Arrange
            var command = new LoginUserCommand("flight@app.com", "P@$$W0RD");
            var userManager = new FakeUserManagerBuilder().Build();
            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("Property1", "Error message 1."),
                new ValidationFailure("Property2", "Error message 2.")
            };

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(validationErrors));

            var handler = new LoginUserCommandHandler(
                userManager.Object,
                _authenticationServiceMock.Object, 
                _validatorMock.Object);

            //Act

            var result = await handler.Handle(command, default);

            //Assert
            result.IsFailed.Should().BeTrue();
            result.IsSucceeded.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_OnValidCommand_ReturnsSucceededResult()
        {
            //Arrange
            var command = new LoginUserCommand("flight@app.com", "P@$$W0RD");
            var userManager = new FakeUserManagerBuilder().Build();

            _validatorMock.Setup(
                x => x.ValidateAsync(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var handler = new LoginUserCommandHandler(
                userManager.Object,
                _authenticationServiceMock.Object,
                _validatorMock.Object);

            //Act

            var result = await handler.Handle(command, default);

            //Assert
            result.IsFailed.Should().BeFalse();
            result.IsSucceeded.Should().BeTrue();
        }
    }
}
