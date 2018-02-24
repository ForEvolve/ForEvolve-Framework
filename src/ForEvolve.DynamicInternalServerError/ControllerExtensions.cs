using ForEvolve.Api.Contracts.Errors;
using ForEvolve.AspNetCore;
using ForEvolve.AspNetCore.ErrorFactory.Implementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForEvolve.DynamicInternalServerError
{
    public static class ControllerExtensions
    {
        public static IDynamicActionResult InternalServerError(this Controller controller, Error error)
        {
            return new DynamicExceptionResult(error);
        }

        public static IDynamicActionResult InternalServerError(this Controller controller, IOperationResult operationResult)
        {
            var error = DefaultErrorFactory.Current.Create(operationResult);
            return new DynamicExceptionResult(error);
        }

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
