using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Queue
{
    public class JsonQueueMessage<TMessage> : ObjectQueueMessage<TMessage>
    {
        public JsonQueueMessage(IQueueMessage queueMessage)
            : base(queueMessage, (m) => JsonConvert.DeserializeObject<TMessage>(m.OriginalMessage))
        {
        }
    }
}