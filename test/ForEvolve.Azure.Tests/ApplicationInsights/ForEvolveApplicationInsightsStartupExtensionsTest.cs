using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ForEvolve.Azure.ApplicationInsights
{
    public class ForEvolveApplicationInsightsStartupExtensionsTest
    {
        //private readonly 
        public ForEvolveApplicationInsightsStartupExtensionsTest()
        {
        }

        public class AddForEvolveApplicationInsights : ForEvolveApplicationInsightsStartupExtensionsTest
        {
            [Fact]
            public void Should_add_types_to_the_ServiceCollection_required_to_instanciate_TrackExceptionsFilterAttribute()
            {
                // Arrange
                var serviceProvider = new ServiceCollection()
                    .AddSingleton(new TelemetryClient()) // Simulate ApplicationInsights
                    .AddForEvolveApplicationInsights()
                    .BuildServiceProvider();

                // Act
                var result = serviceProvider.GetService<TrackExceptionsFilterAttribute>();

                // Assert
                Assert.NotNull(result);
            }
        }

        public class ConfigureForEvolveApplicationInsights : ForEvolveApplicationInsightsStartupExtensionsTest
        {
            //MvcOptions
            [Fact]
            public void Should_add_TrackExceptionsFilterAttribute_to_Mvc_filters()
            {
                // Arrange
                var options = new MvcOptions();
                var expectedTypeName = typeof(TrackExceptionsFilterAttribute).Name;

                // Act
                options.ConfigureForEvolveApplicationInsights();

                // Assert
                var filter = options.Filters.FirstOrDefault();
                Assert.Equal(1, options.Filters.Count);
                Assert.NotNull(filter);
                var typeFilterAttribute = Assert.IsType<TypeFilterAttribute>(filter);
                Assert.Equal(expectedTypeName, typeFilterAttribute.ImplementationType.Name);
            }

        }
    }
}
