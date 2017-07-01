using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Queue
{
    public interface IQueueMessage
    {
        string OriginalMessage { get; }
        byte[] OriginalMessageBytes { get; }

        string MessageId { get; }
        string PopReceipt { get; }
        DateTimeOffset? InsertionTime { get; }
        DateTimeOffset? ExpirationTime { get; }
        DateTimeOffset? NextVisibleTime { get; }
        int DequeueCount { get; }
    }
}