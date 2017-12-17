using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.XUnit.HttpTests
{
    public class EmptyResponseProvider : IResponseProvider
    {
        public string ResponseText(HttpContext context)
        {
            return "";
        }
    }
}
