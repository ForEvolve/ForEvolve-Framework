using ForEvolve.Pdf.Abstractions;
using ForEvolve.Pdf.PhantomJs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class PhantomJsStartupExtensionsTest
    {
        public class Should_bind : PhantomJsStartupExtensionsTest
        {
            private readonly ServiceCollection _services;

            public Should_bind()
            {
                // Arrange
                _services = new ServiceCollection();

                // Act
                _services.AddForEvolvePhantomJsHtmlToPdfConverter();
            }

            [Fact]
            public void HtmlToPdfConverter_to_IHtmlToPdfConverter()
            {
                // Assert
                _services.AssertSingletonServiceExists<IHtmlToPdfConverter, HtmlToPdfConverter>();
            }

            [Fact]
            public void OperatingSystemFinder_to_IOperatingSystemFinder()
            {
                // Assert
                _services.AssertSingletonServiceExists<IOperatingSystemFinder, OperatingSystemFinder>();
            }

            [Fact]
            public void HtmlToPdfConverterOptionsJsonSerializer_to_IHtmlToPdfConverterOptionsSerializer()
            {
                // Assert
                _services.AssertSingletonServiceExists<IHtmlToPdfConverterOptionsSerializer, HtmlToPdfConverterOptionsJsonSerializer>();
            }

            [Fact]
            public void ExecutableNameFinder_to_IExecutableNameFinder()
            {
                // Assert
                _services.AssertSingletonServiceExists<IExecutableNameFinder, ExecutableNameFinder>();
            }

        }

        [Fact]
        public void Should_execute_configuration_action()
        {
            // Arrange
            var services = new ServiceCollection();
            var executedAction = false;

            // Act
            services.AddForEvolvePhantomJsHtmlToPdfConverter((options) => executedAction = true);

            // Assert
            Assert.True(executedAction, "optionsAction should be invoked by AddPhantomJsHtmlToPdfConverter()");
            services.AssertSingletonServiceExists<HtmlToPdfConverterOptions>();
        }
    }
}
