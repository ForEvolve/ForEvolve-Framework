using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Sdk;

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

            var missingServices = expectedServices
                .Except(registeredServiceType)
                .Select(x => x.Name);
            var unexpectedServices = registeredServiceType
                .Except(expectedServices)
                .Select(x => x.Name);

            var missingServicesCount = missingServices.Count();
            var unexpectedServicesCount = unexpectedServices.Count();

            if(missingServicesCount > 0 || unexpectedServicesCount > 0)
            {
                var message = "Invalid services.";
                if (missingServicesCount > 0)
                {
                    var missingServicesName = string.Join(", ", missingServices);
                    message += $"\r\nThe following {missingServicesCount} service(s) are missing: {missingServicesName}";
                }
                if (unexpectedServicesCount > 0)
                {
                    var unexpectedServicesName = string.Join(", ", unexpectedServices);
                    message += $"\r\nThe following {unexpectedServicesCount} service(s) were not expected: {unexpectedServicesName}";
                }
                throw new XunitException(message);
            }
        }
    }
}
