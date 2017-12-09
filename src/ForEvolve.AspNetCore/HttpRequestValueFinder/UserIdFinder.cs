using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.AspNetCore
{
    public class UserIdFinder : IUserIdFinder
    {
        private readonly IHttpRequestValueFinder _httpRequestValueFinder;
        private readonly UserIdFinderSettings _userIdFinderSettings;

        public UserIdFinder(UserIdFinderSettings userIdFinderSettings, IHttpRequestValueFinder httpRequestValueFinder)
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

    public interface IUserIdFinder
    {
        string GetUserId();
        bool HasUserId();
    }

    public class UserIdFinderSettings
    {
        public const string DefaultHeaderName = "X-ForEvolve-UserId";

        public string HeaderName { get; set; } = DefaultHeaderName;
    }
}
