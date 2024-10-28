
using CommonLib.Models;
using Microsoft.EntityFrameworkCore;


namespace CommonLib.MySql;
public class GlobalParametersDbContext : DbContext
{
    public DbSet<ProfileCountry> ProfileCountries { get; set; }
    public DbSet<ProfileTitle> ProfileTitles { get; set; }
    public DbSet<ProfileGender> ProfileGenders { get; set; }
    public DbSet<ProfileAgeRange> ProfileAgeRanges { get; set; }
    public DbSet<ProfileSector> ProfilesSectors { get; set; }

    public GlobalParametersDbContext(DbContextOptions<GlobalParametersDbContext> options) : base(options)
    {

    }


}
