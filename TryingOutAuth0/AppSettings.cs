namespace TryingOutAuth0
{
    public class AppSettings
    {
        public Auth0 Auth0 { get; set; }
    }

    public class Auth0
    {
        public string Domain { get; set; }
        public string ApiIdentifier { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string Connection { get; set; }
    }
}
