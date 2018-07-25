using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.AspNetCore.UserIdFinder
{
    public class HttpHeaderUserIdAccessor : IUserIdAccessor
    {
        private readonly IHttpHeaderValueAccessor _httpRequestValueFinder;
        private readonly HttpHeaderUserIdAccessorSettings _userIdFinderSettings;

        public HttpHeaderUserIdAccessor(HttpHeaderUserIdAccessorSettings userIdFinderSettings, IHttpHeaderValueAccessor httpRequestValueFinder)
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
