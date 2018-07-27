using System;
using System.Collections.Generic;

namespace ForEvolve.Pdf.PhantomJs
{
    public class ClipRectangle : IEquatable<ClipRectangle>
    {
        public static ClipRectangle Null => new ClipRectangle();

        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public bool Equals(ClipRectangle other)
        {
            if (Top != other.Top)
            {
                return false;
            }
            if (Left != other.Left)
            {
                return false;
            }
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
            properties.Add("top", Top);
            properties.Add("left", Left);
            properties.Add("width", Width);
            properties.Add("height", Height);
            return properties;
        }
    }
}
