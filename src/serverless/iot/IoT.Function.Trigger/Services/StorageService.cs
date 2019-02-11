using IoT.Function.Trigger.Context;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace IoT.Function.Trigger.Services
{
    public static class StorageService
    {
        private static readonly string _containerLog = Environment.GetEnvironmentVariable("ContainerLog", EnvironmentVariableTarget.Process);

        public static async Task InsertBlobAsync(string blobName, string blob)
        {
            var container = StorageContext.BlobClient.GetContainerReference(_containerLog);

            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            var cloudBlockBlob = container.GetBlockBlobReference($"{blobName}");

            await cloudBlockBlob.UploadTextAsync(blob);
        }
    }
}
