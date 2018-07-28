using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForEvolve.DynamicInternalServerError
{
    public class DynamicBadRequestResult : ObjectResult, IDynamicResult
    {
        public DynamicBadRequestResult(Error error)
            : base(new ErrorResponse(error))
        {
            StatusCode = StatusCodes.Status400BadRequest;
            Error = error;
        }

        public Error Error { get; }
    }
}
