using System;

namespace ForEvolve.Pdf.Abstractions
{
    /// <summary>
    /// Convert HTML to PDF.
    /// </summary>
    public interface IHtmlToPdfConverter
    {
        /// <summary>
        /// Render the specified HTML code to a PDF.
        /// Save that PDF to the specified directory.
        /// </summary>
        /// <param name="html">The HTML to convert to PDF.</param>
        /// <param name="outputDirectory">The directory to save the PDF to.</param>
        /// <returns>The full file path of the generated PDF.</returns>
        string Convert(string html, string outputDirectory);
    }
}
