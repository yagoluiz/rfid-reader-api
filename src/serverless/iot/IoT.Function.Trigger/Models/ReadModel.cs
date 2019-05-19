using Newtonsoft.Json;
using System;

namespace IoT.Function.Trigger.Models
{
    public class ReadModel
    {
        [JsonProperty("id")]
        public Guid Id => Guid.NewGuid();
        [JsonProperty("ip")]
        public string Ip { get; set; }
        [JsonProperty("epc")]
        public string Epc { get; set; }
        [JsonProperty("readDate")]
        public string ReadDate { get; set; }
        [JsonProperty("antenna")]
        public int Antenna { get; set; }
    }
}
