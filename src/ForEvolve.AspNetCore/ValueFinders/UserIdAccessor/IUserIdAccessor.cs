namespace ForEvolve.AspNetCore
{
    public interface IUserIdAccessor
    {
        string FindUserId();
        bool HasUserId();
    }
}
