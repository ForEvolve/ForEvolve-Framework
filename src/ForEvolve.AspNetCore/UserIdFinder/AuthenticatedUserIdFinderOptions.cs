namespace ForEvolve.AspNetCore.UserIdFinder
{
    public class AuthenticatedUserIdFinderOptions
    {
        public const string DefaultUserIdClaimType = "sub";

        public string UserIdClaimType { get; set; } = DefaultUserIdClaimType;
    }
}
