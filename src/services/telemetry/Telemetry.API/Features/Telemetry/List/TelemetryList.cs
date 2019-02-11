using System;

namespace Telemetry.API.Features.Telemetry.List
{
    public class TelemetryList
    {
        public Guid Id { get; set; }
        public string Ip { get; set; }
        public int Temperature { get; set; }
        public bool IsConnection { get; set; }
    }
}
