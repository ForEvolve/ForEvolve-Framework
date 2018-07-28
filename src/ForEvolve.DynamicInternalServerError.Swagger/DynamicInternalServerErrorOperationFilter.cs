using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.DynamicInternalServerError.Swagger
{
    public class DynamicInternalServerErrorOperationFilter : IOperationFilter
    {
        public readonly string Status500InternalServerError = StatusCodes.Status500InternalServerError.ToString();

        public void Apply(Operation operation, OperationFilterContext context)
        {
            // Register DynamicException
            var name = nameof(ErrorResponse);
            if (!context.SchemaRegistry.Definitions.ContainsKey(name))
            {
                context.SchemaRegistry.GetOrRegister(typeof(ErrorResponse));
            }

            // Register the new default 500 behaviour
            if (!operation.Responses.ContainsKey(Status500InternalServerError))
            {
                var response = new Response
                {
                    Description = "Internal Server Error",
                    Schema = new Schema
                    {
                        Ref = $"#/definitions/{nameof(ErrorResponse)}",
                    },
                    Examples = ExamplesFactory.CreateDynamicException()
                };
                operation.Responses.Add(Status500InternalServerError, response);
            }
        }
    }
}
