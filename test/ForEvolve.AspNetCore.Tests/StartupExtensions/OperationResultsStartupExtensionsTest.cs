using ForEvolve.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class OperationResultsStartupExtensionsTest
    {
        public class AddForEvolveOperationResults : BaseStartupExtensionsTest
        {
            public static readonly IEnumerable<Type> ExpectedSingletonServices = ErrorFactoryStartupExtensionsTest.AddForEvolveErrorFactory
                .ExpectedSingletonServices.Concat(new Type[]
                {
                    typeof(IOperationResultFactory),
                });

            [Fact]
            public void Should_register_default_services_implementations()
            {
                // Arange, Act & Assert
                AssertThatAllServicesAreRegistered(
                    services => services
                        .AddSingletonMock<IHostingEnvironment>()
                        .AddForEvolveOperationResults(),
                    expectedSingletonServices: ExpectedSingletonServices
                );
            }
        }
    }
}
