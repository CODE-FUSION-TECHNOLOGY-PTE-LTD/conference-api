
using CommonLib.Models;
using Microsoft.EntityFrameworkCore;



namespace CommonLib.MySql;
public class MySqlDbContext(DbContextOptions<MySqlDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<ProfileCountry> ProfileCountries { get; set; }
    public DbSet<ProfileTitle> ProfileTitles { get; set; }
    public DbSet<ProfileGender> ProfileGenders { get; set; }
    public DbSet<ProfileAgeRange> ProfileAgeRanges { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<ProfileSector> ProfilesSectors { get; set; }
    public DbSet<Organisation> Organisations { get; set; }
}
