using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Pdf.PhantomJs
{
    public class HtmlToPdfConverterOptionsJsonSerializer : IHtmlToPdfConverterOptionsSerializer
    {
        public string Serialize(HtmlToPdfConverterOptions options)
        {
            var properties = new Dictionary<string, object>();
            var paperSize = new { paperSize = properties };
            options.PaperSize.SerializeTo(properties);
            properties.Add("orientation", options.Orientation.ToString().ToLowerInvariant());
            properties.Add("margin", new
            {
                top = options.Margins.Top.ToString(),
                right = options.Margins.Right.ToString(),
                bottom = options.Margins.Bottom.ToString(),
                left = options.Margins.Left.ToString()
            });

            return JsonConvert.SerializeObject(paperSize);
        }
    }
}
