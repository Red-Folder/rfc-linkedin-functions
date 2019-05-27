using System;

namespace RedFolder.LinkedIn.Models
{
    public class UserData
    {
        public string PersonId { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
