namespace AuthManager.Models;

public class AuthenticationResponse
{
    public string? JwtToken { get; set; }

    public int? ExpireIn { get; set; }
}
