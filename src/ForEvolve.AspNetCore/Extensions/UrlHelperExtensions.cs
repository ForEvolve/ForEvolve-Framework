using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class UrlHelperExtensions
    {
        public static string ToFullyQualifiedUri(this IUrlHelper urlHelper, string uri)
        {
            if (string.IsNullOrWhiteSpace(uri)) { throw new ArgumentNullException(nameof(uri)); }
            if (uri[0] != '/') { throw new ArgumentException("Invalid uri. The uri must begin with a \"/\".", nameof(uri)); }

            var request = urlHelper.ActionContext.HttpContext.Request;
            var scheme = request.Scheme;
            var host = request.Host.ToUriComponent();
            var result = $"{scheme}://{host}{uri}";
            return result;
        }
    }
}
