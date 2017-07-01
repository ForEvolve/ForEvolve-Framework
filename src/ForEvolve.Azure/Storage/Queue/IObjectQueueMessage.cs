using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Queue
{
    public interface IObjectQueueMessage<TMessage> : IQueueMessage
    {
        TMessage Value { get; }
        Exception DeserializationError { get; }
        bool HasDeserializationError();
    }
}