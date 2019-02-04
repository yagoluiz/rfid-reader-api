namespace Monitoring.Domain.Models
{
    public class TelemetryModel
    {
        public string Ip { get; set; }
        public int Temperature { get; set; }
        public bool IsConnection { get; set; }
    }
}
