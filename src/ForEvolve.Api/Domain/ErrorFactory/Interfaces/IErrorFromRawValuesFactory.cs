using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.Api.Domain
{
    public interface IErrorFromRawValuesFactory
    {
        Error Create(string errorCode, string errorTarget, object errorMessage);
    }
}
