using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

[assembly: FunctionsStartup(typeof(RedFolder.LinkedIn.Startup))]

namespace RedFolder.LinkedIn
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<LinkedInProxy>();
        }
    }
}
