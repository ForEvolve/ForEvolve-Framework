using System.Collections.Generic;

namespace ForEvolve.Pdf.PhantomJs
{
    /// <summary>
    /// Represent the paper size.
    /// </summary>
    public abstract class PaperSize
    {
        public abstract void SerializeTo(IDictionary<string, object> properties);
    }
}
