using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Table
{
    public interface ITableStorageFactory
    {
        ITableStorageRepository<TModel> CreateRepository<TModel>() where TModel : class, ITableEntity, new();
        IFilterableTableStorageReader<TModel> CreateReader<TModel>() where TModel : class, ITableEntity, new();
    }
}
