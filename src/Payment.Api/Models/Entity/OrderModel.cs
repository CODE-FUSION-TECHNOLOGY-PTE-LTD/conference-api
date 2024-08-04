using CommonLib;

namespace Payment.Api.Models.Entity;

public class OrderModel : IEntity
{
    public uint Id { get; set; }
    public uint UserId { get; set; }
    public List<RegistrationRef> Registrations { get; set; } = new List<RegistrationRef>();
    public Address? Address { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DueAmount { get; set; }
    public decimal TotalPaidAmount { get; set; }
    public int PaymentStatus { get; set; }

    public List<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


}
public class RegistrationRef
{

    public uint? RegistrationId { get; set; }
}

public class Address
{
    public string? AddressLine { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
}

public class PaymentDetail
{
    public string? StripePaymentId { get; set; }
    public decimal AmountPaid { get; set; }
    public string? Currency { get; set; }


    public DateTime PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }
    public string? TransactionId { get; set; }
}