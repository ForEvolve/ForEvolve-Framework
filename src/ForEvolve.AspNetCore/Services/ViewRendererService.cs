//
// Instead of converting my own .NET 4 version of this service, 
// I found an article titled "ASP.NET Core Render View to String".
// I may update this code at some point, until then, thanks to the author(s).
// Link: https://ppolyzos.com/2016/09/09/asp-net-core-render-view-to-string/
//
// I made some changes.
//
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace ForEvolve.AspNetCore.Services
{
    public class ViewRendererService : IViewRendererService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public ViewRendererService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            _razorViewEngine = razorViewEngine ?? throw new ArgumentNullException(nameof(razorViewEngine));
            _tempDataProvider = tempDataProvider ?? throw new ArgumentNullException(nameof(tempDataProvider));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task<string> RenderAsync(string viewName, object viewModel)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            return RenderAsync(viewName, viewModel, httpContext, actionContext);
        }

        public Task<string> RenderAsync(string controllerName, string viewName, object viewModel)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var routeData = new RouteData();
            routeData.Values.Add("controller", controllerName);
            var actionDescriptor = new ActionDescriptor();
            actionDescriptor.RouteValues.Add("controller", controllerName);
            var actionContext = new ActionContext(
                httpContext,
                routeData,
                actionDescriptor
            );
            return RenderAsync(viewName, viewModel, httpContext, actionContext);
        }

        public async Task<string> RenderAsync(string viewName, object viewModel, HttpContext httpContext, ActionContext actionContext)
        {
            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ForEvolveException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = viewModel
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}
