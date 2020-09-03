# ForEvolve.Azure

This project aims at adding features over the Azure SDK like Object (Blob), Queue, Table and KeyVault repositories.

## Test

To test the project, you will need:

-   [Azure Cosmos Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=ssl-netstd21#table-api)
-   [Azurite emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite)

You may also need to start the Azure Cosmos Emulator using teh `/EnableTableEndpoint` option, like this:

```powershell
cd "C:\Program Files\Azure Cosmos DB Emulator"
.\Microsoft.Azure.Cosmos.Emulator.exe /EnableTableEndpoint
```
