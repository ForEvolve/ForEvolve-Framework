using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.AspNetCore
{
    public interface IOperationResult<TValue> : IOperationResult
    {
        TValue Value { get; set; }
        bool HasResult();
    }
}
