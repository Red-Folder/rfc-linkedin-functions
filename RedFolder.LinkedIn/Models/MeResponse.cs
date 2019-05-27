using Newtonsoft.Json;

namespace RedFolder.LinkedIn.Models
{
    public class MeResponse
    {
        [JsonProperty("id")]
        public string PersonId { get; set; }
    }
}
