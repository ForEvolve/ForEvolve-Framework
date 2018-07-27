﻿namespace ForEvolve.AspNetCore.UserIdFinder
{
    public class HttpHeaderUserIdAccessorSettings
    {
        public const string DefaultHeaderName = "X-ForEvolve-UserId";

        public string HeaderName { get; set; } = DefaultHeaderName;
    }
}
