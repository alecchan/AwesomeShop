using AwesomeShop.AzureQueueLibrary.Infrastructure;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeShop.AzureQueueLibrary.QueueConnection
{

    public interface ICloudQueueClientFactory
    {
        CloudQueueClient GetClient();
    }

    public class CloudQueueClientFactory : ICloudQueueClientFactory
    {
        private readonly QueueConfig _queueConfig;
        private CloudQueueClient _cloudQueueClient;

        public CloudQueueClientFactory(QueueConfig queueConfig)
        {
            _queueConfig = queueConfig;
        }

        public CloudQueueClient GetClient()
        {
            if (_cloudQueueClient != null)
                return _cloudQueueClient;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_queueConfig.QueueConnectionString);
            _cloudQueueClient = storageAccount.CreateCloudQueueClient();

            return _cloudQueueClient;
        }
    }
}
