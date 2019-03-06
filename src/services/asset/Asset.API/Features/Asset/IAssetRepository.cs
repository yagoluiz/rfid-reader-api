using Asset.API.Features.Asset.Get;
using System.Threading.Tasks;

namespace Asset.API.Features.Asset
{
    public interface IAssetRepository
    {
        Task<AssetItemReadGet> GetAssetItemRead(string epc);
    }
}
