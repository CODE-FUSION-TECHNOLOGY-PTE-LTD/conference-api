using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.Models;

[Table("departments")]
public partial class Department
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("department_name")]
    [StringLength(150)]
    public string DepartmentName { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("budget")]
    [StringLength(150)]
    public string? Budget { get; set; }

    [Column("organisation_id", TypeName = "int(11)")]
    public int OrganisationId { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }
}
