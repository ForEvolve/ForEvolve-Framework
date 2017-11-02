using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.AspNetCore
{
    public interface IErrorFromRawValuesFactory
    {
        Error Create(string errorCode, string errorTarget, object errorMessage);
    }
}
