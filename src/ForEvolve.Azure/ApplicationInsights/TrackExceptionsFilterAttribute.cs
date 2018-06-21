using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace ForEvolve.Azure.ApplicationInsights
{
    public class TrackExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ITelemetryClient _telemetryClient;
        public TrackExceptionsFilterAttribute(ITelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        public override void OnException(ExceptionContext context)
        {
            if (context != null && context.Exception != null)
            {
                _telemetryClient.TrackException(context.Exception);
            }
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            return base.OnExceptionAsync(context);
        }
    }
}
