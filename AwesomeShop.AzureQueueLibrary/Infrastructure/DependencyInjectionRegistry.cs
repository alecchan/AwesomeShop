using AwesomeShop.AzureQueueLibrary.MessageSerializer;
using AwesomeShop.AzureQueueLibrary.QueueConnection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AwesomeShop.AzureQueueLibrary.Infrastructure
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddAzureQueueLibrary(this IServiceCollection service, string queueConnectionString )
        {
            service.AddSingleton(new QueueConfig(queueConnectionString));
            service.AddSingleton<ICloudQueueClientFactory, CloudQueueClientFactory>();
            service.AddSingleton<IMessageSerializer, JsonMessageSerializer>();
            service.AddTransient<IQueueCommunicator, QueueCommunicator>();

            return service;
        }
    }
}
