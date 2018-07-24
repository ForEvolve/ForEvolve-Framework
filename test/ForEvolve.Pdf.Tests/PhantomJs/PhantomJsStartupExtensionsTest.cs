﻿using ForEvolve.Pdf.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs
{
    public class PhantomJsStartupExtensionsTest
    {
        [Fact]
        public void Should_bind_HtmlToPdfConverter_to_IHtmlToPdfConverter()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddPhantomJsHtmlToPdfConverter();

            // Assert
            services.AssertSingletonServiceImplementationExists<IHtmlToPdfConverter, HtmlToPdfConverter>();
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