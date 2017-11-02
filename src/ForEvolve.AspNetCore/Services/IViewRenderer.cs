using System.Threading.Tasks;

namespace ForEvolve.AspNetCore.Services
{
    public interface IViewRenderer
    {
        Task<string> RenderAsync(string viewName, object viewModel);
    }
}
