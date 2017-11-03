using ForEvolve.AspNetCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ErrorFactoryStartupExtensionsTest
    {
        public class AddErrorFactory : BaseStartupExtensionsTest
        {
            public static readonly Type[] ExpectedSingletonServices = new Type[]
            {
                    typeof(IErrorFromExceptionFactory),
                    typeof(IErrorFromDictionaryFactory),
                    typeof(IErrorFromKeyValuePairFactory),
                    typeof(IErrorFromRawValuesFactory),
                    typeof(IErrorFactory),
            };

            [Fact]
            public void Should_register_default_services_implementations()
            {
                // Arange, Act & Assert
                AssertThatAllServicesAreRegistered(
                    services => services.AddErrorFactory(),
                    expectedSingletonServices: ExpectedSingletonServices
                );
            }
        }
    }
}
