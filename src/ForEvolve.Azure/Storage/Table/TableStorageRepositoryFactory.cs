using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ForEvolve.Azure.Storage.Table
{
    public class TableStorageRepositoryFactory : ITableStorageRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TableStorageRepositoryFactorySettings _settings;

        public TableStorageRepositoryFactory(TableStorageRepositoryFactorySettings settings, IServiceProvider serviceProvider)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public ITableStorageRepository<TModel> CreateRepository<TModel>()
            where TModel : class, ITableEntity, new()
        {
            var repo = _serviceProvider.GetService<ITableStorageRepository<TModel>>();
            if (_settings.AutoCreateMissingBindings && repo == null)
            {
                var storageSettings = _serviceProvider.GetService<ITableStorageSettings>();
                repo = new TableStorageRepository<TModel>(storageSettings);
            }
            return repo;
        }

        public IFilterableTableStorageReader<TModel> CreateReader<TModel>()
            where TModel : class, ITableEntity, new()
        {
            var reader = _serviceProvider.GetService<IFilterableTableStorageReader<TModel>>();
            if (_settings.AutoCreateMissingBindings && reader == null)
            {
                var storageSettings = _serviceProvider.GetService<ITableStorageSettings>();
                reader = new FilterableTableStorageReader<TModel>(storageSettings);
            }
            return reader;
        }
    }

    public class TableStorageRepositoryFactorySettings
    {
        public bool AutoCreateMissingBindings { get; set; }
    }
}
