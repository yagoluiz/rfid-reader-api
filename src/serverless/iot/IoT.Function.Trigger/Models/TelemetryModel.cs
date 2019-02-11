using Newtonsoft.Json;
using System;

namespace IoT.Function.Trigger.Models
{
    public class TelemetryModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id => Guid.NewGuid();
        [JsonProperty(PropertyName = "ip")]
        public string Ip { get; set; }
        [JsonProperty(PropertyName = "temperature")]
        public int Temperature { get; set; }
        [JsonProperty(PropertyName = "isConnection")]
        public bool IsConnection { get; set; }
    }
}
