namespace ForEvolve.AspNetCore
{
    public class UserIdFinderSettings
    {
        public const string DefaultHeaderName = "X-ForEvolve-UserId";

        public string HeaderName { get; set; } = DefaultHeaderName;
    }
}
