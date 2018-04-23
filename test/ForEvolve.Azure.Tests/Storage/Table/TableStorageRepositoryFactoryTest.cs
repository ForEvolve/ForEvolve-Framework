using ForEvolve.XUnit.HttpTests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage.Table;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Azure.Storage.Table
{
    public class TableStorageRepositoryFactoryTest : BaseHttpTest
    {
        private ITableStorageRepositoryFactory FactoryUnderTest => Server.Host.Services.GetService<ITableStorageRepositoryFactory>();

        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddSingleton<ITableStorageSettings>(new DevelopmentTableStorageSettings
            {
                TableName = "TableStorageRepositoryFactoryTest"
            });
            services.AddTableStorage();
        }

        protected override IWebHostBuilder SetupWebHostBuilder(IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder.Configure(app => { });
        }

        public class CreateRepository : TableStorageRepositoryFactoryTest
        {
            protected override void ConfigureServices(IServiceCollection services)
            {
                base.ConfigureServices(services);
                services.AddSingleton<ITableStorageRepository<MyEntity>, TableStorageRepository<MyEntity>>();
            }

            [Fact]
            public void Should_return_the_expected_implementation()
            {
                var result = FactoryUnderTest.CreateRepository<MyEntity>();
                Assert.IsType<TableStorageRepository<MyEntity>>(result);
            }

            public class When_AutoCreateMissingBindings_is_set_to_true : CreateRepository
            {
                protected override void ConfigureServices(IServiceCollection services)
                {
                    services.AddSingleton(new TableStorageRepositoryFactorySettings
                    {
                        AutoCreateMissingBindings = true
                    });
                    base.ConfigureServices(services);
                }

                [Fact]
                public void Should_return_a_TableStorageRepository_TModel()
                {
                    var result = FactoryUnderTest.CreateRepository<MyUnboundEntity>();
                    Assert.IsType<TableStorageRepository<MyUnboundEntity>>(result);
                }
            }

            public class When_AutoCreateMissingBindings_is_set_to_false : CreateRepository
            {
                protected override void ConfigureServices(IServiceCollection services)
                {
                    services.AddSingleton(new TableStorageRepositoryFactorySettings
                    {
                        AutoCreateMissingBindings = false
                    });
                    base.ConfigureServices(services);
                }

                [Fact]
                public void Should_return_null()
                {
                    var result = FactoryUnderTest.CreateRepository<MyUnboundEntity>();
                    Assert.Null(result);
                }
            }
        }

        public class CreateReader : TableStorageRepositoryFactoryTest
        {
            protected override void ConfigureServices(IServiceCollection services)
            {
                base.ConfigureServices(services);
                services.AddSingleton<IFilterableTableStorageReader<MyEntity>, FilterableTableStorageReader<MyEntity>>();
            }

            [Fact]
            public void Should_return_the_expected_implementation()
            {
                var result = FactoryUnderTest.CreateReader<MyEntity>();
                Assert.IsType<FilterableTableStorageReader<MyEntity>>(result);
            }

            public class When_AutoCreateMissingBindings_is_set_to_true : CreateRepository
            {
                protected override void ConfigureServices(IServiceCollection services)
                {
                    services.AddSingleton(new TableStorageRepositoryFactorySettings
                    {
                        AutoCreateMissingBindings = true
                    });
                    base.ConfigureServices(services);
                }

                [Fact]
                public void Should_return_a_TableStorageRepository_TModel()
                {
                    var result = FactoryUnderTest.CreateReader<MyUnboundEntity>();
                    Assert.IsType<FilterableTableStorageReader<MyUnboundEntity>>(result);
                }
            }

            public class When_AutoCreateMissingBindings_is_set_to_false : CreateRepository
            {
                protected override void ConfigureServices(IServiceCollection services)
                {
                    services.AddSingleton(new TableStorageRepositoryFactorySettings
                    {
                        AutoCreateMissingBindings = false
                    });
                    base.ConfigureServices(services);
                }

                [Fact]
                public void Should_return_null()
                {
                    var result = FactoryUnderTest.CreateReader<MyUnboundEntity>();
                    Assert.Null(result);
                }
            }
        }

        private class MyEntity : TableEntity
        {
        }

        private class MyUnboundEntity : TableEntity
        {
        }
    }
}
