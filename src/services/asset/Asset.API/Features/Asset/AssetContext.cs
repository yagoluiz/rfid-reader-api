using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Asset.API.Features.Asset
{
    public class AssetContext
    {
        private readonly IConfiguration _configuration;

        public AssetContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection AssetConnection =>
            new SqlConnection(_configuration["SqlServerDB:ConnectionString"]);

        public IMessageReceiver MessageReceiver =>
            new MessageReceiver(_configuration["ServiceBus:ConnectionString"],
                _configuration["ServiceBus:Queue"], ReceiveMode.ReceiveAndDelete);
    }
}
