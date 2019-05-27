using Newtonsoft.Json;

namespace RedFolder.LinkedIn.Models
{
    public class ShareRequest
    {
        public ShareRequest(string personId)
        {
            Owner = $"urn:li:person:{personId}";
            Distribution = new Distribution();
        }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("text")]
        public Text Text { get; set; }

        [JsonProperty("distribution")]
        public Distribution Distribution { get; set; }
    }

    public class Text
    {
        [JsonProperty("text")]
        public string Content { get; set; }
    }

    public class Distribution
    {
        public Distribution()
        {
            DistributionTarget = new DistributionTarget();
        }

        [JsonProperty("linkedInDistributionTarget")]
        public DistributionTarget DistributionTarget { get; set; }
    }

    public class DistributionTarget
    {
        public DistributionTarget()
        {
            VisibleToGuest = true;
        }

        [JsonProperty("visibleToGuest")]
        public bool VisibleToGuest { get; set; }
    }
}
