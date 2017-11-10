using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ForEvolve.Api.Contracts.Errors;

namespace ForEvolve.AspNetCore
{
    public interface IOperationResult
    {
        bool Succeeded { get; }

        IEnumerable<Error> Errors { get; }
        IEnumerable<Exception> Exceptions { get; }

        void AppendError(Error error);
        void AppendException(Exception exception);

        bool HasException();
        bool HasError();
    }
}
