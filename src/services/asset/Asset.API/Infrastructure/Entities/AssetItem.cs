using System;

namespace Asset.API.Infrastructure.Entities
{
    public class AssetItem
    {
        public AssetItem(Guid id, int assetLocalId, int assetTypeId, string name, string identifierNumber, string serialNumber, string epc, bool situation, DateTime dateCreated)
        {
            Id = id;
            AssetLocalId = assetLocalId;
            AssetTypeId = assetTypeId;
            Name = name;
            IdentifierNumber = identifierNumber;
            SerialNumber = serialNumber;
            Epc = epc;
            Situation = situation;
            DateCreated = dateCreated;
        }

        public Guid Id { get; private set; }
        public int AssetLocalId { get; private set; }
        public int AssetTypeId { get; private set; }
        public string Name { get; private set; }
        public string IdentifierNumber { get; private set; }
        public string SerialNumber { get; private set; }
        public string Epc { get; private set; }
        public bool Situation { get; private set; }
        public DateTime DateCreated { get; private set; }
        public AssetLocal Local { get; private set; }
        public AssetType Type { get; private set; }
    }
}
