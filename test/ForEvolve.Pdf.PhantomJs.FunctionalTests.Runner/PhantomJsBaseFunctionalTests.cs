using ForEvolve.Pdf.PhantomJs.AppShared.FunctionalTests;
using System.Collections.Generic;
using System.IO;

namespace ForEvolve.Pdf.PhantomJs.FunctionalTests.Runner
{
    public abstract class PhantomJsBaseFunctionalTests
    {
        protected void CleanupGeneratedFiles(IEnumerable<TestCaseResult> results)
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
