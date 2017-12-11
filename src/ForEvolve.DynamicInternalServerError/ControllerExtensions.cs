using ForEvolve.Api.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.DynamicInternalServerError
{
    public static class ControllerExtensions
    {
        public static DynamicExceptionResult InternalServerError(this Controller controller, Error error)
        {
            return new DynamicExceptionResult(error);
        }

        public static DynamicExceptionResult InternalServerError(this Controller controller, string code, string message, IEnumerable<Error> details)
        {
            return new DynamicExceptionResult(new Error
            {
                Code = code,
                Message = message,
                Details = new List<Error>(details)
            });
        }
    }
}
