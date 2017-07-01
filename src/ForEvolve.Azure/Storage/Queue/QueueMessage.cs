using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Queue
{
    public class QueueMessage : IQueueMessage
    {
        public QueueMessage(IQueueMessage queueMessage)
        {
            OriginalMessage = queueMessage.OriginalMessage;
            OriginalMessageBytes = queueMessage.OriginalMessageBytes;
            MessageId = queueMessage.MessageId;
            PopReceipt = queueMessage.PopReceipt;
            InsertionTime = queueMessage.InsertionTime;
            ExpirationTime = queueMessage.ExpirationTime;
            NextVisibleTime = queueMessage.NextVisibleTime;
            DequeueCount = queueMessage.DequeueCount;
        }

        public QueueMessage(CloudQueueMessage cloudQueueMessage)
        {
            OriginalMessage = cloudQueueMessage.AsString;
            OriginalMessageBytes = cloudQueueMessage.AsBytes;
            MessageId = cloudQueueMessage.Id;
            PopReceipt = cloudQueueMessage.PopReceipt;
            InsertionTime = cloudQueueMessage.InsertionTime;
            ExpirationTime = cloudQueueMessage.ExpirationTime;
            NextVisibleTime = cloudQueueMessage.NextVisibleTime;
            DequeueCount = cloudQueueMessage.DequeueCount;
        }
        public string OriginalMessage { get; }
        public byte[] OriginalMessageBytes { get; }
        public string MessageId { get; }
        public string PopReceipt { get; }
        public DateTimeOffset? InsertionTime { get; }
        public DateTimeOffset? ExpirationTime { get; }
        public DateTimeOffset? NextVisibleTime { get; }
        public int DequeueCount { get; }
    }
}