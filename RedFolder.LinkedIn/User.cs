using Microsoft.Azure.WebJobs;
using RedFolder.LinkedIn.Models;

namespace RedFolder.LinkedIn
{
    public class User
    {
        [FunctionName("User")]
        public void Run([EntityTrigger]IDurableEntityContext ctx)
        {
            var newValue = ctx.GetInput<UserData>();
            ctx.SetState(newValue);
        }
    }
}
