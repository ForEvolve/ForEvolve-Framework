using System;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.AspNetCore
{
    public interface IErrorFromExceptionFactory
    {
        Error Create<TException>(TException exception)
            where TException : Exception;
    }
}
