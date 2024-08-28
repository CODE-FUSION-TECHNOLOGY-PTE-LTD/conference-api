using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.Models;

[Table("users")]
[Index("Country", Name = "Country")]
[Index("HeardAbout", Name = "Heard_about")]
[Index("JobCategory", Name = "Job_category")]
[Index("Qualification", Name = "Qualifications")]
[Index("Gender", Name = "Sex")]
[Index("AzId", Name = "az_id", IsUnique = true)]
[Index("Email", Name = "email", IsUnique = true)]
[Index("Nationality", Name = "nationality")]
[Index("Title", Name = "title")]
[Index("TypeOfInstitution", Name = "type_of_organisation")]
[MySqlCharSet("utf8")]
[MySqlCollation("utf8_unicode_ci")]
public partial class User : IEntity
{
    [Key]
    [Column("id", TypeName = "int(7) unsigned zerofill")]
    public uint Id { get; set; }

    [Column("merge_id", TypeName = "int(11)")]
    public int? MergeId { get; set; }

    [Column("title", TypeName = "int(11)")]
    public int? Title { get; set; }

    [Column("image")]
    [StringLength(256)]
    public string? Image { get; set; }

    /// <summary>
    /// Admin configaration
    /// </summary>
    [Column("organisation_id", TypeName = "int(11)")]
    public int? OrganisationId { get; set; }

    [Column("surname")]
    [StringLength(255)]
    public string? Surname { get; set; }

    [Column("first_name")]
    [StringLength(255)]
    public string? FirstName { get; set; }

    [Column("second_name")]
    [StringLength(255)]
    public string? SecondName { get; set; }

    [Column("qualification", TypeName = "int(11)")]
    public int? Qualification { get; set; }

    [Column("gender")]
    [StringLength(1)]
    public string? Gender { get; set; }

    [Column("age_range")]
    [StringLength(10)]
    public string? AgeRange { get; set; }

    [Column("job_title")]
    [StringLength(255)]
    public string? JobTitle { get; set; }

    [Column("job_category", TypeName = "int(11)")]
    public int? JobCategory { get; set; }

    [Column("institution")]
    [StringLength(255)]
    public string? Institution { get; set; }

    [Column("department")]
    [StringLength(80)]
    public string? Department { get; set; }

    /// <summary>
    /// Admin configaration
    /// </summary>
    [Column("department_id", TypeName = "int(11)")]
    public int? DepartmentId { get; set; }

    [Column("type_of_institution", TypeName = "int(11)")]
    public int? TypeOfInstitution { get; set; }

    [Column("address_line1")]
    [StringLength(255)]
    public string? AddressLine1 { get; set; }

    [Column("address_line2")]
    [StringLength(255)]
    public string? AddressLine2 { get; set; }

    [Column("address_line3")]
    [StringLength(255)]
    public string? AddressLine3 { get; set; }

    [Column("po_code")]
    [StringLength(20)]
    public string? PoCode { get; set; }

    [Column("city")]
    [StringLength(255)]
    public string? City { get; set; }

    [Column("state")]
    [StringLength(255)]
    public string? State { get; set; }

    [Column("country")]
    [StringLength(4)]
    public string? Country { get; set; }

    [Column("country_id")]
    [StringLength(10)]
    public string? CountryId { get; set; }

    [Column("nationality")]
    [StringLength(3)]
    public string? Nationality { get; set; }

    [Column("fax")]
    [StringLength(20)]
    public string? Fax { get; set; }

    [Column("telephone")]
    [StringLength(20)]
    public string? Telephone { get; set; }

    [Column("telephone_country_code")]
    [StringLength(5)]
    public string? TelephoneCountryCode { get; set; }

    [Column("fax_country_code")]
    [StringLength(5)]
    public string? FaxCountryCode { get; set; }

    [Column("mobile_country_code")]
    [StringLength(5)]
    public string? MobileCountryCode { get; set; }

    [Column("mobile")]
    [StringLength(255)]
    public string? Mobile { get; set; }

    [Column("subscribe")]
    public bool? Subscribe { get; set; }

    [Column("email")]
    [MySqlCollation("utf8_general_ci")]
    public string? Email { get; set; }

    [Column("alternative_email")]
    [StringLength(255)]
    public string? AlternativeEmail { get; set; }

    [Column("password")]
    [StringLength(256)]
    public string? Password { get; set; }

    [Column("old_password")]
    [StringLength(255)]
    public string? OldPassword { get; set; }

    [Column("marketing_opt")]
    public bool? MarketingOpt { get; set; }

    [Column("group_sponsor")]
    [StringLength(6)]
    public string? GroupSponsor { get; set; }

    [Column("heard_about", TypeName = "int(11)")]
    public int? HeardAbout { get; set; }

    [Column("origin", TypeName = "text")]
    public string? Origin { get; set; }

    [Column("date_of_birth")]
    [StringLength(15)]
    public string? DateOfBirth { get; set; }

    [Column("li_id")]
    [StringLength(255)]
    public string? LiId { get; set; }

    [Column("fb_id")]
    [StringLength(255)]
    public string? FbId { get; set; }

    [Column("gg_id")]
    [StringLength(255)]
    public string? GgId { get; set; }

    [Column("az_id")]
    public string? AzId { get; set; }

    [Column("stripe_id")]
    [StringLength(50)]
    public string? StripeId { get; set; }

    [Column("last_update", TypeName = "datetime")]
    public DateTime? LastUpdate { get; set; }

    [Column("last_login", TypeName = "datetime")]
    public DateTime? LastLogin { get; set; }

    [Column("date_created", TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [Column("pwd_reset_token_creation_date", TypeName = "datetime")]
    public DateTime? PwdResetTokenCreationDate { get; set; }

    [Column("email_verified_at", TypeName = "datetime")]
    public DateTime? EmailVerifiedAt { get; set; }

    [Column("remember_token", TypeName = "text")]
    public string? RememberToken { get; set; }

    [Column("operator_id", TypeName = "int(7) unsigned zerofill")]
    public uint? OperatorId { get; set; }

    [Column("sync_flag")]
    public bool? SyncFlag { get; set; }

    [Column("type_of_institution_other", TypeName = "text")]
    public string? TypeOfInstitutionOther { get; set; }

    [Column("job_category_other", TypeName = "text")]
    public string? JobCategoryOther { get; set; }

    [Column("deceased", TypeName = "int(10) unsigned")]
    public uint? Deceased { get; set; }

    [Column("msgraph_gid")]
    [StringLength(255)]
    public string? MsgraphGid { get; set; }

    [Column("custom_inputs", TypeName = "json")]
    public string? CustomInputs { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("email_test")]
    [StringLength(255)]
    public string? EmailTest { get; set; }

    [Column("background_image")]
    [StringLength(255)]
    public string? BackgroundImage { get; set; }

    [Column("is_one_time")]
    public bool? IsOneTime { get; set; }

    [Column("layout")]
    [StringLength(50)]
    public string? Layout { get; set; }

    [Column("password_expire_date")]
    public DateOnly? PasswordExpireDate { get; set; }

    [Column("google2fa_secret")]
    public string? Google2faSecret { get; set; }

    [Column("is_multiple_membership", TypeName = "int(11)")]
    public int IsMultipleMembership { get; set; }

    [Column("unsubscribe_list", TypeName = "text")]
    public string? UnsubscribeList { get; set; }

    [Column("is_test_user", TypeName = "tinyint(4)")]
    public sbyte IsTestUser { get; set; }

    [Column("is_mailchimp", TypeName = "tinyint(3)")]
    public sbyte IsMailchimp { get; set; }

    /// <summary>
    /// need to review
    /// </summary>
    [Column("privacy_policy_accept", TypeName = "int(11)")]
    public int? PrivacyPolicyAccept { get; set; }

    [Column("otp")]
    [StringLength(20)]
    public string? Otp { get; set; }

    [Column("otp_at", TypeName = "datetime")]
    public DateTime? OtpAt { get; set; }

}
