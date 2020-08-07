using AwesomeShop.AzureFunctions.Email;
using AwesomeShop.AzureQueueLibrary.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AwesomeShop.AzureFunctions.Infrastructure
{
    public sealed class DIContainer
    {
        private static readonly IServiceProvider _instance = Build();
        public static IServiceProvider Instance => _instance;

        static DIContainer()
        {

        }

        private DIContainer()
        {

        }

        private static IServiceProvider Build()
        {
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

            services.AddSingleton(new EmailConfig(
                host: configuration["EmailHost"],
                port: Convert.ToInt32(configuration["EmailPort"]),
                sender: configuration["EmailSender"],
                password: configuration["EmailPassword"]
                ));
            services.AddSingleton<ISendEmailCommandHandler, SendEmailCommandHandler>();
            services.AddAzureQueueLibrary(configuration["AzureWebJobsStorage"]);

            return services.BuildServiceProvider();
        }
    }

}
