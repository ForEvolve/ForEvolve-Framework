using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ForEvolve.Pdf.PhantomJs
{
    public class HtmlToPdfConverterOptionsJsonSerializer : IHtmlToPdfConverterOptionsSerializer
    {
        public string Serialize(HtmlToPdfConverterOptions options)
        {
            var settings = new Dictionary<string, object>
            {
                { "paperSize", options.PaperSize.SerializeTo(new Dictionary<string, object>()) },
                { "viewportSize", options.ViewportSize.SerializeTo(new Dictionary<string, object>()) },
                { "zoomFactor", options.ZoomFactor }
            };

            if(!options.ClipRectangle.Equals(ClipRectangle.Null))
            {
                settings.Add("clipRect", options.ClipRectangle.SerializeTo(new Dictionary<string, object>()));
            }

            var json = JsonSerializer.Serialize(settings);
            return json.Replace("\"", "\\\"");
        }
    }
}
