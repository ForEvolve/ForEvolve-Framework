using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.XUnit.HttpTests
{
    public interface IStatusCodeProvider
    {
        int StatusCode { get; }
    }
}
