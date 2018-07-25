using ForEvolve.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ForEvolveOperationResultsStartupExtensionsTest
    {
        public class AddForEvolveOperationResults
        {
            public static readonly IEnumerable<Type> ExpectedSingletonServices = ForEvolveErrorFactoryStartupExtensionsTest.AddForEvolveErrorFactory
                .ExpectedSingletonServices.Concat(new Type[]
                {
                    typeof(IOperationResultFactory),
                });

            [Fact]
            public void Should_register_default_services_implementations()
            {
                // Arrange
                var services = new ServiceCollection();
                services
                    .AddSingletonMock<IHostingEnvironment>()

                    // Act
                    .AddForEvolveOperationResults()

                    // Assert
                    .AssertSingletonServicesExist(ExpectedSingletonServices);
            }
        }
    }
}
