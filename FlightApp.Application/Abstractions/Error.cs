using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Application.Abstractions
{
    public sealed record Error(string Code, string? Description = null)
    {
        public static List<Error> ValidationErrorsToResultErrors(List<ValidationFailure> erorrs)
        {
            List<Error> domainErrors = new();

            foreach(var error in erorrs)
            {
                domainErrors.Add(new Error(error.ErrorCode, error.ErrorMessage));
            }

            return domainErrors;
        }
    }
}
