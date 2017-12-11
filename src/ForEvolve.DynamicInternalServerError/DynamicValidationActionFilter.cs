using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Text;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.DynamicInternalServerError
{
    public class DynamicValidationActionFilter : IActionFilter
    {
        protected ILogger Logger;
        protected IErrorFactory ErrorFactory;

        public DynamicValidationActionFilter(
            IErrorFactory errorFactory,
            ILogger<DynamicValidationActionFilter> logger)
        {
            ErrorFactory = errorFactory ?? throw new ArgumentNullException(nameof(errorFactory));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Logger.LogTrace($"Action executed.");
            if (context.Result is BadRequestObjectResult objContext)
            {
                Logger.LogTrace($"context.Result is a BadRequestObjectResult.");
                if (objContext.Value is SerializableError serializableError)
                {
                    Logger.LogInformation($"Converting SerializableError to ErrorResponse.");
                    var error = ErrorFactory.CreateErrorFor(serializableError);
                    context.Result = new BadRequestObjectResult(new ErrorResponse(error));
                }
            }       
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Logger.LogTrace($"Action executing.");
        }
    }
}