using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore.ExceptionFilters
{
    /// <summary>
    /// Represent a filter that runs asynchronously after an action has thrown an System.Exception.
    /// If the exception is an <see cref="NotImplementedException"/>, the filter convert the status code from 500 to 501.
    /// Implements the <see cref="ExceptionFilterAttribute" />
    /// </summary>
    /// <seealso cref="ExceptionFilterAttribute" />
    public class NotImplementedExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Called when an exception is thrown in the MVC pipeline.
        /// </summary>
        /// <param name="context">The exception context.</param>
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.HttpContext.Response.StatusCode = 501;
                context.ExceptionHandled = true;
            }
        }
    }
}
