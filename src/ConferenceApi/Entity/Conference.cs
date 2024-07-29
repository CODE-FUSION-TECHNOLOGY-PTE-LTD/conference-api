using CommonLib;



namespace ConferenceApi.Entity;

public class Conference : IEntity
{
    public uint Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? OrganizationId { get; set; }
    public List<Category>? Categories { get; set; }
    public List<Addon>? Addons { get; set; }
    public List<Coupon>? Coupons { get; set; }

    
    public Location? Location { get; set; }


    public Contact? Contact { get; set; }


    public List<Term>? Terms { get; set; }
}
public class Category
{

    public Guid Id { get; set; }


    public string? Title { get; set; }

    public List<DateTime>? DateOptions { get; set; }


    public List<PriceBookEntry>? PriceBookEntries { get; set; }
}

public class PriceBookEntry
{


    public Guid Id { get; set; }

    public string? IncomeLevel { get; set; }


    public List<Price>? Prices { get; set; }
}

public class Price
{

    public Guid Id { get; set; }


    public string? Type { get; set; }

    public decimal? PriceAmount { get; set; }


    public decimal Discount { get; set; }


    public string? Currency { get; set; }


    public DateTime ValidFrom { get; set; }


    public DateTime ValidUntil { get; set; }
}

public class Addon
{
    public Guid Id { get; set; }


    public string? Title { get; set; }

    public string? Description { get; set; }

    public decimal Amount { get; set; }
}

public class Coupon
{

    public Guid Id { get; set; }

    public string? Code { get; set; }


    public string? DiscountType { get; set; }


    public decimal DiscountValue { get; set; }


    public List<string>? EmailList { get; set; }
}

public class Location
{

    public Guid Id { get; set; }


    public string? Venue { get; set; }

    public Address? Address { get; set; }
}

public class Address
{

    public Guid Id { get; set; }

    public string? StreetAddress { get; set; }


    public string? City { get; set; }


    public string? ZipCode { get; set; }


    public string? Country { get; set; }
}

public class Contact
{

    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

public class Term
{

    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsRequired { get; set; }
}
