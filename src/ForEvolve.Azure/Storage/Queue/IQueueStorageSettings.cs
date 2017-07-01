namespace ForEvolve.Azure.Storage.Queue
{
    public interface IQueueStorageSettings : IStorageSettings
    {
        string QueueName { get; set; }
    }
}