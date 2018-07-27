namespace ForEvolve.Azure.Storage.Blob
{
    public interface IObjectStorageSettings : IStorageSettings
    {
        string ContainerName { get; set; }
    }
}