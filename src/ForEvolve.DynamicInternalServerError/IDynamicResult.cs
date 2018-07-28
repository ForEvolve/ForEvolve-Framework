using ForEvolve.Contracts.Errors;

namespace ForEvolve.DynamicInternalServerError
{
    public interface IDynamicResult
    {
        Error Error { get; }
    }
}