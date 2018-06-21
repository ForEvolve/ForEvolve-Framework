using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;

namespace ForEvolve.Azure.ApplicationInsights
{
    public class TelemetryClientWrapper : ITelemetryClient
    {
        private readonly TelemetryClient _telemetryClient;
        public TelemetryClientWrapper(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        public void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            _telemetryClient.TrackException(exception, properties, metrics);
        }
    }
}
