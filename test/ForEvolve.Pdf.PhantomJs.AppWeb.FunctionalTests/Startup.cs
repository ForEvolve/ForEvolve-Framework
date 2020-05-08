using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ForEvolve.Pdf.Abstractions;
using ForEvolve.Pdf.PhantomJs.AppShared.FunctionalTests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace ForEvolve.Pdf.PhantomJs.AppWeb.FunctionalTests
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddForEvolvePhantomJsHtmlToPdfConverter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var dllDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var targetDirectory = Path.Combine(dllDirectory, "PhantomJs", "pdf-out");
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var htmlToPdfConverter = app.ApplicationServices.GetService<IHtmlToPdfConverter>();
            var options = app.ApplicationServices.GetService<HtmlToPdfConverterOptions>();
            var testRunner = new TestCaseRunner();

            app.Run(async (context) =>
            {
                var results = testRunner.RunAll(htmlToPdfConverter, targetDirectory);
                var json = JsonConvert.SerializeObject(results);
                await context.Response.WriteAsync(json);
            });
        }
    }
}
