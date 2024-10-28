
using CommonLib.Models;
using Microsoft.EntityFrameworkCore;


namespace CommonLib.MySql;
public class IdentityDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Organisation> Organisations { get; set; }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {

    }


}
