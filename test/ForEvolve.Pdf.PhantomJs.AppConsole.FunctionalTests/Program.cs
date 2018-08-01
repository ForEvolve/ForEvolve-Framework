using ForEvolve.Pdf.Abstractions;
using ForEvolve.Pdf.PhantomJs.AppShared.FunctionalTests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ForEvolve.Pdf.PhantomJs.AppConsole.FunctionalTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var allSucceeded = RunTestCases()
                .All(x => x.Suceeded);
            Console.WriteLine($"All succeeded: {allSucceeded}");
            Console.ReadLine();
        }

        public static IEnumerable<TestCaseResult> RunTestCases()
        {
            var htmlToPdfConverter = CreateHtmlToPdfConverter();
            var currentDirectory = Directory.GetCurrentDirectory();
            var targetDirectory = Path.Combine(currentDirectory, "PhantomJs", "pdf-out");
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var testRunner = new TestCaseRunner();
            return testRunner.RunAll(htmlToPdfConverter, targetDirectory);
        }

        private static IHtmlToPdfConverter CreateHtmlToPdfConverter()
        {
            var services = new ServiceCollection();
            services.AddForEvolvePhantomJsHtmlToPdfConverter();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IHtmlToPdfConverter>();
        }
    }
}
