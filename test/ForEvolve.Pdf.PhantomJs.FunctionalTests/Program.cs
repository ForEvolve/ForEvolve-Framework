﻿using ForEvolve.Pdf.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

namespace ForEvolve.Pdf.PhantomJs.FunctionalTests
{
    public class Program
    {
        private static readonly string _targetDirectory;
        private static IHtmlToPdfConverter HtmlToPdfConverter { get; }

        static Program()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            _targetDirectory = Path.Combine(currentDirectory, "PhantomJs", "pdf-out");
            if (!Directory.Exists(_targetDirectory))
            {
                Directory.CreateDirectory(_targetDirectory);
            }
            HtmlToPdfConverter = CreateHtmlToPdfConverter();
        }

        public static void Main(string[] args)
        {
            RunTestCases();
            Console.ReadLine();
        }

        public static IEnumerable<TestCaseResult> RunTestCases()
        {
            yield return Run(() => BasicHtmlTest());
            yield return Run(() => WithInlineStyles());
            yield return Run(() => WithRelativeStyleSheet());
        }

        private static TestCaseResult Run(Func<string> testCase)
        {
            var result = new TestCaseResult();
            try
            {
                result.GeneratedFilePath = testCase();
                result.Suceeded = true;
            }
            catch (Exception ex)
            {
                result.Suceeded = false;
                result.Error = ex.Message;
            }
            return result;
        }

        private static IHtmlToPdfConverter CreateHtmlToPdfConverter()
        {
            var services = new ServiceCollection();
            services.AddPhantomJsHtmlToPdfConverter();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IHtmlToPdfConverter>();
        }

        private static string WithRelativeStyleSheet()
        {
            var htmlToConvert =
@"
<!DOCTYPE html>
<html>
<head>
    <title>complex-test</title>
    <link href=""assets/css/pdf-styles.css"" rel=""stylesheet"" />
</head>
<body>
    <h1>Hello World!</h1>
    <p>This PDF has been generated by PhantomJs ;)</p>
    <p>This PDF loads an external CSS stylesheet.</p>

    <h2>This is a subtitle</h2>
    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla dignissim sit amet erat vehicula semper. In lobortis, velit mattis sagittis accumsan, erat nisl tincidunt dui, varius imperdiet elit lacus non metus. Ut iaculis maximus semper. Interdum et malesuada fames ac ante ipsum primis in faucibus. Ut ac pharetra augue. Fusce vitae felis auctor, rutrum urna vitae, sagittis nibh. Curabitur in aliquet odio, non varius orci. Aenean sit amet rutrum nibh. Aenean erat urna, efficitur id maximus a, dapibus ut sem.</p>
</body>
</html>
";
            return ConvertHtml(htmlToConvert);
        }

        private static string WithInlineStyles()
        {
            var htmlToConvert =
@"
<!DOCTYPE html>
<html>
<head>
    <title>complex-test</title>
    <style>
        body {
            color: #333;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        h1 {
            background: #ff0000;
        }
    </style>
</head>
<body>
    <h1>Hello World!</h1>
    <p>This PDF has been generated by PhantomJs ;)</p>

    <h2>This is a subtitle</h2>
    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla dignissim sit amet erat vehicula semper. In lobortis, velit mattis sagittis accumsan, erat nisl tincidunt dui, varius imperdiet elit lacus non metus. Ut iaculis maximus semper. Interdum et malesuada fames ac ante ipsum primis in faucibus. Ut ac pharetra augue. Fusce vitae felis auctor, rutrum urna vitae, sagittis nibh. Curabitur in aliquet odio, non varius orci. Aenean sit amet rutrum nibh. Aenean erat urna, efficitur id maximus a, dapibus ut sem.</p>
</body>
</html>
";
            return ConvertHtml(htmlToConvert);
        }

        private static string BasicHtmlTest()
        {
            var htmlToConvert =
@"
<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <h1>Hello World!</h1>
    <p>This PDF has been generated by PhantomJs ;)</p>
</body>
</html>
";
            // Generate pdf from html and place in the current folder.
            return ConvertHtml(htmlToConvert);
        }

        private static string ConvertHtml(string htmlToConvert)
        {
            var pathOftheGeneratedPdf = HtmlToPdfConverter.Convert(htmlToConvert, _targetDirectory);
            Console.WriteLine($"Pdf generated at: {pathOftheGeneratedPdf}");
            return pathOftheGeneratedPdf;
        }
    }

    public class TestCaseResult
    {
        public bool Suceeded { get; set; }
        public string GeneratedFilePath { get; set; }
        public string Error { get; set; }
    }
}
