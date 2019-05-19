using Newtonsoft.Json;
using System;

namespace IoT.Function.Trigger.Models
{
    public class TelemetryModel
    {
        [JsonProperty("id")]
        public Guid Id => Guid.NewGuid();
        [JsonProperty("ip")]
        public string Ip { get; set; }
        [JsonProperty("temperature")]
        public int Temperature { get; set; }
        [JsonProperty("isConnection")]
        public bool IsConnection { get; set; }
    }
}
