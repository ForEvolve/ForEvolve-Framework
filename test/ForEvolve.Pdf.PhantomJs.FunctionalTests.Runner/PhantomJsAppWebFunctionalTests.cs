using ForEvolve.Pdf.PhantomJs.AppShared.FunctionalTests;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs.FunctionalTests.Runner
{
    public class PhantomJsAppWebFunctionalTests : PhantomJsBaseFunctionalTests, IClassFixture<WebApplicationFactory<AppWeb.FunctionalTests.Startup>>
    {
        private readonly WebApplicationFactory<AppWeb.FunctionalTests.Startup> _appFactory;
        public PhantomJsAppWebFunctionalTests(WebApplicationFactory<AppWeb.FunctionalTests.Startup> appFactory)
        {
            _appFactory = appFactory ?? throw new ArgumentNullException(nameof(appFactory));
        }

        [Fact]
        public async Task Should_execute_all_test_cases_sucessfully()
        {
            // Arange & Act
            var client = _appFactory.CreateClient();
            var httpResult = await client.GetAsync("/");
            var body = await httpResult.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<TestCaseResult[]>(body);

            // Assert
            CleanupGeneratedFiles(results);
            Assert.Collection(results,
                result => Assert.True(result.Suceeded),
                result => Assert.True(result.Suceeded),
                result => Assert.True(result.Suceeded)
            );
        }
    }
}
