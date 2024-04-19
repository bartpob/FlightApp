using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.Authentication
{
    internal interface IAuthenticationService
    {
        public Task<string> GenerateTokenAsync(IdentityUser user);
    }
}
