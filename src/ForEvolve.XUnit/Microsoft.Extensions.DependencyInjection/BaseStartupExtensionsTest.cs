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
        private readonly IServiceCollection _services;

        public BaseStartupExtensionsTest()
        {
            _services = new ServiceCollection();
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
            act(_services);

            // Assert
            var hasSingletonServices = expectedSingletonServices != null;
            var hasScopedServices = expectedScopedServices != null;
            var hasTransientServices = expectedTransientServices != null;
            var hasNoService = !hasSingletonServices && !hasScopedServices && !hasTransientServices;
            if (hasNoService)
            {
                Assert.Empty(_services);
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
            var registeredServiceType = _services
                .Where(x => x.Lifetime == lifetime)
                .Select(x => x.ServiceType);
            Assert.Equal(expectedServices.Count(), registeredServiceType.Count());
            foreach (var expectedService in expectedServices)
            {
                var service = registeredServiceType
                    .FirstOrDefault(x => x.FullName == expectedService.FullName);
                Assert.Equal(expectedService, service);
            }
        }
    }
}
