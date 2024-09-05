using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.Models;

[Table("organisations")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class Organisation
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("account_name")]
    [StringLength(255)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? AccountName { get; set; }

    [Column("phone")]
    [StringLength(20)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? Phone { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("ceo")]
    [StringLength(255)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? Ceo { get; set; }

    [Column("type")]
    [StringLength(50)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? Type { get; set; }

    [Column("website", TypeName = "text")]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? Website { get; set; }

    [Column("geographic_area_of_interest")]
    [StringLength(255)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? GeographicAreaOfInterest { get; set; }

    [Column("cautions")]
    [StringLength(255)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string Cautions { get; set; } = null!;

    [Column("description", TypeName = "text")]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? Description { get; set; }

    [Column("primary_contact")]
    [StringLength(255)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? PrimaryContact { get; set; }

    [Column("common_area_of_interest")]
    [StringLength(255)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? CommonAreaOfInterest { get; set; }

    [Column("billing_country")]
    [StringLength(255)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? BillingCountry { get; set; }

    [Column("billing_street")]
    [StringLength(255)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? BillingStreet { get; set; }

    [Column("billing_city")]
    [StringLength(255)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? BillingCity { get; set; }

    [Column("billing_state")]
    [StringLength(255)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? BillingState { get; set; }

    [Column("billing_postal_code")]
    [StringLength(255)]
    [MySqlCharSet("utf8")]
    [MySqlCollation("utf8_unicode_ci")]
    public string? BillingPostalCode { get; set; }

    [Column("social_links", TypeName = "text")]
    public string? SocialLinks { get; set; }

    [Column("terms_and_conditions")]
    [StringLength(255)]
    public string? TermsAndConditions { get; set; }

    [Column("privacy_policy")]
    [StringLength(255)]
    public string? PrivacyPolicy { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

    [Column("account_image", TypeName = "text")]
    public string? AccountImage { get; set; }

    [Column("openingHours")]
    [StringLength(255)]
    public string? OpeningHours { get; set; }

    [Column("latitude")]
    [StringLength(50)]
    public string? Latitude { get; set; }

    [Column("longitude")]
    [StringLength(255)]
    public string? Longitude { get; set; }
}
