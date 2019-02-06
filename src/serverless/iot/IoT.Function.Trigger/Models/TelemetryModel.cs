using Newtonsoft.Json;
using System;

namespace IoT.Function.Trigger.Models
{
    public class TelemetryModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id => Guid.NewGuid();
        public string Ip { get; set; }
        public int Temperature { get; set; }
        public bool IsConnection { get; set; }
    }
}
