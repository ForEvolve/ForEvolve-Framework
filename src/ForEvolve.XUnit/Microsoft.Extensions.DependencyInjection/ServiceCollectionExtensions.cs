using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
