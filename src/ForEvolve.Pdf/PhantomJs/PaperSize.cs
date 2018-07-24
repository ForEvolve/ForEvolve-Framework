using System.Collections.Generic;

namespace ForEvolve.Pdf.PhantomJs
{
    /// <summary>
    /// Represent the paper size.
    /// </summary>
    public abstract class PaperSize
    {
        /// <summary>
        /// Gets or sets the page orientation.
        /// </summary>
        public Orientation Orientation { get; set; } = Orientation.Portrait;

        /// <summary>
        /// Gets or sets the page margins.
        /// </summary>
        public Margins Margins { get; set; } = Margins.Normal;

        public IDictionary<string, object> SerializeTo(IDictionary<string, object> properties)
        {
            ChildSerializeTo(properties);
            properties.Add("orientation", Orientation.ToString().ToLowerInvariant());
            properties.Add("margin", new
            {
                top = Margins.Top.ToString(),
                right = Margins.Right.ToString(),
                bottom = Margins.Bottom.ToString(),
                left = Margins.Left.ToString()
            });
            return properties;
        }

        protected abstract void ChildSerializeTo(IDictionary<string, object> properties);
    }
}
