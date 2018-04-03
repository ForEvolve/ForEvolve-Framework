using ForEvolve.Markdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class MarkdownStartupExtensionsTest
    {
        [Fact]
        public void Should_register_default_services()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddMarkdown();

            // Assert
            AssertMarkdownConverter(services);
        }

        [Fact]
        public void Should_configure_pipeline_when_an_action_is_provided()
        {
            // Arrange
            var services = new ServiceCollection();
            bool called = false;

            // Act
            services.AddMarkdown(builder => called = true);

            // Assert
            AssertMarkdownConverter(services);
            Assert.True(called);
        }

        private static void AssertMarkdownConverter(ServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var markdownConverter = serviceProvider.GetService<IMarkdownConverter>();
            Assert.IsType<MarkdownConverter>(markdownConverter);
        }
    }
}
