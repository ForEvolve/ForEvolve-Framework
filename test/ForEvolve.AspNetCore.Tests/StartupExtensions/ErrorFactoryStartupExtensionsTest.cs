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
        private readonly Mock<IServiceCollection> _servicesMock;
        private readonly List<ServiceDescriptor> _registeredDescriptors;

        public ErrorFactoryStartupExtensionsTest()
        {
            _registeredDescriptors = new List<ServiceDescriptor>();
            _servicesMock = new Mock<IServiceCollection>();
            _servicesMock
                .Setup(x => x.Add(It.IsAny<ServiceDescriptor>()))
                .Callback((ServiceDescriptor d) => _registeredDescriptors.Add(d))
                .Verifiable();
        }

        public class AddErrorFactory : ErrorFactoryStartupExtensionsTest
        {
            [Fact]
            public void Should_register_default_services_implementations_as_Singleton()
            {
                // Arrange
                var expectedServices = new Type[]
                {
                    typeof(IErrorFromExceptionFactory),
                    typeof(IErrorFromDictionaryFactory),
                    typeof(IErrorFromKeyValuePairFactory),
                    typeof(IErrorFromRawValuesFactory),
                    typeof(IErrorFactory),
                };

                // Act
                _servicesMock.Object.AddErrorFactory();

                // Assert
                var registeredServiceType = _registeredDescriptors
                    .Where(x => x.Lifetime == ServiceLifetime.Singleton)
                    .Select(x => x.ServiceType);
                Assert.Equal(expectedServices, registeredServiceType);
            }
        }
    }
}
