using System.Threading.Tasks;

namespace Asset.API.Features.Asset
{
    public interface IAssetServiceBus
    {
        Task<string> ReceiveAsync();
    }
}
