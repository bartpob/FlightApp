using FlightApp.Infrastructure.Persistence.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.Identity.LoginUser
{
    internal class LoginUserValidator
        : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator(UserManager<IdentityUser> _userManager)
        {
            IdentityUser? user = null;
            RuleFor(l => l.Password).NotNull().NotEmpty();


            RuleFor(l => l.Email).NotNull().NotEmpty().EmailAddress().MustAsync(async (email, Cancellation) =>
            {
                user = await _userManager.FindByEmailAsync(email);
                return user != null;
            }).
                WithErrorCode("NOT_EXISTS").
                WithMessage("Given email address doesn't exist"); ;

            RuleFor(l => l.Password).MustAsync(async (password, Cancellation) =>
            {
                var valid = await _userManager.CheckPasswordAsync(user!, password);
                return valid;
            }).
            WithErrorCode("INVALID_PASSWORD").
            WithMessage("Given password is not valid");
        }
    }
}
