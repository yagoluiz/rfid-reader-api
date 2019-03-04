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
    }
}
