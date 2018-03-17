namespace ForEvolve.AspNetCore
{
    public interface IUserIdFinder
    {
        string GetUserId();
        bool HasUserId();
    }
}
