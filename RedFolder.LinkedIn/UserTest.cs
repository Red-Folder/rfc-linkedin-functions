using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using RedFolder.LinkedIn.Models;
using System;
using System.Threading.Tasks;

namespace RedFolder.LinkedIn
{
    public class UserTest
    {
        private readonly LinkedInProxy _proxy;

        public UserTest(LinkedInProxy proxy)
        {
            _proxy = proxy;
        }


        [FunctionName("UserTest")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequest request,
                                [OrchestrationClient]IDurableOrchestrationClient entityClient,
                                TraceWriter log)
        {
            var userId = request.Query["userid"];
            var testEntity = new EntityId(nameof(User), userId);

            var current = await entityClient.ReadEntityStateAsync<UserData>(testEntity);

            if (current.EntityExists)
            {
                var share = new ShareRequest(current.EntityState.PersonId);
                share.Text = new Text { Content = "Test share" };
                await _proxy.Share(current.EntityState.AccessToken, share);

                return new OkObjectResult(current);
            }

            return new UnauthorizedResult();
        }
    }
}
