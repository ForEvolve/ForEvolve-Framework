using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using ForEvolve.Azure.Storage.Queue.MessageType;

namespace ForEvolve.Azure.Storage.Queue
{
    public class ObjectMessageQueueStorageRepository<TMessage> : IObjectQueueStorageRepository<TMessage>
    {
        private readonly IQueueStorageRepository _queueStorageRepository;

        public ObjectMessageQueueStorageRepository(IQueueStorageRepository queueStorageRepository)
        {
            _queueStorageRepository = queueStorageRepository ?? throw new ArgumentNullException(nameof(queueStorageRepository));
        }

        public async Task AddMessageAsync(TMessage message)
        {
            var serializedMessage = JsonConvert.SerializeObject(message);
            await _queueStorageRepository.AddMessageAsync(serializedMessage);
        }

        public async Task<IObjectQueueMessage<TMessage>> GetMessageAsync()
        {
            var message = await _queueStorageRepository.GetMessageAsync();
            return new JsonQueueMessage<TMessage>(message);
        }

        public async Task<IEnumerable<IObjectQueueMessage<TMessage>>> GetMessagesAsync(int messageCount)
        {
            var messages = await _queueStorageRepository.GetMessagesAsync(messageCount);
            return messages.Select(m => new JsonQueueMessage<TMessage>(m));
        }
    }
}