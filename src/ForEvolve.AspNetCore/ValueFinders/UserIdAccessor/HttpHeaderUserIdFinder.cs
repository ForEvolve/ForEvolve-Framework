using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.AspNetCore.UserIdFinder
{
    public class HttpHeaderUserIdFinder : IUserIdAccessor
    {
        private readonly IHttpHeaderValueFinder _httpRequestValueFinder;
        private readonly HttpHeaderUserIdFinderSettings _userIdFinderSettings;

        public HttpHeaderUserIdFinder(HttpHeaderUserIdFinderSettings userIdFinderSettings, IHttpHeaderValueFinder httpRequestValueFinder)
        {
            _userIdFinderSettings = userIdFinderSettings ?? throw new ArgumentNullException(nameof(userIdFinderSettings));
            _httpRequestValueFinder = httpRequestValueFinder ?? throw new ArgumentNullException(nameof(httpRequestValueFinder));
        }
        
        public string FindUserId()
        {
            return _httpRequestValueFinder.FindHeader(_userIdFinderSettings.HeaderName);
        }

        public bool HasUserId()
        {
            var id = FindUserId();
            return !string.IsNullOrWhiteSpace(id);
        }
    }
}
