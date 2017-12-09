using ForEvolve.AspNetCore;
using System;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class OperationResultsStartupExtensionsTest
    {
        public class AddForEvolveOperationResults : BaseStartupExtensionsTest
        {
            public static readonly Type[] ExpectedSingletonServices = new Type[]
            {
                typeof(IOperationResultFactory),
            };

            [Fact]
            public void Should_register_default_services_implementations()
            {
                // Arange, Act & Assert
                AssertThatAllServicesAreRegistered(
                    services => services.AddForEvolveOperationResults(),
                    expectedSingletonServices: ExpectedSingletonServices
                );
            }
        }
    }
}
