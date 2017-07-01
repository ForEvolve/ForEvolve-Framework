//
// Instead of converting my own .NET 4 version of this service, 
// I found an article titled "ASP.NET Core Render View to String".
// I may update this code at some point, until then, thanks to the author(s).
// Link: https://ppolyzos.com/2016/09/09/asp-net-core-render-view-to-string/
//
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore.Services
{
    public interface IViewRenderer
    {
        Task<string> RenderAsync(string viewName, object viewModel);
    }
}
