using Microsoft.AspNetCore.Http;

namespace angu.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Allow-Control-Expose-Headers", "Application-Error");
         }
    }
}