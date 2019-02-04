using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Read.Domain.Models
{
    public class ReadModel : TableEntity
    {
        public string Epc { get; set; }
        public string Ip { get; set; }
        public int Antenna { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
