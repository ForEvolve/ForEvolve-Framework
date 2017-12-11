using ForEvolve.DynamicInternalServerError.Swagger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class DynamicInternalServerErrorSwaggerStartupExtensions
    {
        public static IServiceCollection AddDynamicInternalServerErrorSwagger(this IServiceCollection services)
        {
            services.TryAddSingleton<DynamicInternalServerErrorOperationFilter>();
            services.TryAddSingleton<DynamicValidationActionOperationFilter>();
            return services;
        }

        public static SwaggerGenOptions AddDynamicInternalServerError(this SwaggerGenOptions options)
        {
            options.OperationFilter<DynamicInternalServerErrorOperationFilter>();
            options.OperationFilter<DynamicValidationActionOperationFilter>();
            return options;
        }
    }
}
