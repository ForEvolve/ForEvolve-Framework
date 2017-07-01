using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Queue
{
    public interface IObjectQueueStorageRepository<TMessage>
    {
        Task AddMessageAsync(TMessage message);
        Task<IObjectQueueMessage<TMessage>> GetMessageAsync();
        Task<IEnumerable<IObjectQueueMessage<TMessage>>> GetMessagesAsync(int messageCount);
    }
}