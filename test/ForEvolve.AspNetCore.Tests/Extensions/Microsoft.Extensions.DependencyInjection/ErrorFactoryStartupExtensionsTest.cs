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
        public class AddForEvolveErrorFactory
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
                typeof(IErrorFromOperationResultFactory),
                typeof(IErrorFromSerializableErrorFactory),
                typeof(IErrorFactory),
                // Prerequisite
                typeof(IHostingEnvironment),
            };

            [Fact]
            public void Should_register_default_services_implementations()
            {
                // Arrange
                var services = new ServiceCollection();
                services
                    .AddSingletonMock<IHostingEnvironment>()
                    
                    // Act
                    .AddForEvolveErrorFactory()
                    
                    // Assert
                    .AssertSingletonServicesExist(ExpectedSingletonServices);
            }
        }
    }
}
