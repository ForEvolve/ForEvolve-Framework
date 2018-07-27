namespace ForEvolve.AspNetCore.UserIdFinder
{
    public class AuthenticatedUserIdAccessorSettings
    {
        public const string DefaultUserIdClaimType = "sub";

        public string UserIdClaimType { get; set; } = DefaultUserIdClaimType;
    }
}
