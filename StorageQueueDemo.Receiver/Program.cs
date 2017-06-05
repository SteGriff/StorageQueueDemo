using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;
using StorageQueueDemo;

namespace StorageQueueDemo.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Receiver";
            Console.WriteLine("Get StorageAccount...");

            string connectionString = "YourStorageAccountConnectionString";
            var blobProvider = new BlobProvider(connectionString);
            var queueProvider = new QueueProvider(connectionString);

            Console.WriteLine("Get StorageQueue...");
            var textFileQ = queueProvider.GetQueue("textqueue");

            Console.WriteLine("Get Blob...");
            var container = blobProvider.GetContainer("messages");
            
            Console.WriteLine("OK");
            Console.WriteLine("Processing Queue...");

            while (true)
            {
                //Check if a message is available
                var message = textFileQ.GetMessage();
                if (message != null)
                {
                    Console.WriteLine("Got message - processing...");
                    //Extract the good bit and send it to blob storage
                    var fileNameAndContent = ProcessMessage(message);

                    var file = container.GetBlockBlobReference(fileNameAndContent.Key);
                    file.UploadText(fileNameAndContent.Value);
                    
                    Console.WriteLine("Uploaded '{0}' to '{1}'", fileNameAndContent.Value, fileNameAndContent.Key);

                    //Now that we have processed the message, delete it so it isn't processed again
                    textFileQ.DeleteMessage(message);
                }
                else
                {
                    //No message - wait 
                    Console.WriteLine("No message to process - sleep");
                    System.Threading.Thread.Sleep(5000);
                }
            }
            
        }

        private static KeyValuePair<string,string> ProcessMessage(CloudQueueMessage message)
        {
            string content = message.AsString;
            var lines = content.Split(new string[] { Environment.NewLine }, 10, StringSplitOptions.RemoveEmptyEntries);

            string fileLine = lines.FirstOrDefault();
            string fileName = fileLine + ".txt";

            string messageLine = lines.Where(l => l.Length > 7 && l.Substring(0, 7).ToLower() == "message").FirstOrDefault();
            string messageOnly = messageLine.Substring(8);

            return new KeyValuePair<string, string>(fileName, messageOnly);
        }
    }
}
