using Microsoft.EntityFrameworkCore;
using Payment.Api.Models;

namespace Payment.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<StripeCustomer> Users { get; set; }
    public DbSet<PaymentModel> Products { get; set; }
}
