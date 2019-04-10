using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Log.API.Features.Log
{
    public class LogContext
    {
        private readonly IConfiguration _configuration;

        public LogContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CloudBlobClient BlobClient =>
            CloudStorageAccount.Parse(_configuration["AzureStorage:ConnectionString"]).CreateCloudBlobClient();
    }
}
