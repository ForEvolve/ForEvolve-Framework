using ForEvolve.Pdf.Abstractions;
using ForEvolve.Pdf.PhantomJs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Pdf.Tests.PhantomJs
{
    public class PhantomJsHtmlToPdfConverter : IHtmlToPdfConverter
    {
        private readonly OS _platform;
        private readonly HtmlToPdfConverterOptions _options;

        public PhantomJsHtmlToPdfConverter(HtmlToPdfConverterOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _platform = GetOsPlatform();
        }

        /// <summary>
        /// Render the specified HTML code to a PDF.
        /// Save that PDF to the specified directory.
        /// </summary>
        /// <param name="html">The HTML to convert to PDF.</param>
        /// <param name="outputFolder">The directory to save the PDF to.</param>
        /// <returns>The file name of the generated PDF.</returns>
        public string Convert(string html, string outputFolder)
        {
            var htmlFileName = WriteHtmlToTemporaryFile(html);
            try
            {
                if (!Directory.Exists(outputFolder))
                {
                    throw new ArgumentException("The output folder is not a valid directory!");
                }

                var phantomExeToUse =
                    (_platform == OS.LINUX) ? "linux64_phantomjs.exe" :
                    (_platform == OS.WINDOWS) ? "windows_phantomjs.exe" :
                    "osx_phantomjs.exe";

                return ExecutePhantomJs(phantomExeToUse, htmlFileName, outputFolder);
            }
            finally
            {
                File.Delete(Path.Combine(_options.PhantomRootDirectory, htmlFileName));
            }
        }

        private OS GetOsPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OS.WINDOWS;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OS.LINUX;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OS.OSX;
            }

            throw new Exception("PdfGenerator: OS Environment could not be probed, halting!");
        }

        private string ExecutePhantomJs(string phantomJsExeToUse, string inputFileName, string outputFolder)
        {
            // The output file must be located in the output folder.
            var outputFilePath = Path.Combine(outputFolder, $"{inputFileName}.pdf");

            var phantomJsAbsolutePath = Path.Combine(_options.PhantomRootDirectory, phantomJsExeToUse);
            var startInfo = new ProcessStartInfo(phantomJsAbsolutePath)
            {
                WorkingDirectory = _options.PhantomRootDirectory,
                Arguments = $"rasterize.js \"{inputFileName}\" \"{outputFilePath}\" \"{_options.PaperSize}\"",
                UseShellExecute = false
            };

            using (var proc = new Process() { StartInfo = startInfo })
            {
                proc.Start();
                proc.WaitForExit();
                return outputFilePath;
            }
        }

        private string WriteHtmlToTemporaryFile(string html)
        {
            var filename = Path.GetRandomFileName() + ".html";
            var absolutePath = Path.Combine(_options.PhantomRootDirectory, filename);
            File.WriteAllText(absolutePath, html);
            return filename;
        }

        private enum OS
        {
            LINUX,
            WINDOWS,
            OSX
        }
    }
}
