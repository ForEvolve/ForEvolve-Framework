using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Queue
{
    public interface IQueueStorageRepository
    {
        Task AddMessageAsync(string message);
        Task<IQueueMessage> PeekMessageAsync();
        Task<IQueueMessage> GetMessageAsync();
        Task<IEnumerable<IQueueMessage>> GetMessagesAsync(int messageCount);
        Task DeleteMessageAsync(IQueueMessage message);

        Task<int> GetApproximateQueueLengthAsync();
    }
}
