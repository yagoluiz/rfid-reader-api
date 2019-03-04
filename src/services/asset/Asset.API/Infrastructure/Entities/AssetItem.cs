using System;

namespace Asset.API.Infrastructure.Entities
{
    public class AssetItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IdentifierNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Epc { get; set; }
        public bool Situation { get; set; }
        public DateTime DateCreated { get; set; }
        public AssetLocal Local { get; set; }
        public AssetType Type { get; set; }
    }
}
