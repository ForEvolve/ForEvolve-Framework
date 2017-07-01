using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Queue
{
    public class QueueStorageRepository : StorageRepository<IQueueStorageSettings>, IQueueStorageRepository
    {
        public QueueStorageRepository(IQueueStorageSettings storageSettings)
            : base(storageSettings)
        {

        }

        protected async Task<CloudQueue> GetQueueAsync()
        {
            var storageAccount = StorageSettings.CreateCloudStorageAccount();
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(StorageSettings.QueueName);
            await queue.CreateIfNotExistsAsync();
            return queue;
        }

        public async Task AddMessageAsync(string message)
        {
            var queueMessage = new CloudQueueMessage(message);
            var queue = await GetQueueAsync();
            await queue.AddMessageAsync(queueMessage);
        }

        public async Task<IQueueMessage> PeekMessageAsync()
        {
            var queue = await GetQueueAsync();
            var message = await queue.PeekMessageAsync();
            return new QueueMessage(message);
        }

        public async Task<IQueueMessage> GetMessageAsync()
        {
            var queue = await GetQueueAsync();
            var message = await queue.GetMessageAsync();
            return new QueueMessage(message);
        }

        public async Task<IEnumerable<IQueueMessage>> GetMessagesAsync(int messageCount)
        {
            var queue = await GetQueueAsync();
            var messages = await queue.GetMessagesAsync(messageCount);
            return messages.Select(m => new QueueMessage(m));
        }

        public async Task<int> GetApproximateQueueLengthAsync()
        {
            var queue = await GetQueueAsync();
            await queue.FetchAttributesAsync();
            return queue.ApproximateMessageCount.GetValueOrDefault();
        }

        public async Task DeleteMessageAsync(IQueueMessage message)
        {
            var queue = await GetQueueAsync();
            await queue.DeleteMessageAsync(message.MessageId, message.PopReceipt);
        }
    }
}
