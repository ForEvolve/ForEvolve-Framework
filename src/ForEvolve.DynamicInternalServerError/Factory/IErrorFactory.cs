using ForEvolve.Api.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ForEvolve.DynamicInternalServerError
{
    public interface IErrorFactory
    {
        Error CreateErrorFor<TException>(TException ex)
            where TException : Exception;
        Error CreateErrorFor(SerializableError serializableError);
    }
}