using ForEvolve.Pdf.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Pdf.PhantomJs
{
    public class HtmlToPdfConverter : IHtmlToPdfConverter
    {
        //private readonly OS _platform;
        //private readonly HtmlToPdfConverterOptions _options;


        public string Convert(string html, string outputFolder)
        {
            throw new NotImplementedException();
        }

        private enum OS
        {
            LINUX,
            WINDOWS,
            OSX
        }
    }
}
