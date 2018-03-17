namespace ForEvolve.AspNetCore.UserIdFinder
{
    public class AuthenticatedUserIdFinderSettings
    {
        public const string DefaultUserIdClaimType = "sub";

        public string UserIdClaimType { get; set; } = DefaultUserIdClaimType;
    }
}
