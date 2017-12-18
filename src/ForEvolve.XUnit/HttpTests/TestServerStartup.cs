using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.XUnit.HttpTests
{
    public class TestServerStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IResponseProvider, EmptyResponseProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(context =>
            {
                // Get services
                var statusCodeProvider = app.ApplicationServices.GetRequiredService<IStatusCodeProvider>();
                var responseProvider = app.ApplicationServices.GetRequiredService<IResponseProvider>();

                // Set response 
                context.Response.StatusCode = statusCodeProvider.StatusCode;
                var responseText = responseProvider.ResponseText(context);
                return responseText == null ? Task.CompletedTask : context.Response.WriteAsync(responseText);
            });
        }
    }
}
