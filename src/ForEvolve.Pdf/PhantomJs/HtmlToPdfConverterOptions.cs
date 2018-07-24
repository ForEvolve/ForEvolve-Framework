using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Pdf.PhantomJs
{
    /// <summary>
    /// PhantomJs PDF generator options.
    /// </summary>
    /// <remarks>See http://phantomjs.org/api/webpage/property/paper-size.html for more information about values and measurements.</remarks>
    public class HtmlToPdfConverterOptions
    {
        /// <summary>
        /// Initializes a new instance of the ForEvolve.Pdf.PhantomJs.PdfGeneratorOptions class.
        /// </summary>
        public HtmlToPdfConverterOptions()
            : this(GetDefaultPhantomRootDirectory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the ForEvolve.Pdf.PhantomJs.PdfGeneratorOptions class using the specified phantomRootDirectory.
        /// </summary>
        /// <param name="phantomRootDirectory">The Phantom JS root path where executables are located.</param>
        public HtmlToPdfConverterOptions(string phantomRootDirectory)
        {
            if (string.IsNullOrWhiteSpace(phantomRootDirectory)) { throw new ArgumentNullException(nameof(phantomRootDirectory)); }
            if (!Directory.Exists(phantomRootDirectory)) { throw new ArgumentException(PhantomJsConstants.DirectoryDoesNotExist, nameof(phantomRootDirectory)); }
            PhantomRootDirectory = phantomRootDirectory;
        }

        /// <summary>
        /// The PhantomJS root directory.
        /// </summary>
        public string PhantomRootDirectory { get; }

        /// <summary>
        /// Gets or sets the paper size. 
        /// You can use PaperSizeFormat or define custom size like 10cm*20cm.
        /// </summary>
        public PaperSize PaperSize { get; set; } = PaperSizeFormat.Letter;

        /// <summary>
        /// Gets or sets the page orientation.
        /// </summary>
        public Orientation Orientation { get; set; } = Orientation.Portrait;

        /// <summary>
        /// Gets or sets the page margins.
        /// </summary>
        public Margins Margins { get; set; } = Margins.Normal;

        private static string GetDefaultPhantomRootDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            return Path.Combine(currentDirectory, "PhantomJs", "Root");
        }
    }
}
