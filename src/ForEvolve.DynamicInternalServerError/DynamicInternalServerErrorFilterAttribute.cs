using ForEvolve.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForEvolve.DynamicInternalServerError
{
    public class DynamicInternalServerErrorFilterAttribute : ExceptionFilterAttribute
    {
        protected List<IDynamicExceptionResultFilter> Filters { get; }
        protected ILogger Logger;
        protected IErrorFactory ErrorFactory;

        public DynamicInternalServerErrorFilterAttribute(
            IErrorFactory errorFactory, 
            ILogger<DynamicInternalServerErrorFilterAttribute> logger, 
            IEnumerable<IDynamicExceptionResultFilter> filters)
        {
            ErrorFactory = errorFactory ?? throw new ArgumentNullException(nameof(errorFactory));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Filters = new List<IDynamicExceptionResultFilter>(filters ?? Enumerable.Empty<IDynamicExceptionResultFilter>());
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception == null)
            {
                Logger.LogTrace($"Cannot handle null Exception.");
                return;
            }

            Logger.LogTrace($"Converting {context.Exception.GetType().Name} to ErrorResponse.");
            context.Result = CreateActionResult(context);
            context.ExceptionHandled = true;

            base.OnException(context);
        }

        protected virtual IActionResult CreateActionResult(ExceptionContext context)
        {
            var error = ErrorFactory.Create(context.Exception);
            foreach (var filter in Filters)
            {
                filter.Apply(context, error);
            }
            return new DynamicExceptionResult(error);
        }
    }
}
