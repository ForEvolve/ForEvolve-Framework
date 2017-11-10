using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.AspNetCore
{
    public interface IOperationResult<TResult> : IOperationResult
    {
        TResult Result { get; set; }
        bool HasResult();
    }
}
