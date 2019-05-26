using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.LinkedIn
{
    public class LinkedInProxy
    {
        private const string AccessTokenUrl = "https://www.linkedin.com/oauth/v2/accessToken";

        private static HttpClient _client = new HttpClient();

        public async Task<string> GetAccesToken(string code, string redirectUri, string clientId, string clientSecret)
        {
            var payload = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret)
            };
            var content = new FormUrlEncodedContent(payload);

            var response = await _client.PostAsync(AccessTokenUrl, content);

            // Convert response to accesstokenrepose object

            return "";
        }

        public class AccessTokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
            [JsonProperty("expires_in")]
            public long ExpiresIn { get; set; }
        }
    }
}
