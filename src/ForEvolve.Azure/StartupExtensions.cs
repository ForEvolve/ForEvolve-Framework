using ForEvolve.Azure.Storage.Queue;
using ForEvolve.Azure.Storage.Table;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class ForEvolveAzureStartupExtensions
    {
        public static void AddTableStorage(this IServiceCollection services, TableStorageRepositoryFactorySettings factorySettings = null)
        {
            services.TryAddSingleton<ITableStorageRepositoryFactory, TableStorageRepositoryFactory>();
            services.TryAddSingleton(factorySettings ?? new TableStorageRepositoryFactorySettings
            {
                AutoCreateMissingBindings = true
            });
        }

        public static void AddTableMessageQueueStorage(this IServiceCollection services, QueueStorageSettings queueStorageSettings)
        {
            services.TryAddSingleton<IQueueStorageRepository>(new QueueStorageRepository(queueStorageSettings));
            services.TryAddSingleton<ITableMessageQueueStorageRepository, TableMessageQueueStorageRepository>();
        }

        public static void AddTableMessageQueueStorage(this IServiceCollection services)
        {
            services.TryAddSingleton<ITableMessageQueueStorageRepository, TableMessageQueueStorageRepository>();
        }
    }
}
