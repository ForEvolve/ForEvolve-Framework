using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ForEvolve.DynamicInternalServerError.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Builder;

namespace ForEvolve.DynamicInternalServerError.Swagger.TWebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .ConfigureServices(services =>
                {
                    // Add DynamicInternalServerError & Swagger
                    services.AddDynamicInternalServerError();
                    services.AddDynamicInternalServerErrorSwagger();

                    // Add Mvc and configure DynamicInternalServerError.
                    services.AddMvc(options =>
                    {
                        options.ConfigureDynamicInternalServerError();
                    });

                    // Add and configure Swagger.
                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                        // Add and configure DynamicInternalServerError.
                        c.AddDynamicInternalServerError();
                    });
                })
                .Configure(app =>
                {
                    // Use MVC
                    app.UseMvc();

                    // Use Swagger & UI
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    });
                })
                .Build();

            host.Run();
        }
    }
}
