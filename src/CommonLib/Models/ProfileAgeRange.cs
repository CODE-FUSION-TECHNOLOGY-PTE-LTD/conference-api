using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.Models;

[Keyless]
[Table("profile_age_range")]
[MySqlCollation("utf8mb4_unicode_ci")]
public partial class ProfileAgeRange
{
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("age_range")]
    [StringLength(255)]
    public string? AgeRange { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }
}
