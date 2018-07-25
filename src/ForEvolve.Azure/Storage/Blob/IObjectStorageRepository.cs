using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Blob
{
    public interface IObjectStorageRepository<TModel>
    {
        Task InsertOrUpdateAsync(string fileName, TModel crawl);
        Task DeleteAsync(string crawlName);
        Task<IEnumerable<TModel>> ReadAllAsync();
        Task<TModel> ReadOneAsync(string crawlName);
        Task<IEnumerable<TModel>> ReadAllInDirectoryAsync(string directoryName);
    }
}