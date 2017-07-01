using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace ForEvolve.ApplicationInsights
{
    public class TrackExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        private readonly TelemetryClient _telemetryClient;
        public TrackExceptionsFilterAttribute(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        public override void OnException(ExceptionContext context)
        {
            TrackException(context);
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            TrackException(context);
            return base.OnExceptionAsync(context);
        }

        private void TrackException(ExceptionContext context)
        {
            if (context != null && context.Exception != null)
            {
                _telemetryClient.TrackException(context.Exception);
            }
        }
    }
}
