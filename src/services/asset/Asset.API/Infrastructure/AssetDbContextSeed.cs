using Asset.API.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asset.API.Infrastructure
{
    public class AssetDbContextSeed
    {
        public void SeedInitial(AssetDbContext context)
        {
            if (!context.AssetLocals.Any())
            {
                var locals = new List<AssetLocal>()
                {
                    new AssetLocal(name: "Garage"),
                    new AssetLocal(name: "Concierge")
                };

                context.AddRange(locals);
                context.SaveChanges();
            }

            if (!context.AssetTypes.Any())
            {
                var types = new List<AssetType>()
                {
                    new AssetType(name: "Eletronic"),
                    new AssetType(name: "Furniture")
                };

                context.AddRange(types);
                context.SaveChanges();
            }

            if (!context.AssetItems.Any())
            {
                var localGarageId = context.AssetLocals.First(x => x.Name == "Garage").Id;
                var localConciergeId = context.AssetLocals.First(x => x.Name == "Concierge").Id;
                var typeEletronicId = context.AssetTypes.First(x => x.Name == "Eletronic").Id;
                var typeFurnitureId = context.AssetTypes.First(x => x.Name == "Furniture").Id;

                var items = new List<AssetItem>()
                {
                    new AssetItem(id: Guid.NewGuid(),
                        assetLocalId: localGarageId,
                        assetTypeId: typeEletronicId,
                        name: "Computer",
                        identifierNumber: "A-001",
                        serialNumber: "X6HSZBOBAPY6ZCZ",
                        epc: "494E44553030303030613133",
                        situation: true,
                        dateCreated: DateTime.Now),
                     new AssetItem(id: Guid.NewGuid(),
                        assetLocalId: localGarageId,
                        assetTypeId: typeFurnitureId,
                        name: "Table",
                        identifierNumber: "A-002",
                        serialNumber: "N141SN4Z2ZZRD9K",
                        epc: "414953443030303030303032",
                        situation: true,
                        dateCreated: DateTime.Now),
                     new AssetItem(id: Guid.NewGuid(),
                        assetLocalId: localConciergeId,
                        assetTypeId: typeFurnitureId,
                        name: "Chair",
                        identifierNumber: "A-003",
                        serialNumber: "TTN33BTWDC5E8SR",
                        epc: "E2002083980201001070A87F",
                        situation: true,
                        dateCreated: DateTime.Now)
                };

                context.AddRange(items);
                context.SaveChanges();
            }
        }
    }
}