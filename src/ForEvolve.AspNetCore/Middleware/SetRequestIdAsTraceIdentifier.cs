using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ForEvolve.AspNetCore
{
    public class SetRequestIdAsTraceIdentifier : BaseMiddleware
    {
        private readonly IHttpRequestValueFinder _httpRequestValueFinder;
        protected ILogger<SetRequestIdAsTraceIdentifier> Logger { get; }
        protected SetRequestIdAsTraceIdentifierSettings Settings { get; }

        public SetRequestIdAsTraceIdentifier(RequestDelegate next, 
            IHttpRequestValueFinder httpRequestValueFinder,
            ILogger<SetRequestIdAsTraceIdentifier> logger,
            SetRequestIdAsTraceIdentifierSettings settings)
            : base(next)
        {
            _httpRequestValueFinder = httpRequestValueFinder ?? throw new ArgumentNullException(nameof(httpRequestValueFinder));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        protected override Task InternalInvokeAsync(HttpContext context)
        {
            Logger.LogTrace($"Entering InternalInvokeAsync with HeaderName: {Settings.HeaderName} and TransferToResponse: {Settings.TransferToResponse}.");
            var requestId = _httpRequestValueFinder.Find(Settings.HeaderName);
            if (!string.IsNullOrWhiteSpace(requestId))
            {
                Logger.LogTrace($"{Settings.HeaderName} found with value of {requestId}.");

                context.TraceIdentifier = requestId;
                if(Settings.TransferToResponse)
                {
                    Logger.LogTrace($"Setting {Settings.HeaderName} response to {requestId}.");
                    context.Response.Headers.Add(Settings.HeaderName, requestId);
                }
            }
            return Task.FromResult(0);
        }
    }
}
