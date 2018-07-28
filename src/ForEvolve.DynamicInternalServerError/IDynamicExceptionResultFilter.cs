using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.DynamicInternalServerError
{
    public interface IDynamicExceptionResultFilter
    {
        void Apply(ExceptionContext context, Error error);
    }
}
