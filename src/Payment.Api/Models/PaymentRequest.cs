namespace Payment.Api.Models;

public class PaymentRequest
{
    public string? PriceId { get; set; }

    public string? CustomerEmail { get; set; }
   public long? Quantity { get; set; }
    public string? CustomerId { get; set; }
}
