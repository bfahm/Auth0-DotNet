using System;

namespace TryingOutAuth0.Auth.Auth0Client.Models
{
    public class GetUserModel
    {
        public string sub { get; set; }
        public string nickname { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public DateTime updated_at { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
    }

}
