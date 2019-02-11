using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace IoT.Function.Trigger.Context
{
    public static class StorageContext
    {
        private static CloudStorageAccount _cloudStorageAccount;
        private static readonly string _connectionStringBlob = Environment.GetEnvironmentVariable("AzureWebJobsStorage", EnvironmentVariableTarget.Process);

        public static CloudBlobClient BlobClient
        {
            get
            {
                _cloudStorageAccount = _cloudStorageAccount ?? CloudStorageAccount.Parse(_connectionStringBlob);

                return _cloudStorageAccount.CreateCloudBlobClient();
            }
        }
    }
}
