using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ForEvolve.Contracts.Errors;
using Microsoft.AspNetCore.Identity;

namespace ForEvolve.AspNetCore
{
    public interface IOperationResult
    {
        bool Succeeded { get; }

        IEnumerable<Error> Errors { get; }
        IEnumerable<Exception> Exceptions { get; }

        void AddError(Error error);
        void AddErrors(IEnumerable<Error> errors);
        void AddException(Exception exception);
        void AddErrorsFrom(IdentityResult result);
        void AddErrorsFrom(IOperationResult result);

        bool HasException();
        bool HasError();
    }
}
