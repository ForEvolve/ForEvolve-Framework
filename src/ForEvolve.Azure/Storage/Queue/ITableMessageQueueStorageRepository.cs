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
    public interface ITableMessageQueueStorageRepository : IObjectQueueStorageRepository<TableMessage>
    {

    }
}