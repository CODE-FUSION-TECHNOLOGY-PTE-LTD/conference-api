namespace Payment.Api.Models;

public class PaymentModel
{
    public string? Id { get; set; }
    public int Status { get; set; }
    public string? CustomerEmail { get; set; }
}
