using Newtonsoft.Json;
using System;

namespace IoT.Function.Trigger.Models
{
    public class ReadModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id => Guid.NewGuid();
        [JsonProperty(PropertyName = "ip")]
        public string Ip { get; set; }
        [JsonProperty(PropertyName = "epc")]
        public string Epc { get; set; }
        [JsonProperty(PropertyName = "readDate")]
        public string ReadDate { get; set; }
        [JsonProperty(PropertyName = "antenna")]
        public int Antenna { get; set; }
    }
}
