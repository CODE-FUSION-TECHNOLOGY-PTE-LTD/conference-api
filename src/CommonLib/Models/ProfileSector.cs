using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.Models;

[Table("profile_sector")]
public partial class ProfileSector
{
    [Key]
    [Column("id", TypeName = "int(2)")]
    public int Id { get; set; }

    [Column("lable_eng")]
    [StringLength(255)]
    public string LableEng { get; set; } = null!;

    [Column("document_has")]
    [StringLength(1)]
    public string DocumentHas { get; set; } = null!;
}
