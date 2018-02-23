using Microsoft.AspNetCore.Mvc;

namespace ForEvolve.DynamicInternalServerError
{
    public interface IDynamicActionResult : IActionResult, IDynamicResult
    {

    }
}