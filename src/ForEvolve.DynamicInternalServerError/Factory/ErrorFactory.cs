using ForEvolve.Api.Contracts.Errors;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ForEvolve.DynamicInternalServerError
{
    public class ErrorFactory : IErrorFactory
    {
        public Error CreateErrorFor<TException>(TException ex) where TException : Exception
        {
            var error = new Error
            {
                //TODO: add a unit test for this - ArgumentException == ArgumentException and not Exception
                // Was typeof(TException).Name
                Code = ex.GetType().Name,
                Message = ex.Message
            };
            if (ex.InnerException != null)
            {
                error.Details = new List<Error>
                {
                    CreateErrorFor(ex.InnerException)
                };
            }
            return error;
        }

        public Error CreateErrorFor(SerializableError serializableError)
        {
            var error = new Error
            {
                Code = "BadRequest",
                Message = "One or more error occured during model validation.",
                Details = serializableError
                    .Select(v => CreateErrorFor(v)).ToList()
            };
            return error;
        }

        public Error CreateErrorFor(KeyValuePair<string, object> value)
        {
            var error = new Error
            {
                Code = "ModelStateValidationError",
                Target = value.Key
            };

            // TODO: unit test these scenarios
            switch (value.Value)
            {
                case string str:
                    error.Message = str;
                    break;
                case IEnumerable<string> strCollection:
                    if (strCollection.Count() == 1)
                    {
                        error.Message = strCollection.First();
                    }
                    else
                    {
                        error.Details = strCollection
                            .Select(msg => new Error
                            {
                                Code = "ModelStateValidationErrorMessage",
                                Message = msg
                            }).ToList();
                    }
                    break;
                default:
                    error.Message = value.ToString();
                    break;
            }

            return error;
        }
    }
}
