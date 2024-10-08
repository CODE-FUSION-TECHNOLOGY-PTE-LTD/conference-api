using CommonLib;

namespace ConferenceApi.Entity;

public class Register : IEntity
{
    public uint Id { get; set; }
    public uint Conference_Id { get; set; }
    public uint User_Id { get; set; }
    public string? Type { get; set; } // "single" or "group"
    public DateTimeOffset RegistrationDate { get; set; }
    public List<SelectedAddon>? SelectedAddons { get; set; }
    public List<TermsStatus>? TermsStatus { get; set; }
   
    public decimal ConferenceAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


}

public class SelectedAddon
{
    public Guid AddonId { get; set; }
    public string? Title { get; set; }
    public int? Quantity { get; set; }
    public decimal? Amount { get; set; }
}

public class TermsStatus
{
    public Guid TermId { get; set; }
    public bool Accepted { get; set; }
}

