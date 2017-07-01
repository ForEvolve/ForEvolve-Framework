using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ForEvolve.AspNetCore
{
    public class SetRequestIdAsTraceIdentifierSettings
    {
        public const string DefaultHeaderName = AddRequestIdMiddleware.DefaultHeaderName;
        public const bool DefaultTransferToResponse = true;

        public string HeaderName { get; set; } = DefaultHeaderName;
        public bool TransferToResponse { get; set; } = DefaultTransferToResponse;
    }
}