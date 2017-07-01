using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.Azure.Storage.Queue
{
    public class QueueStorageSettings : StorageSettings, IQueueStorageSettings
    {
        public string QueueName { get; set; }
    }
}
