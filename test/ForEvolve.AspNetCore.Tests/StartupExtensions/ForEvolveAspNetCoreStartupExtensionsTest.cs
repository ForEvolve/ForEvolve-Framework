using ForEvolve.AspNetCore;
using ForEvolve.AspNetCore.Emails;
using ForEvolve.AspNetCore.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ForEvolveAspNetCoreStartupExtensionsTest
    {
        public class AddForEvolveAspNetCore : BaseStartupExtensionsTest
        {
            public readonly IEnumerable<Type> ExpectedSingletonServices;
            public readonly IEnumerable<Type> ExpectedScopedServices;
            public AddForEvolveAspNetCore()
            {
                ExpectedSingletonServices =
                    ErrorFactoryStartupExtensionsTest.AddForEvolveErrorFactory.ExpectedSingletonServices
                    .Concat(OperationResultsStartupExtensionsTest.AddForEvolveOperationResults.ExpectedSingletonServices)
                    .Concat(new Type[]
                    {
                        typeof(ForEvolveAspNetCoreSettings),
                        typeof(IHttpContextAccessor),
                        typeof(IHttpRequestValueFinder),
                        typeof(IEmailSender),
                        typeof(EmailOptions),
                    });
                ExpectedScopedServices = new Type[]
                {
                    typeof(IViewRenderer),
                };
            }

            [Fact]
            public void Should_register_default_services_implementations()
            {
                // Arrange
                IConfiguration nullConfigurationMock = null;

                // Act & Assert
                AssertThatAllServicesAreRegistered(
                    services => AddSingletonMock<IHostingEnvironment>(
                        () => services.AddForEvolveAspNetCore(nullConfigurationMock)
                    ),
                    expectedSingletonServices: ExpectedSingletonServices,
                    expectedScopedServices: ExpectedScopedServices
                );
            }
        }
    }
}
