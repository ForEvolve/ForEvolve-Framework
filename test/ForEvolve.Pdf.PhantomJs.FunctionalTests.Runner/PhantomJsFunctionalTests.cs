using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Pdf.PhantomJs.FunctionalTests.Runner
{
    public class PhantomJsFunctionalTests
    {
        [Fact]
        public void Should_execute_all_test_cases_sucessfully()
        {
            // Act
            var results = Program.RunTestCases();

            // Assert
            CleanupGeneratedFiles(results);
            Assert.Collection(results,
                result => Assert.True(result.Suceeded),
                result => Assert.True(result.Suceeded),
                result => Assert.True(result.Suceeded)
            );
        }

        private static void CleanupGeneratedFiles(IEnumerable<TestCaseResult> results)
        {
            foreach (var item in results)
            {
                if (!string.IsNullOrWhiteSpace(item.GeneratedFilePath))
                {
                    if (File.Exists(item.GeneratedFilePath))
                    {
                        File.Delete(item.GeneratedFilePath);
                    }
                }
            }
        }
    }
}
