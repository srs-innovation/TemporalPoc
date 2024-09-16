using Microsoft.EntityFrameworkCore;
public class AgilityDbContext : DbContext
{
    public AgilityDbContext(DbContextOptions<AgilityDbContext> options) : base(options)
    {
    }
    public DbSet<AgilityOrder> Orders { get; set; }
    public DbSet<AgilityLineItem> LineItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgilityOrder>().HasMany(o => o.LineItems).WithOne().HasForeignKey(l => l.OrderId);
    }
}