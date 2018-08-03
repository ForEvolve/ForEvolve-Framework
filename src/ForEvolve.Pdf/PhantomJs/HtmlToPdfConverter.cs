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
        private readonly IHtmlToPdfConverterOptionsSerializer _optionsSerializer;

        public HtmlToPdfConverter(HtmlToPdfConverterOptions options, IExecutableNameFinder executableNameFinder, IHtmlToPdfConverterOptionsSerializer optionsSerializer)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _executableNameFinder = executableNameFinder ?? throw new ArgumentNullException(nameof(executableNameFinder));
            _optionsSerializer = optionsSerializer ?? throw new ArgumentNullException(nameof(optionsSerializer));
        }

        public string Convert(string html, string outputDirectory)
        {
            var htmlFileName = WriteHtmlToTempFile(html);
            try
            {
                if (!Directory.Exists(outputDirectory))
                {
                    var fullPath = Path.GetFullPath(outputDirectory);
                    var message = string.Concat(
                        PhantomJsConstants.OutputDirectoryDoesNotExist,
                        Environment.NewLine,
                        fullPath
                    );
                    throw new ArgumentException(message, nameof(outputDirectory));
                }
                return ExecutePhantomJs(htmlFileName, outputDirectory);
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

        private string ExecutePhantomJs(string inputFileName, string outputDirectory)
        {
            var phantomJsExeName = _executableNameFinder.Find();
            var outputFilePath = Path.Combine(outputDirectory, $"{inputFileName}.pdf");
            var phantomJsAbsolutePath = Path.Combine(_options.PhantomRootDirectory, phantomJsExeName);
            var serializedOptions = _optionsSerializer.Serialize(_options);

            if (!File.Exists(phantomJsAbsolutePath))
            {
                throw new FileNotFoundException(PhantomJsConstants.PhantomJSExeNotFound, phantomJsExeName);
            }

            using (var proc = new Process()
            {
                StartInfo = new ProcessStartInfo(phantomJsAbsolutePath)
                {
                    WorkingDirectory = _options.PhantomRootDirectory,
                    Arguments = $"rasterize.js \"{inputFileName}\" \"{outputFilePath}\" \"{serializedOptions}\"",
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
