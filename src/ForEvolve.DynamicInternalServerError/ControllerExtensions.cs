using ForEvolve.Contracts.Errors;
using ForEvolve.AspNetCore;
using ForEvolve.AspNetCore.ErrorFactory.Implementations;
using ForEvolve.DynamicInternalServerError;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerExtensions
    {
        public static IDynamicActionResult InternalServerError(this ControllerBase controller, Error error)
        {
            return new DynamicExceptionResult(error);
        }

        public static IDynamicActionResult InternalServerError(this ControllerBase controller, IOperationResult operationResult)
        {
            var error = DefaultErrorFactory.Current.Create(operationResult);
            return new DynamicExceptionResult(error);
        }

        //
        //public static IDynamicActionResult InternalServerError(this Controller controller, string code, string message, IEnumerable<Error> details)
        //{
        //    return new DynamicExceptionResult(new Error
        //    {
        //        Code = code,
        //        Message = message,
        //        Details = new List<Error>(details)
        //    });
        //}
    }
}
