using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using RedFolder.LinkedIn.Utils;

namespace RedFolder.LinkedIn
{
    public class StartAuthentication
    {
        private const string LINKEDIN_URL = "https://www.linkedin.com/oauth/v2/authorization";

        [FunctionName("StartAuthentication")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest request, TraceWriter log)
        {
            var clientId = Environment.GetEnvironmentVariable("LinkedInClientId", EnvironmentVariableTarget.Process);
            var state = request.Query["userid"];
            var redirectUri = RedirectUri.Generate(request);
            var redirectTo = $"{LINKEDIN_URL}?response_type=code&client_id={clientId}&state={state}&scope=r_liteprofile w_member_social&redirect_uri={redirectUri}";

            var response = new RedirectResult(redirectTo);

            return response;
        }
    }
}
