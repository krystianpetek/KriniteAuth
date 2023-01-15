namespace KriniteAuthServer.ResourceDataApp.Models;

public class AuthConfig
{
    public string RequestUri { get; set; }

    public string GrantType { get; set; }

    public string ClientId { get; set;}

    public string ClientSecret { get; set;}

    public string Scope { get; set;}
}
