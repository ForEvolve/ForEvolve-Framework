using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Pdf.PhantomJs
{
    public interface IHtmlToPdfConverterOptionsSerializer
    {
        string Serialize(HtmlToPdfConverterOptions options);
    }

    public class HtmlToPdfConverterOptionsJsonSerializer : IHtmlToPdfConverterOptionsSerializer
    {
        public string Serialize(HtmlToPdfConverterOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
