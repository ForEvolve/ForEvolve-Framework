using System;
using System.Collections.Generic;

namespace ForEvolve.Pdf.PhantomJs
{
    public class ViewportSize : IEquatable<ViewportSize>
    {
        public static ViewportSize Default => new ViewportSize { Width = 600, Height = 600 };

        /// <summary>
        /// Gets or sets the width of the viewport.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the viewport.
        /// </summary>
        public int Height { get; set; }

        public bool Equals(ViewportSize other)
        {
            if (Width != other.Width)
            {
                return false;
            }
            if (Height != other.Height)
            {
                return false;
            }
            return true;
        }

        public IDictionary<string, object> SerializeTo(IDictionary<string, object> properties)
        {
            properties.Add("width", Width);
            properties.Add("height", Height);
            return properties;
        }
    }
}
