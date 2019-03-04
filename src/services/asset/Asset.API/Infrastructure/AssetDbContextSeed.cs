using Asset.API.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.API.Infrastructure
{
    public class AssetDbContextSeed
    {
        public async Task SeedAsync(AssetDbContext context)
        {
            if (!context.AssetTypes.Any())
            {
                var types = new List<AssetType>()
                {
                    new AssetType()
                    {
                        Name = "Eletronic"
                    },
                    new AssetType()
                    {
                        Name = "Furniture"
                    }
                };

                await context.AddRangeAsync(types);
                await context.SaveChangesAsync();
            }
        }
    }
}