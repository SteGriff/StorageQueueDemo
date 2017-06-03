using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageQueueDemo
{
    public class QueueProvider
    {
        private string _connectionString;

        public QueueProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CloudQueue GetQueue(string queueName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(queueName);

            queue.CreateIfNotExists();
            return queue;
        }
    }
}
