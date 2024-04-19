using FlightApp.Application.Abstractions;
using FlightApp.Infrastructure.Authentication;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.Identity.LoginUser
{
    internal sealed class LoginUserCommandHandler(UserManager<IdentityUser> _userManager, IAuthenticationService _authService, 
         IValidator<LoginUserCommand> _validator)
         : IRequestHandler<LoginUserCommand, Result<LoginUserResponse>>
    {
        public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
           var validationResult =  await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Result<LoginUserResponse>.Failed(Error.ValidationErrorsToResultErrors(validationResult.Errors));
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            var token = await _authService.GenerateTokenAsync(user!);

            var response = new LoginUserResponse(request.Email, token);

            return Result<LoginUserResponse>.Succeeded(response);
        }
    }
}
