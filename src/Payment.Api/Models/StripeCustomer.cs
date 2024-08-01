namespace Payment.Api.Models;

public class StripeCustomer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }

}
