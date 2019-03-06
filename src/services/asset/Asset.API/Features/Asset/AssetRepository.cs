using System.Threading.Tasks;
using Asset.API.Features.Asset.Get;
using Dapper;

namespace Asset.API.Features.Asset
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetContext _assetContext;

        public AssetRepository(AssetContext assetContext)
        {
            _assetContext = assetContext;
        }

        public async Task<AssetItemReadGet> GetAssetItemRead(string epc)
        {
            var query = @"SELECT i.Name AS ItemName, l.Name AS LocalName, t.Name AS TypeName, i.Epc
                FROM dbo.AssetItem i
                INNER JOIN dbo.AssetLocal l
                ON i.AssetLocalId = l.Id
                INNER JOIN dbo.AssetType t
                ON i.AssetTypeId = t.Id
                WHERE i.Epc = @Epc";

            return await _assetContext.AssetConnection.QueryFirstOrDefaultAsync<AssetItemReadGet>(query, new { Epc = epc });
        }
    }
}
