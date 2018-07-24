using ForEvolve.Pdf.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Pdf.PhantomJs
{
    public class HtmlToPdfConverter : IHtmlToPdfConverter
    {
        private readonly HtmlToPdfConverterOptions _options;
        private readonly IExecutableNameFinder _executableNameFinder;

        public HtmlToPdfConverter(HtmlToPdfConverterOptions options, IExecutableNameFinder executableNameFinder)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _executableNameFinder = executableNameFinder ?? throw new ArgumentNullException(nameof(executableNameFinder));
        }

        public string Convert(string html, string outputFolder)
        {
            var htmlFileName = WriteHtmlToTempFile(html);
            try
            {
                if (!Directory.Exists(outputFolder))
                {
                    throw new ArgumentException("The output folder is not a valid directory!");
                }
                return ExecutePhantomJs(htmlFileName, outputFolder);
            }
            finally
            {
                File.Delete(Path.Combine(_options.PhantomRootDirectory, htmlFileName));
            }
        }

        private string WriteHtmlToTempFile(string html)
        {
            var filename = Path.GetRandomFileName() + ".html";
            var absolutePath = Path.Combine(_options.PhantomRootDirectory, filename);
            File.WriteAllText(absolutePath, html);
            return filename;
        }

        private string ExecutePhantomJs(string inputFileName, string outputFolder)
        {
            var phantomJsExeName = _executableNameFinder.Find();
            var outputFilePath = Path.Combine(outputFolder, $"{inputFileName}.pdf");
            var phantomJsAbsolutePath = Path.Combine(_options.PhantomRootDirectory, phantomJsExeName);

            using (var proc = new Process()
            {
                StartInfo = new ProcessStartInfo(phantomJsAbsolutePath)
                {
                    WorkingDirectory = _options.PhantomRootDirectory,
                    Arguments = $"rasterize.js \"{inputFileName}\" \"{outputFilePath}\" \"{_options.PaperSize}\"",
                    UseShellExecute = false
                }
            })
            {
                proc.Start();
                proc.WaitForExit();
                return outputFilePath;
            }
        }
    }
}
