using System;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.Api.Domain
{
    public interface IErrorFromExceptionFactory
    {
        Error Create<TException>(TException exception)
            where TException : Exception;
    }
}
