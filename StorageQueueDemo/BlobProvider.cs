using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageQueueDemo
{
    public class BlobProvider
    {
        private string _connectionString;

        public BlobProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CloudBlobContainer GetContainer(string containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            container.CreateIfNotExists();
            return container;
        }

        private string GetUri(string blobName)
        {
            return "https://" + blobName + ".blob.core.windows.net/";
        }
        
    }
}
