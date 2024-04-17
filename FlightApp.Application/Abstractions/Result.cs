using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlightApp.Application.Abstractions
{
    public class Result
    {
        public bool IsSucceeded { get; private set; }
        public bool IsFailed => !IsSucceeded;
        public List<Error> Errors { get; private set; }
        protected Result(bool isSucceeded, List<Error> errors)
        {
            if (isSucceeded && errors.Any() ||
                !isSucceeded && !errors.Any())
            {
                throw new ArgumentException("Invalid error", nameof(Result));
            }

            IsSucceeded = isSucceeded;
            Errors = errors;
        }

        public static Result Failed(List<Error> errors)
        {
            return new Result(false, errors);
        }

        public static Result Succeeded()
        {
            return new Result(true, new());
        }
    }

    public sealed class Result<TBody>
        : Result
        where TBody : class
    {

        public TBody? Body { get; private set; }
        private Result(bool isSucceeded, List<Error> errors, TBody? body)
            : base(isSucceeded, errors)
        {
            Body = body;
        }

        public new static Result<TBody> Failed(List<Error> errors)
        {
            return new Result<TBody>(false, errors, null);
        }

        public static Result<TBody> Succeeded(TBody body)
        {
            return new(true, new(), body);
        }
    }
}
