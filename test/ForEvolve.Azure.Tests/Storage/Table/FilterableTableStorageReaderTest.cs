using ForEvolve.Testing;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Azure.Storage.Table
{
    [Trait(DependencyTrait.Name, DependencyTrait.Values.AzureStorageTable)]
    public class FilterableTableStorageReaderTest
    {
        private readonly ITableStorageSettings _settings;
        private readonly IFilterableTableStorageReader<MyTestModel> sut;

        private readonly TableStorageRepository<MyTestModel> _myTestModelRepository;


        public FilterableTableStorageReaderTest()
        {
            _settings = new CosmosDbLocalEmulatorSettings("FilterableTableStorageReaderTest");
            sut = new FilterableTableStorageReader<MyTestModel>(_settings);
            _myTestModelRepository = new TableStorageRepository<MyTestModel>(_settings);
        }

        public class ReadAsync : FilterableTableStorageReaderTest
        {
            [Fact]
            public async Task Should_return_expected_entities()
            {
                // Arrange
                await _myTestModelRepository.InsertOrReplaceAsync(new MyTestModel {
                    PartitionKey = "ReadAsyncPartition",
                    RowKey = "ReadAsyncRow1",
                    Name = "My Name 1"
                });
                await _myTestModelRepository.InsertOrReplaceAsync(new MyTestModel {
                    PartitionKey = "ReadAsyncPartition",
                    RowKey = "ReadAsyncRow2",
                    Name = "My Name 2"
                });
                await _myTestModelRepository.InsertOrReplaceAsync(new MyTestModel {
                    PartitionKey = "ReadAsyncPartition",
                    RowKey = "ReadAsyncRow3",
                    Name = "Some other name structure"
                });
                await _myTestModelRepository.InsertOrReplaceAsync(new MyTestModel
                {
                    PartitionKey = "AnotherPartition",
                    RowKey = "ReadAsyncRow4",
                    Name = "My Name 3"
                });

                // Act
                var result = await sut.ReadAsync(query =>
                {
                    var projectFilter = TableQuery.GenerateFilterCondition(
                        nameof(MyTestModel.PartitionKey), 
                        QueryComparisons.Equal, 
                        "ReadAsyncPartition"
                    );
                    var nameFilter = TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition(
                            nameof(MyTestModel.Name),
                            QueryComparisons.GreaterThanOrEqual,
                            "My Name"
                        ),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition(
                            nameof(MyTestModel.Name),
                            QueryComparisons.LessThan,
                            "My Namf"
                        )
                    );
                    var combinedFilters = TableQuery.CombineFilters(
                        projectFilter, 
                        TableOperators.And, 
                        nameFilter
                    );
                    return query.Where(combinedFilters);
                });

                // Assert
                Assert.NotNull(result);
                Assert.Collection(result.OrderBy(x => x.Name),
                    model => Assert.Equal("My Name 1", model.Name),
                    model => Assert.Equal("My Name 2", model.Name)
                );
            }
        }

        public class MyTestModel : TableEntity
        {
            public string Name { get; set; }
        }
    }
}
