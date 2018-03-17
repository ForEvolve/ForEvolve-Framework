using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.AspNetCore.UserIdFinder
{
    public class HttpHeaderUserIdFinder : IUserIdFinder
    {
        private readonly IHttpRequestValueFinder _httpRequestValueFinder;
        private readonly HttpHeaderUserIdFinderSettings _userIdFinderSettings;

        public HttpHeaderUserIdFinder(HttpHeaderUserIdFinderSettings userIdFinderSettings, IHttpRequestValueFinder httpRequestValueFinder)
        {
            _userIdFinderSettings = userIdFinderSettings ?? throw new ArgumentNullException(nameof(userIdFinderSettings));
            _httpRequestValueFinder = httpRequestValueFinder ?? throw new ArgumentNullException(nameof(httpRequestValueFinder));
        }
        
        public string GetUserId()
        {
            return _httpRequestValueFinder.Find(_userIdFinderSettings.HeaderName);
        }

        public bool HasUserId()
        {
            var id = GetUserId();
            return !string.IsNullOrWhiteSpace(id);
        }
    }
}
