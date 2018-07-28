using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ForEvolve.DynamicInternalServerError
{
    public class DynamicExceptionResult : ObjectResult, IDynamicActionResult
    {
        public DynamicExceptionResult(Error error)
            : base(new ErrorResponse(error))
        {
            StatusCode = StatusCodes.Status500InternalServerError;
            Error = error;
        }

        public Error Error { get; }
    }
}
