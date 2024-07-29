namespace RegisterApi;

public class RegisterDtos
{
    public class RegisterConferenceDto
    {
        public uint Id { get; set; }
        public uint ConferenceId { get; set; }
        public uint UserId { get; set; }
        public string? Type { get; set; } // "single" or "group"
        public DateTime RegistrationDate { get; set; }
        public List<SelectedAddonDto>? SelectedAddons { get; set; }
        public List<TermsStatusDto>? TermsStatus { get; set; }
        public AppliedCouponDto? AppliedCouponCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
    }
    public class RegisterCreateDto
    {
        public uint ConferenceId { get; set; }
        public uint UserId { get; set; }
        public string? Type { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<SelectedAddonDto>? SelectedAddons { get; set; }
        public List<TermsStatusDto>? TermsStatus { get; set; }
        public AppliedCouponDto? AppliedCouponCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
    }

    // DTO for updating a registration
    public class RegisterUpdateDto
    {
        public uint ConferenceId { get; set; }
        public uint UserId { get; set; }

        public string? Type { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<SelectedAddonDto>? SelectedAddons { get; set; }
        public List<TermsStatusDto>? TermsStatus { get; set; }
        public AppliedCouponDto? AppliedCouponCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
    }

    // DTO for selected addons
    public class SelectedAddonDto
    {
        public Guid AddonId { get; set; }
        public string? Title { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }


    public class TermsStatusDto
    {
        public Guid TermId { get; set; }
        public bool Accepted { get; set; }
    }

    // DTO for applied coupon
    public class AppliedCouponDto
    {
        public string? Code { get; set; }
        public string? DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
    }
}
