using ForEvolve.Contracts.Errors;
using ForEvolve.AspNetCore.ErrorFactory.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.DynamicInternalServerError.Swagger
{
    internal static class ExamplesFactory
    {
        public static ErrorResponse CreateDynamicException()
        {
            // Base error
            var error = new Error
            {
                Code = "ExempleErrorCode",
                Message = "This is an error message."
            };

            // Inner error
            error.InnerError = new InnerError { Code = "SomeErrorCode" };
            error.InnerError.Add("someProperty", "Some error happened!");

            // Details
            error.Details = new List<Error>
            {
                new Error
                {
                    Code = "SomeDetailsErrorCode",
                    Message = "Some nested error!",
                    Target = "SomeHardToFindTarget"
                }
            };

            // Return the error response object
            return new ErrorResponse(error);
        }

        public static ErrorResponse CreateDynamicValidation()
        {
            var factory = new DefaultErrorFromSerializableErrorFactory(new DefaultErrorFromKeyValuePairFactory(new DefaultErrorFromRawValuesFactory()));
            var modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError("Property1", "Some validation error.");
            modelStateDictionary.AddModelError("Property1", "Some more validation error.");
            modelStateDictionary.AddModelError("Property2", "This is bad!");
            modelStateDictionary.AddModelError("Property3", "This is very bad!");
            modelStateDictionary.AddModelError("Property4", "This is even worst!");
            var error = factory.Create(new SerializableError(modelStateDictionary));
            return new ErrorResponse(error);
        }
    }
}
