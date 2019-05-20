using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Asset.API.Features.Asset
{
    public class AssetServiceBus : IAssetServiceBus
    {
        private readonly AssetContext _assetContext;
        private readonly ILogger<AssetServiceBus> _logger;

        public AssetServiceBus(AssetContext assetContext, ILogger<AssetServiceBus> logger)
        {
            _assetContext = assetContext;
            _logger = logger;
        }

        public async Task<string> ReceiveAsync()
        {
            var message = string.Empty;

            var messageReceive = await _assetContext.MessageReceiver.ReceiveAsync();
            await _assetContext.MessageReceiver.CloseAsync();

            if (messageReceive == null) return message;

            message = Encoding.UTF8.GetString(messageReceive.Body);

            return message;
        }
    }
}
