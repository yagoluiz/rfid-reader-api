using Log.API.Features.Log.List;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Log.API.Features.Log
{
    public class LogRepository : ILogRepository
    {
        private readonly IConfiguration _configuration;
        private readonly LogContext _logContext;

        public LogRepository(IConfiguration configuration, LogContext logContext)
        {
            _configuration = configuration;
            _logContext = logContext;
        }

        public async Task<IEnumerable<LogList>> GetAllLastReadByLimit(int limit = 10)
        {
            var logs = new List<LogList>();

            var container = _logContext.BlobClient.GetContainerReference(_configuration["ContainerBlob"]);

            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            var blobs = await container.ListBlobsSegmentedAsync(
                prefix: null,
                useFlatBlobListing: false,
                blobListingDetails: BlobListingDetails.None,
                maxResults: limit,
                currentToken: null,
                options: null,
                operationContext: null);

            foreach (var blob in blobs.Results)
            {
                var cloudBlockBlob = container.GetBlockBlobReference(blob.Uri.AbsolutePath.Replace($"/{_configuration["ContainerBlob"]}/", string.Empty));

                var blobLog = await cloudBlockBlob.DownloadTextAsync();
                var log = JsonConvert.DeserializeObject<LogList>(blobLog);

                logs.Add(log);
            }

            return logs;
        }
    }
}
