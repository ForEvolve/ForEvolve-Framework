using ForEvolve.AspNetCore;
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
        public class AddForEvolveAspNetCore
        {
            public readonly IEnumerable<Type> ExpectedSingletonServices;
            public readonly IEnumerable<Type> ExpectedScopedServices;
            public AddForEvolveAspNetCore()
            {
                ExpectedSingletonServices = new Type[]
                {
                    typeof(ForEvolveAspNetCoreSettings),
                    typeof(IHttpContextAccessor),
                    typeof(IHttpHeaderValueAccessor),
                    typeof(IEmailSenderService),
                    typeof(EmailOptions),
                    typeof(IHtmlToPlainTextEmailBodyConverter),
                    typeof(IHostingEnvironment),
                };
                ExpectedScopedServices = new Type[]
                {
                    typeof(IViewRendererService),
                };
            }

            [Fact]
            public void Should_register_default_services_implementations()
            {
                // Arrange
                var services = new ServiceCollection();
                services
                    .AddSingletonMock<IHostingEnvironment>()

                    // Act
                    .AddForEvolveAspNetCore(default(IConfiguration))

                    // Assert
                    .AssertScopedServicesExist(ExpectedScopedServices)
                    .AssertSingletonServicesExist(ExpectedSingletonServices)
                    ;
            }
        }
    }
}
