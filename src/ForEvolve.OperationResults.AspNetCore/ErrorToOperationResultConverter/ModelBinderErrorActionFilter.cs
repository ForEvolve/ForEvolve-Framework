using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.OperationResults.AspNetCore
{
    public class ModelBinderErrorActionFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorMessages = context.ModelState.Values
                    .SelectMany(x =>
                    {
                        return x.Errors;
                    })
                    .Where(x => x.Exception == default)
                    .Select(x => new Message(OperationMessageLevel.Error, new
                    {
                        ErrorCode = "ModelBindingError",
                        x.ErrorMessage
                    }));
                var exceptionMessages = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Where(x => x.Exception != default)
                    .Select(x => new ExceptionMessage(x.Exception));

                var messages = errorMessages.Concat(exceptionMessages);
                var failure = new OperationResult();
                failure.Messages.AddRange(messages);
                context.Result = new BadRequestObjectResult(failure);
                return Task.CompletedTask;
            }
            return next();
        }
    }
}
