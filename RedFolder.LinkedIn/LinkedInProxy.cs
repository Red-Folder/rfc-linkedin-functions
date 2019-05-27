using Newtonsoft.Json;
using RedFolder.LinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.LinkedIn
{
    public class LinkedInProxy
    {
        private const string AccessTokenUrl = "https://www.linkedin.com/oauth/v2/accessToken";
        private const string MeUrl = "https://api.linkedin.com/v2/me";
        private const string ShareUrl = "https://api.linkedin.com/v2/shares";

        private static HttpClient _client = new HttpClient();

        public async Task<AccessTokenResponse> AccessToken(string code, string redirectUri, string clientId, string clientSecret)
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

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AccessTokenResponse>(json);

            return result;
        }

        public async Task< MeResponse> Me(string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _client.DefaultRequestHeaders.Add("X-Restli-Protocol-Version", "2.0.0");
            var response = await _client.GetAsync(MeUrl);

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MeResponse>(json);

            return result;
        }

        public async Task Share(string accessToken, ShareRequest share)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _client.DefaultRequestHeaders.Add("X-Restli-Protocol-Version", "2.0.0");

            var json = JsonConvert.SerializeObject(share);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(ShareUrl, content);
        }
    }
}
