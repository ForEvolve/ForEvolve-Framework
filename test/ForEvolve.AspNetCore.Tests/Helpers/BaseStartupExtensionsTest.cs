using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public abstract class BaseStartupExtensionsTest
    {
        private readonly Mock<IServiceCollection> _servicesMock;
        private readonly List<ServiceDescriptor> _registeredDescriptors;

        public BaseStartupExtensionsTest()
        {
            _registeredDescriptors = new List<ServiceDescriptor>();
            _servicesMock = new Mock<IServiceCollection>();
            _servicesMock
                .Setup(x => x.Add(It.IsAny<ServiceDescriptor>()))
                .Callback((ServiceDescriptor d) => _registeredDescriptors.Add(d))
                .Verifiable();

            // Mock used by TryAdd methods
            _servicesMock
                .Setup(x => x.GetEnumerator())
                .Returns(() => new ReadOnlyCollection<ServiceDescriptor>(_registeredDescriptors).GetEnumerator());
        }

        protected void AssertThatAllServicesAreRegistered(
            Action<IServiceCollection> act,
            
            // Arrange
            IEnumerable<Type> expectedSingletonServices = null,
            IEnumerable<Type> expectedScopedServices = null,
            IEnumerable<Type> expectedTransientServices = null
        )
        {
            // Act
            act(_servicesMock.Object);

            // Assert
            var hasSingletonServices = expectedSingletonServices != null;
            var hasScopedServices = expectedScopedServices != null;
            var hasTransientServices = expectedTransientServices != null;
            var hasNoService = !hasSingletonServices && !hasScopedServices && !hasTransientServices;
            if (hasNoService)
            {
                Assert.Empty(_registeredDescriptors);
            }
            else
            {
                if (hasSingletonServices)
                {
                    AssertServicesAreRegisteredInScope(
                        expectedSingletonServices, 
                        ServiceLifetime.Singleton
                    );
                }
                if (hasScopedServices)
                {
                    AssertServicesAreRegisteredInScope(
                        expectedScopedServices,
                        ServiceLifetime.Scoped
                    );
                }
                if (hasTransientServices)
                {
                    AssertServicesAreRegisteredInScope(
                        expectedTransientServices,
                        ServiceLifetime.Transient
                    );
                }
            }
        }

        private void AssertServicesAreRegisteredInScope(IEnumerable<Type> expectedServices, ServiceLifetime lifetime)
        {
            var registeredServiceType = _registeredDescriptors
                .Where(x => x.Lifetime == lifetime)
                .Select(x => x.ServiceType);
            Assert.Equal(expectedServices, registeredServiceType);
        }
    }
}
