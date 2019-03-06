using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Asset.API.Features.Asset.Get
{
    public class AssetGetHandler : IRequestHandler<AssetItemReadGetRequest, AssetItemReadGet>
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetServiceBus _assertServiceBus;

        public AssetGetHandler(IAssetRepository assetRepository, IAssetServiceBus assertServiceBus)
        {
            _assetRepository = assetRepository;
            _assertServiceBus = assertServiceBus;
        }

        public async Task<AssetItemReadGet> Handle(AssetItemReadGetRequest request, CancellationToken cancellationToken)
        {
            var message = await _assertServiceBus.ReceiveAsync();

            if (string.IsNullOrEmpty(message)) return new AssetItemReadGet();

            var assetEpcRead = JsonConvert.DeserializeObject<AssetEpcReadGet>(message);

            return await _assetRepository.GetAssetItemRead(assetEpcRead.Epc);
        }
    }
}
