#if SUPPORT_AZURE_EMULATOR
using ForEvolve.Testing;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ForEvolve.Azure.Storage.Table
{
    [Trait(DependencyTrait.Name, DependencyTrait.Values.AzureStorageTable)]
    public class TableStorageRepositoryTest
    {
        protected TableStorageRepository<SomeTestEntity> RepositoryUnderTest { get; }
        protected DevelopmentTableStorageSettings Settings { get; }
        private const string DefaultTableName = "MyTestTable";

        public TableStorageRepositoryTest(string tableName = DefaultTableName)
        {
            Settings = new DevelopmentTableStorageSettings { TableName = tableName };
            RepositoryUnderTest = new TableStorageRepository<SomeTestEntity>(Settings);
        }

        public class ReadPartitionAsync : TableStorageRepositoryTest
        {
            [Fact]
            public async void Should_return_an_enumerable()
            {
                // Arrange
                const string partitionName = "ReadPartitionAsync";
                await RepositoryUnderTest.InsertOrReplaceAsync(new SomeTestEntity
                {
                    PartitionKey = partitionName,
                    RowKey = "Key1"
                });
                await RepositoryUnderTest.InsertOrReplaceAsync(new SomeTestEntity
                {
                    PartitionKey = partitionName,
                    RowKey = "Key2"
                });

                // Act
                var result = await RepositoryUnderTest.ReadPartitionAsync(partitionName);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count());
            }
        }

        public class ReadOneAsync : TableStorageRepositoryTest
        {
            [Fact]
            public async void Should_return_an_entity()
            {
                // Arrange
                const string partitionName = "ReadOneAsync";
                const string rowKey = "MyKey";
                const string expectedValue = "SomeValue";
                var entity = new SomeTestEntity
                {
                    PartitionKey = partitionName,
                    RowKey = rowKey,
                    SomeProp = expectedValue
                };
                await RepositoryUnderTest.InsertOrReplaceAsync(entity);

                // Act
                var result = await RepositoryUnderTest.ReadOneAsync(partitionName, rowKey);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(partitionName, result.PartitionKey);
                Assert.Equal(rowKey, result.RowKey);
                Assert.Equal(expectedValue, result.SomeProp);
            }
        }

        public class ReadAllAsync : TableStorageRepositoryTest
        {
            public ReadAllAsync()
                : base("ReadAllAsyncTestTable")
            {

            }

            [Fact]
            public async void Should()
            {
                // Arrange
                await RepositoryUnderTest.InsertOrReplaceAsync(new SomeTestEntity
                {
                    PartitionKey = "Partition1",
                    RowKey = "Key1"
                });
                await RepositoryUnderTest.InsertOrReplaceAsync(new SomeTestEntity
                {
                    PartitionKey = "Partition1",
                    RowKey = "Key2"
                });
                await RepositoryUnderTest.InsertOrReplaceAsync(new SomeTestEntity
                {
                    PartitionKey = "Partition2",
                    RowKey = "Key1"
                });

                // Act
                var result = await RepositoryUnderTest.ReadAllAsync();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(3, result.Count());
            }
        }

        public class InsertOrMergeAsync : TableStorageRepositoryTest
        {
            [Fact]
            public async void Should_insert_or_merge_an_entity()
            {
                // Arrange
                const string partitionName = "InsertOrMergeAsync";
                const string rowKey = "MyKey";
                var entity = new SomeTestEntity
                {
                    PartitionKey = partitionName,
                    RowKey = rowKey
                };

                // Act
                var result = await RepositoryUnderTest.InsertOrMergeAsync(entity);

                // Assert
                Assert.NotNull(result);
            }
        }

        public class InsertOrReplaceAsync : TableStorageRepositoryTest
        {
            [Fact]
            public async void Should_insert_or_replace_an_entity()
            {
                // Arrange
                const string partitionName = "InsertOrReplaceAsync";
                const string rowKey = "MyKey";
                var entity = new SomeTestEntity
                {
                    PartitionKey = partitionName,
                    RowKey = rowKey
                };

                // Act
                var result = await RepositoryUnderTest.InsertOrReplaceAsync(entity);

                // Assert
                Assert.NotNull(result);
            }

        }

        public class DeleteOneAsync : TableStorageRepositoryTest
        {
            [Fact]
            public async void Should_delete_an_entity()
            {
                // Arrange
                const string partitionName = "DeleteOneAsync";
                const string rowKey = "MyKey";
                var entity = new SomeTestEntity
                {
                    PartitionKey = partitionName,
                    RowKey = rowKey
                };
                await RepositoryUnderTest.InsertOrReplaceAsync(entity);

                // Act
                var result = await RepositoryUnderTest.DeleteOneAsync(partitionName, rowKey);

                // Assert
                Assert.NotNull(result);
                var dbResult = await RepositoryUnderTest.ReadOneAsync(partitionName, rowKey);
                Assert.Null(dbResult);
            }
        }

        public class DeletePartitionAsync : TableStorageRepositoryTest
        {
            [Fact]
            public async void Should_delete_a_partition()
            {
                // Arrange
                const string partitionName = "DeletePartitionAsync";
                var entity1 = new SomeTestEntity
                {
                    PartitionKey = partitionName,
                    RowKey = "MyKey1"
                };
                var entity2 = new SomeTestEntity
                {
                    PartitionKey = partitionName,
                    RowKey = "MyKey2"
                };
                await RepositoryUnderTest.InsertOrReplaceAsync(entity1);
                await RepositoryUnderTest.InsertOrReplaceAsync(entity2);

                // Act
                var result = await RepositoryUnderTest.DeletePartitionAsync(partitionName);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count());
                var dbResult = await RepositoryUnderTest.ReadPartitionAsync(partitionName);
                Assert.NotNull(dbResult);
                Assert.Empty(dbResult);
            }
        }

        public class InsertAsync : TableStorageRepositoryTest
        {
            [Fact]
            public async void Should_insert_an_entity()
            {
                // Arrange
                const string partitionName = "InsertAsync";
                const string rowKey = "MyKey";
                const string expectedValue = "SomeValue";
                var entity = new SomeTestEntity
                {
                    PartitionKey = partitionName,
                    RowKey = rowKey,
                    SomeProp = expectedValue
                };

                var persistedEntity = await RepositoryUnderTest.ReadOneAsync(partitionName, rowKey);
                if (persistedEntity != null)
                {
                    await RepositoryUnderTest.DeleteOneAsync(partitionName, rowKey);
                }

                // Act
                var result = await RepositoryUnderTest.InsertAsync(entity);

                // Assert
                Assert.NotNull(result);
                var dbResult = await RepositoryUnderTest.ReadOneAsync(partitionName, rowKey);
                Assert.NotNull(dbResult);
                Assert.Equal(expectedValue, dbResult.SomeProp);
            }
        }

        public class ReplaceAsync : TableStorageRepositoryTest
        {
            [Fact]
            public async void Should_replace_an_entity()
            {
                // Arrange
                const string partitionKey = "ReplaceAsync";
                const string rowKey = "MyKey";
                const string expectedValue = "SomeOtherValue";
                await RepositoryUnderTest.InsertOrReplaceAsync(new SomeTestEntity
                {
                    PartitionKey = partitionKey,
                    RowKey = rowKey,
                    ETag = "*",
                    SomeProp = "SomeValue"
                });

                // Act
                var result = await RepositoryUnderTest.ReplaceAsync(new SomeTestEntity
                {
                    PartitionKey = partitionKey,
                    RowKey = rowKey,
                    ETag = "*",
                    SomeProp = expectedValue
                });

                // Assert
                Assert.NotNull(result);
                var entity = await RepositoryUnderTest.ReadOneAsync(partitionKey, rowKey);
                Assert.Equal(expectedValue, entity.SomeProp);
            }
        }

        public class MergeAsync : TableStorageRepositoryTest
        {
            protected TableStorageRepository<SomeOtherTestEntity> SomeOtherTestEntityRepository { get; }

            public MergeAsync()
            {
                SomeOtherTestEntityRepository = new TableStorageRepository<SomeOtherTestEntity>(Settings);
            }

            [Fact]
            public async void Should_merge_the_entities()
            {
                // Arrange
                const string partitionKey = "MergeAsync";
                const string rowKey = "MyKey";
                const string expectedSomePropValue = "SomeFinalValue";
                const string expectedSomeOtherPropValue = "SomeOtherValue";
                await SomeOtherTestEntityRepository.InsertOrReplaceAsync(new SomeOtherTestEntity
                {
                    PartitionKey = partitionKey,
                    RowKey = rowKey,
                    ETag = "*",
                    SomeOtherProp = expectedSomeOtherPropValue
                });

                // Act
                var result = await RepositoryUnderTest.MergeAsync(new SomeTestEntity
                {
                    PartitionKey = partitionKey,
                    RowKey = rowKey,
                    ETag = "*",
                    SomeProp = expectedSomePropValue
                });

                // Assert
                var entity1 = await RepositoryUnderTest.ReadOneAsync(partitionKey, rowKey);
                var entity2 = await SomeOtherTestEntityRepository.ReadOneAsync(partitionKey, rowKey);
                Assert.NotNull(entity1);
                Assert.NotNull(entity2);
                Assert.Equal(expectedSomePropValue, entity1.SomeProp);
                Assert.Equal(expectedSomeOtherPropValue, entity2.SomeOtherProp);
            }
        }

        public class SomeTestEntity : TableEntity
        {
            public string SomeProp { get; set; }
        }
        public class SomeOtherTestEntity : TableEntity
        {
            public string SomeOtherProp { get; set; }
        }
    }
}
#endif