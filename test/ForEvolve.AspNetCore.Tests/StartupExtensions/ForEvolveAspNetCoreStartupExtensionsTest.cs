using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ForEvolve.AspNetCore.StartupExtensions
{
    public class ForEvolveAspNetCoreStartupExtensionsTest
    {
        //BaseStartupExtensionsTest
        //ErrorFactoryStartupExtensionsTest.AddErrorFactory.ExpectedSingletonServices
        public class AddForEvolveAspNetCore : BaseStartupExtensionsTest
        {
            public readonly IEnumerable<Type> ExpectedSingletonServices;
            public AddForEvolveAspNetCore()
            {
                ExpectedSingletonServices =
                    ErrorFactoryStartupExtensionsTest.AddErrorFactory.ExpectedSingletonServices
                    .Concat(new Type[]
                    {
                        typeof(IHttpContextAccessor),
                        typeof(IHttpRequestValueFinder),
                    });
            }

            [Fact]
            public void Should_register_default_services_implementations()
            {
                // Arange, Act & Assert
                AssertThatAllServicesAreRegistered(
                    services => services.AddForEvolveAspNetCore(),
                    expectedSingletonServices: ExpectedSingletonServices
                );
            }
        }
    }
}
