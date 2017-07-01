using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ForEvolve.AspNetCore
{
    public interface IHttpRequestValueFinder
    {
        string Find(string key);
    }
}