using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    public class DefaultErrorFromOperationResultFactory : IErrorFromOperationResultFactory
    {
        public Error Create(IOperationResult operationResult)
        {
            if (operationResult.Errors.Count() == 1)
            {
                return operationResult.Errors.First();
            }
            else
            {
                return new Error
                {
                    Code = "OperationResultErrors",
                    Message = "Multiple errors occured in an operation. See Details for more information.",
                    Details = new List<Error>(operationResult.Errors)
                };
            }
        }
    }
}
