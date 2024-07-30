using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.Models;

[Keyless]
[Table("profile_country")]
[MySqlCharSet("utf8")]
[MySqlCollation("utf8_unicode_ci")]
public partial class ProfileCountry
{
    [Column("id", TypeName = "int(3) unsigned zerofill")]
    public uint Id { get; set; }

    [Column("code_ISO")]
    [StringLength(3)]
    public string CodeIso { get; set; } = null!;

    [Column("alpha3_code_ISO")]
    [StringLength(3)]
    public string Alpha3CodeIso { get; set; } = null!;

    [Column("region")]
    [StringLength(4)]
    public string? Region { get; set; }

    [Column("label_eng")]
    [StringLength(55)]
    public string LabelEng { get; set; } = null!;

    [Column("phonecode", TypeName = "int(11)")]
    public int? Phonecode { get; set; }

    [Column("label_fre")]
    [StringLength(55)]
    public string LabelFre { get; set; } = null!;

    [Column("label_spa")]
    [StringLength(55)]
    public string LabelSpa { get; set; } = null!;

    [Column("nationality")]
    [StringLength(256)]
    public string Nationality { get; set; } = null!;

    [Column("world_bank", TypeName = "int(11)")]
    public int WorldBank { get; set; }

    [Column("world_bank_income_group")]
    [StringLength(20)]
    public string? WorldBankIncomeGroup { get; set; }

    [Column("display", TypeName = "tinyint(3) unsigned")]
    public byte Display { get; set; }
}
