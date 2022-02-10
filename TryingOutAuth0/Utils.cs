using Microsoft.Extensions.Configuration;

namespace TryingOutAuth0
{
    public static class Utils
    {
        public static string URLFromDomain(string domain) => $"https://{domain}/";
    }
}
