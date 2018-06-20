using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RemoveFilter<TFilter>(this IServiceCollection services) 
            where TFilter : IFilterMetadata
        {
            return services.PostConfigure<MvcOptions>(options =>
            {
                var filters = options.Filters.Where(x => x.GetType() == typeof(RequireHttpsAttribute)).ToArray();
                if(filters != null && filters.Count() > 0)
                {
                    foreach (var filter in filters)
                    {
                        options.Filters.Remove(filter);
                    }
                }
            });
        }

        public static IServiceCollection ByPassPolicies(this IServiceCollection services, IEnumerable<string> policiesName)
        {
            var authorizedPolicy = CreateAuthorizedPolicy();
            return services.PostConfigure<AuthorizationOptions>(options =>
            {
                foreach (var policyName in policiesName)
                {
                    options.AddPolicy(policyName, authorizedPolicy);
                }
                options.DefaultPolicy = authorizedPolicy;
            });
        }

        public static IServiceCollection ByPassDefaultPolicy(this IServiceCollection services)
        {
            var authorizedPolicy = CreateAuthorizedPolicy();
            return services.PostConfigure<AuthorizationOptions>(options =>
            {
                options.DefaultPolicy = authorizedPolicy;
            });
        }

        public static IServiceCollection ReplacePolicy(this IServiceCollection services, string policyName, AuthorizationPolicy policy)
        {
            return services.PostConfigure<AuthorizationOptions>(options =>
            {
                options.AddPolicy(policyName, policy);
            });
        }

        private static AuthorizationPolicy CreateAuthorizedPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAssertion(x => true)
                .Build();
        }

        public static IServiceCollection AssertThatAllServicesAreRegistered(
            this IServiceCollection services,

            IEnumerable<Type> expectedSingletonServices = null,
            IEnumerable<Type> expectedScopedServices = null,
            IEnumerable<Type> expectedTransientServices = null
        )
        {
            var hasSingletonServices = expectedSingletonServices != null;
            var hasScopedServices = expectedScopedServices != null;
            var hasTransientServices = expectedTransientServices != null;

            // Validate empty collections
            var hasNoService = !hasSingletonServices && !hasScopedServices && !hasTransientServices;
            if (hasNoService)
            {
                Assert.Empty(services);
                return services;
            }

            // Validate services by scope
            if (hasSingletonServices)
            {
                services.AssertSingletonServices(expectedSingletonServices);
            }
            if (hasScopedServices)
            {
                services.AssertScopedServices(expectedScopedServices);
            }
            if (hasTransientServices)
            {
                services.AssertTransientServices(expectedTransientServices);
            }
            return services;
        }

        public static IServiceCollection AssertSingletonServices(this IServiceCollection services, IEnumerable<Type> expectedServices)
        {
            return services.AssertThatServicesAreRegisteredInScope(
                expectedServices,
                ServiceLifetime.Singleton
            );
        }

        public static IServiceCollection AssertScopedServices(this IServiceCollection services, IEnumerable<Type> expectedServices)
        {
            return services.AssertThatServicesAreRegisteredInScope(
                expectedServices,
                ServiceLifetime.Scoped
            );
        }

        public static IServiceCollection AssertTransientServices(this IServiceCollection services, IEnumerable<Type> expectedServices)
        {
            return services.AssertThatServicesAreRegisteredInScope(
                expectedServices,
                ServiceLifetime.Transient
            );
        }

        public static IServiceCollection AssertThatServicesAreRegisteredInScope(
            this IServiceCollection services,
            IEnumerable<Type> expectedServices, 
            ServiceLifetime lifetime
        )
        {
            var registeredServiceType = services
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

            if (missingServicesCount > 0 || unexpectedServicesCount > 0)
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
            return services;
        }
    }
}
