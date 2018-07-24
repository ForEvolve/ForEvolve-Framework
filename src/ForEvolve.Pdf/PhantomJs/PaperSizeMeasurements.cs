using System;
using System.Collections.Generic;

namespace ForEvolve.Pdf.PhantomJs
{
    /// <summary>
    /// Represent custom paper size formats.
    /// </summary>
    public sealed class PaperSizeMeasurements : PaperSize
    {
        /// <summary>
        /// Initializes a new instance of the ForEvolve.Pdf.PhantomJs.PaperSizeMeasurements class.
        /// </summary>
        /// <param name="width">The width of the page</param>
        /// <param name="height">The height of the page</param>
        public PaperSizeMeasurements(Size width, Size height)
        {
            Width = width ?? throw new ArgumentNullException(nameof(width));
            Height = height ?? throw new ArgumentNullException(nameof(height));
        }

        /// <summary>
        /// Gets or sets the width of the page.
        /// </summary>
        public Size Width { get; }

        /// <summary>
        /// Gets or sets the height of the page.
        /// </summary>
        public Size Height { get; }

        public override void SerializeTo(IDictionary<string, object> properties)
        {
            properties.Add("width", Width.ToString());
            properties.Add("height", Height.ToString());
        }
    }
}
