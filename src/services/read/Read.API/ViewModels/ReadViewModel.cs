using System;

namespace Read.API.ViewModels
{
    public class ReadViewModel
    {
        public string Epc { get; set; }
        public string Ip { get; set; }
        public int Antenna { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
