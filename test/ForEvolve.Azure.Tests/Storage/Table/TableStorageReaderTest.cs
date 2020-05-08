using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using Microsoft.WindowsAzure.Storage.Table;
using ForEvolve.Testing;
using ForEvolve.XUnit.HttpTests;

namespace ForEvolve.Azure.Storage.Table
{
    [Trait(DependencyTrait.Name, DependencyTrait.Values.AzureStorageTable)]
    public class TableStorageReaderTest : BaseHttpTest
    {
        private ITableStorageReader SubjectUnderTest => Server.Host.Services.GetService<ITableStorageReader>();
        private DevelopmentTableStorageSettings tableStorageSettings2 = new DevelopmentTableStorageSettings
        {
            TableName = "TableStorageReaderTest2"
        };
        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddSingleton<ITableStorageSettings>(new DevelopmentTableStorageSettings
            {
                TableName = "TableStorageReaderTest"
            });
            services.AddForEvolveTableStorage();
        }

        protected override IWebHostBuilder ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder.Configure(async app => {
                var factory = app.ApplicationServices.GetService<ITableStorageFactory>();

                // First table
                var repo = factory.CreateRepository<MyEntity>();
                await repo.InsertOrReplaceAsync(new MyEntity
                {
                    PartitionKey = "MyPartition",
                    RowKey = "MyRow1"
                });
                await repo.InsertOrReplaceAsync(new MyEntity
                {
                    PartitionKey = "MyPartition2",
                    RowKey = "MyRow2"
                });
                await repo.InsertOrReplaceAsync(new MyEntity
                {
                    PartitionKey = "MyPartition",
                    RowKey = "MyRow3"
                });


                // Second table
                var repo2 = factory.CreateRepository<MyEntity>(tableStorageSettings2);
                await repo2.InsertOrReplaceAsync(new MyEntity
                {
                    PartitionKey = "MyPartition",
                    RowKey = "MyRow1"
                });
                await repo2.InsertOrReplaceAsync(new MyEntity
                {
                    PartitionKey = "MyPartition",
                    RowKey = "MyRow2"
                });
            });
        }

        [Trait(DependencyTrait.Name, DependencyTrait.Values.AzureStorageTable)]
        public class ReadAsync : TableStorageReaderTest
        {
            [Fact]
            public async Task Should_return_expected_values()
            {
                var result = await SubjectUnderTest.ReadAsync<MyEntity>(query =>
                {
                    return query.Where(TableQuery.GenerateFilterCondition(
                        nameof(MyEntity.PartitionKey),
                        QueryComparisons.Equal,
                        "MyPartition"
                    ));
                });
                Assert.Collection(result,
                    x => Assert.Equal("MyRow1", x.RowKey),
                    x => Assert.Equal("MyRow3", x.RowKey)
                );
            }
        }

        [Trait(DependencyTrait.Name, DependencyTrait.Values.AzureStorageTable)]
        public class ReadAsync_with_ITableStorageSettings : TableStorageReaderTest
        {
            [Fact]
            public async Task Should_return_expected_values()
            {
                var result = await SubjectUnderTest.ReadAsync<MyEntity>(tableStorageSettings2, query =>
                {
                    return query.Where(TableQuery.GenerateFilterCondition(
                        nameof(MyEntity.PartitionKey),
                        QueryComparisons.Equal,
                        "MyPartition"
                    ));
                });
                Assert.Collection(result,
                    x => Assert.Equal("MyRow1", x.RowKey),
                    x => Assert.Equal("MyRow2", x.RowKey)
                );
            }
        }

        private class MyEntity : TableEntity
        {
        }
    }
}
