using CommonLib;
using Payment.Api.Models.Entity;

namespace Payment.Api.Models;

public class PaymentModel : IEntity
{
    public uint Id { get; set; }
    public string? CustomerEmail { get; set; }
    public uint UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public long DueAmount { get; set; }

    public decimal TotalPaidAmount { get; set; }
    public int PaymentStatus { get; set; }

    public List<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();
    public Address? Address { get; set; }

    public List<RegistrationRef> Registrations { get; set; } = new List<RegistrationRef>();
}

