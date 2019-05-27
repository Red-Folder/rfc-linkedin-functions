using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using RedFolder.LinkedIn.Models;
using RedFolder.LinkedIn.Utils;
using System;
using System.Threading.Tasks;

namespace RedFolder.LinkedIn
{
    public class LinkedInCallback
    {
        private readonly LinkedInProxy _proxy;

        public LinkedInCallback(LinkedInProxy proxy)
        {
            _proxy = proxy;
        }

        [FunctionName("LinkedInCallback")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest request,
            [OrchestrationClient]IDurableOrchestrationClient entityClient,
            TraceWriter log)
        {
            var code = request.Query["code"];
            var state = request.Query["state"];
            var error = request.Query["error"];
            var errorDescription = request.Query["error_description"];

            var clientId = Environment.GetEnvironmentVariable("LinkedInClientId", EnvironmentVariableTarget.Process);
            var clientSecret = Environment.GetEnvironmentVariable("LinkedInClientSecret", EnvironmentVariableTarget.Process);
            var entityHub = Environment.GetEnvironmentVariable("EntityHub", EnvironmentVariableTarget.Process);

            var redirectUri = RedirectUri.Generate(request);

            var accessToken = await _proxy.AccessToken(code, redirectUri, clientId, clientSecret);

            var me = await _proxy.Me(accessToken.AccessToken);

            var userData = new UserData
            {
                PersonId = me.PersonId,
                AccessToken = accessToken.AccessToken,
                Expires = accessToken.Expires
            };

            var entityId = new EntityId(nameof(User), state);

            await entityClient.SignalEntityAsync(entityId, "", userData, entityHub);

            return new OkObjectResult("Done - you can close ths window");
        }
    }
}
