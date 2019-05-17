using System;

namespace IoT.Function.Trigger.Models
{
    public class ReadModel
    {
        public Guid Id => Guid.NewGuid();
        public string Ip { get; set; }
        public string Epc { get; set; }
        public string ReadDate { get; set; }
        public int Antenna { get; set; }
    }
}
