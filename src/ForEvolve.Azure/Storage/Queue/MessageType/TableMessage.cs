using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.Azure.Storage.Queue.MessageType
{
    public class TableMessage
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
    }
}
