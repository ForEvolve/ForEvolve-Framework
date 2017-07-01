using ForEvolve.Azure.Storage.Queue;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class ForEvolveAzureStartupExtensions
    {
        public static void AddTableMessageQueueStorage(this IServiceCollection services, QueueStorageSettings queueStorageSettings)
        {
            services.AddSingleton<IQueueStorageRepository>(new QueueStorageRepository(queueStorageSettings));
            services.AddSingleton<ITableMessageQueueStorageRepository, TableMessageQueueStorageRepository>();
        }

        public static void AddTableMessageQueueStorage(this IServiceCollection services)
        {
            services.AddSingleton<ITableMessageQueueStorageRepository, TableMessageQueueStorageRepository>();
        }
    }
}
