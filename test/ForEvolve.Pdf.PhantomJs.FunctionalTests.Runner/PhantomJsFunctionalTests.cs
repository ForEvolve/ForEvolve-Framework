using System;
using System.Collections.Generic;
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
            Assert.Collection(results,
                result => Assert.True(result.Suceeded),
                result => Assert.True(result.Suceeded),
                result => Assert.True(result.Suceeded)
            );
        }

    }
}
