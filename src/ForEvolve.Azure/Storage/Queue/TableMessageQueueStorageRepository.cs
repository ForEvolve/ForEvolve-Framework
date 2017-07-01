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
    public class TableMessageQueueStorageRepository : ObjectMessageQueueStorageRepository<TableMessage>, ITableMessageQueueStorageRepository
    {
        public TableMessageQueueStorageRepository(IQueueStorageRepository queueStorageRepository)
            : base(queueStorageRepository)
        {

        }
    }
}