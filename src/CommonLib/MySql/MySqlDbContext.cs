
using CommonLib.Models;
using Microsoft.EntityFrameworkCore;


namespace CommonLib.MySql;
public class MySqlDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ProfileCountry> ProfileCountries { get; set; }
    
    public MySqlDbContext(DbContextOptions options) : base(options)
    {

    }


}
