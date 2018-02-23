using ForEvolve.Api.Contracts.Errors;
using ForEvolve.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForEvolve.DynamicInternalServerError
{
    public static class ControllerExtensions
    {
        public static IDynamicResult InternalServerError(this Controller controller, Error error)
        {
            return new DynamicExceptionResult(error);
        }

        public static IDynamicResult InternalServerError(this Controller controller, IOperationResult operationResult)
        {
            if (operationResult.Errors.Count() == 1)
            {
                return new DynamicExceptionResult(operationResult.Errors.First());
            }
            else
            {
                return new DynamicExceptionResult(new Error
                {
                    Details = new List<Error>(operationResult.Errors)
                });
            }
        }

        public static IDynamicResult InternalServerError(this Controller controller, string code, string message, IEnumerable<Error> details)
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
