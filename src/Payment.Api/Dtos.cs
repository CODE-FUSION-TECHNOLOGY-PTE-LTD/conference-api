namespace Payment.Api;

public class Dtos
{
    public class OrderRefDto
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public List<RegistrationRefDto> Registrations { get; set; } = new List<RegistrationRefDto>();
        public AddressDto? Address { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public int PaymentStatus { get; set; }
        public List<PaymentDetailDto> PaymentDetails { get; set; } = new List<PaymentDetailDto>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class OrderCreateDto
    {
        public uint UserId { get; set; }
        public List<RegistrationRefDto> Registrations { get; set; } = new List<RegistrationRefDto>();
        public AddressDto? Address { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public int PaymentStatus { get; set; }
        public List<PaymentDetailDto> PaymentDetails { get; set; } = new List<PaymentDetailDto>();
    }

    public class OrderUpdateDto
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public List<RegistrationRefDto> Registrations { get; set; } = new List<RegistrationRefDto>();
        public AddressDto? Address { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public int PaymentStatus { get; set; }
        public List<PaymentDetailDto> PaymentDetails { get; set; } = new List<PaymentDetailDto>();
    }

    public class OrderDeleteDto
    {
        public uint Id { get; set; }
    }

    public class RegistrationRefDto
    {
        public uint? RegistrationId { get; set; }
    }

    public class AddressDto
    {
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
    }

    public class PaymentDetailDto
    {
        public string? StripePaymentId { get; set; }
        public decimal AmountPaid { get; set; }
        public string? Currency { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? TransactionId { get; set; }
    }
}
