using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Create a fully qualified uri based on the specified relative uri.
        /// </summary>
        /// <param name="services">The <c>Microsoft.AspNetCore.Mvc.IUrlHelper</c> to read the request data with.</param>
        /// <param name="uri">The uri to fully qualify.</param>
        /// <returns>The fully qualified uri.</returns>
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
