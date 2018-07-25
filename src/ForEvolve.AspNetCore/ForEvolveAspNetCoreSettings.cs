using Microsoft.Extensions.Configuration;

namespace ForEvolve.AspNetCore
{
    public class ForEvolveAspNetCoreSettings
    {
        public const string DefaultEmailOptionsConfigurationKey = "EmailOptions";
        public ForEvolveAspNetCoreSettings()
        {
            EmailOptionsConfigurationKey = DefaultEmailOptionsConfigurationKey;
        }

        public IConfiguration Configuration { get; set; }
        public string EmailOptionsConfigurationKey { get; set; }
    }
}
