using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.Models;

[Keyless]
[Table("profile_gender")]
[MySqlCharSet("utf8")]
[MySqlCollation("utf8_unicode_ci")]
public partial class ProfileGender
{
    [Column("id")]
    [StringLength(1)]
    public string Id { get; set; } = null!;

    [Column("primary_id", TypeName = "int(11)")]
    public int PrimaryId { get; set; }

    [Column("label_eng")]
    [StringLength(255)]
    public string LabelEng { get; set; } = null!;

    [Column("label_fre")]
    [StringLength(255)]
    public string LabelFre { get; set; } = null!;

    [Column("label_spa")]
    [StringLength(255)]
    public string LabelSpa { get; set; } = null!;

    [Column("display", TypeName = "tinyint(3) unsigned")]
    public byte Display { get; set; }
}
