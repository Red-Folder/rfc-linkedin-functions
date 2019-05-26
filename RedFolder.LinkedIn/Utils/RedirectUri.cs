using Microsoft.AspNetCore.Http;

namespace RedFolder.LinkedIn.Utils
{
    public static class RedirectUri
    {
        public static string Generate(HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host.ToString()}/api/LinkedInCallback";
        }
    }
}
