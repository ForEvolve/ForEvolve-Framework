﻿using ForEvolve.Pdf.Abstractions;
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
                _services.AddPhantomJsHtmlToPdfConverter();
            }

            [Fact]
            public void HtmlToPdfConverter_to_IHtmlToPdfConverter()
            {
                // Assert
                _services.AssertSingletonServiceImplementationExists<IHtmlToPdfConverter, HtmlToPdfConverter>();
            }

            [Fact]
            public void OperatingSystemFinder_to_IOperatingSystemFinder()
            {
                // Assert
                _services.AssertSingletonServiceImplementationExists<IOperatingSystemFinder, OperatingSystemFinder>();
            }

            [Fact]
            public void HtmlToPdfConverterOptionsJsonSerializer_to_IHtmlToPdfConverterOptionsSerializer()
            {
                // Assert
                _services.AssertSingletonServiceImplementationExists<IHtmlToPdfConverterOptionsSerializer, HtmlToPdfConverterOptionsJsonSerializer>();
            }
        }

        [Fact]
        public void Should_execute_configuration_action()
        {
            // Arrange
            var services = new ServiceCollection();
            var executedAction = false;

            // Act
            services.AddPhantomJsHtmlToPdfConverter((options) => executedAction = true);

            // Assert
            Assert.True(executedAction, "optionsAction should be invoked by AddPhantomJsHtmlToPdfConverter()");
            services.AssertSingletonServiceExists<HtmlToPdfConverterOptions>();
        }
    }
}
