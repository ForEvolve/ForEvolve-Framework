using System.Collections.Generic;

namespace ForEvolve.Pdf.PhantomJs
{
    /// <summary>
    /// Represent default paper size format.
    /// </summary>
    public sealed class PaperSizeFormat : PaperSize
    {
        public static PaperSizeFormat A3 => new PaperSizeFormat("A3");
        public static PaperSizeFormat A4 => new PaperSizeFormat("A4");
        public static PaperSizeFormat A5 => new PaperSizeFormat("A5");
        public static PaperSizeFormat Legal => new PaperSizeFormat("Legal");
        public static PaperSizeFormat Letter => new PaperSizeFormat("Letter");
        public static PaperSizeFormat Tabloid => new PaperSizeFormat("Tabloid");

        private PaperSizeFormat(string format)
        {
            Format = format;
        }

        /// <summary>
        /// Gets or sets the page size format.
        /// </summary>
        public string Format { get; }

        public override void SerializeTo(IDictionary<string, object> properties)
        {
            properties.Add("format", Format);
        }
    }
}
