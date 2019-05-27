using Newtonsoft.Json;
using System;

namespace RedFolder.LinkedIn.Models
{
    public class AccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        public DateTime Expires => DateTime.Now.AddSeconds(ExpiresIn);
    }
}
