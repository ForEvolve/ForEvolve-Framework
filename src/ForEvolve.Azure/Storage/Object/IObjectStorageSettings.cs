namespace ForEvolve.Azure.Storage.Object
{
    public interface IObjectStorageSettings : IStorageSettings
    {
        string ContainerName { get; set; }
    }
}