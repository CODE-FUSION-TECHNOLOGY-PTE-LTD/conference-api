using Microsoft.EntityFrameworkCore;
using CommonLib.Models;

namespace CommonLib.MySql;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

}

