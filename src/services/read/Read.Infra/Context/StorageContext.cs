using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Read.Infra.Context
{
    public class StorageContext
    {
        private readonly IConfiguration _configuration;
        private CloudStorageAccount _cloudStorageAccount;

        public StorageContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _cloudStorageAccount = CloudStorageAccount.Parse(_configuration.GetConnectionString("Storage"));
        }

        public CloudTable Read
        {
            get
            {
                return _cloudStorageAccount.CreateCloudTableClient().GetTableReference("Read");
            }
        }
    }
}
