
using CommonLib.Models;
using Microsoft.EntityFrameworkCore;


namespace CommonLib.MySql;
public class MySqlDbContext : DbContext
{
    public MySqlDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
}
