using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore.Services
{
    public interface IViewRendererService
    {
        Task<string> RenderAsync(string viewName, object viewModel);
        Task<string> RenderAsync(string controllerName, string viewName, object viewModel);
        Task<string> RenderAsync(string viewName, object viewModel, HttpContext httpContext, ActionContext actionContext);
    }
}
