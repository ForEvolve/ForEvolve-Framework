using System.Linq;
using System.Text;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs.FunctionalTests.Runner
{
    public class PhantomJsAppConsoleFunctionalTests : PhantomJsBaseFunctionalTests
    {
        [Fact]
        public void Should_execute_all_test_cases_sucessfully()
        {
            // Act
            var results = AppConsole.FunctionalTests.Program.RunTestCases();

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
