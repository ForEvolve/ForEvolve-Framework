using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Http;
using System.Linq;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.DynamicInternalServerError.Swagger
{
    public class DynamicValidationActionOperationFilter : IOperationFilter
    {
        public readonly string Status400BadRequest = StatusCodes.Status400BadRequest.ToString();

        public void Apply(Operation operation, OperationFilterContext context)
        {
            // Override the default 400 behaviour to action that define it, with specifying a type.
            if (operation.Responses.ContainsKey(Status400BadRequest))
            {
                var badRequestResponse = operation.Responses.FirstOrDefault(x => x.Key == Status400BadRequest);
                if (badRequestResponse.Value.Schema == null)
                {
                    badRequestResponse.Value.Description = "Bad Request";
                    badRequestResponse.Value.Schema = new Schema
                    {
                        Ref = $"#/definitions/{nameof(ErrorResponse)}",
                    };
                    badRequestResponse.Value.Examples = ExamplesFactory.CreateDynamicValidation();
                }
            }
        }
    }
}
