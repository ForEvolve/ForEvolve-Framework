namespace ForEvolve.AspNetCore.Services
{
    public interface IHtmlToPlainTextEmailBodyConverter
    {
        string ConvertToPlainText(string body);
    }
}
