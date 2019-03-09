using ForEvolve.OperationResults;
using ForEvolve.OperationResults.Standardizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.OperationResults
{
    /// <summary>
    /// Represents an action filter that convert <see cref="IOperationResult"/> object to a 
    /// more standard result.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Filters.IAsyncActionFilter" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IAsyncActionFilter" />
    public class OperationResultStandardizerActionFilter<TObjectResult> : IAsyncActionFilter
        where TObjectResult : ObjectResult
    {
        private readonly IOperationResultStandardizer _operationResultStandardizer;

        public OperationResultStandardizerActionFilter(IOperationResultStandardizer operationResultStandardizer)
        {
            _operationResultStandardizer = operationResultStandardizer ?? throw new ArgumentNullException(nameof(operationResultStandardizer));
        }

        /// <inheritdoc />
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionExecutedContext = await next.Invoke();
            OnActionExecuted(actionExecutedContext);
        }

        private void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Result is TObjectResult objectResult)
            {
                if (objectResult.Value is IOperationResult operationResult)
                {
                    objectResult.Value = _operationResultStandardizer.Standardize(operationResult);
                }
            }
        }
    }
}
