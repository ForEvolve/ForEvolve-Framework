using ForEvolve.AspNetCore;
using Microsoft.AspNetCore.Hosting;
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
        public class AddForEvolveErrorFactory : BaseStartupExtensionsTest
        {
            public static readonly Type[] ExpectedSingletonServices = new Type[]
            {
                typeof(IErrorFromIdentityErrorFactory),
                typeof(IErrorFromExceptionFactory),
                typeof(IErrorFromDictionaryFactory),
                typeof(IErrorFromKeyValuePairFactory),
                typeof(IErrorFromRawValuesFactory),
                typeof(IErrorFromKeyValuePairFactory),
                typeof(IErrorFromRawValuesFactory),
                typeof(IErrorFactory),
                // Prerequisite
                typeof(IHostingEnvironment),
            };

            [Fact]
            public void Should_register_default_services_implementations()
            {
                // Arange, Act & Assert
                AssertThatAllServicesAreRegistered(
                    services => AddSingletonMock<IHostingEnvironment>(
                        () => services.AddForEvolveErrorFactory()
                    ),
                    expectedSingletonServices: ExpectedSingletonServices
                );
            }
        }
    }
}
