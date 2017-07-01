using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Queue
{
    public class ObjectQueueMessage<TMessage> : QueueMessage, IObjectQueueMessage<TMessage>
    {
        public ObjectQueueMessage(IQueueMessage queueMessage, Func<IQueueMessage, TMessage> messageFactory)
            : base(queueMessage)
        {
            try
            {
                Value = messageFactory(queueMessage);
            }
            catch (Exception ex)
            {
                DeserializationError = ex;
            }
        }

        public TMessage Value { get; }

        public Exception DeserializationError { get; }
        public bool HasDeserializationError()
        {
            return DeserializationError != null;
        }
    }
}