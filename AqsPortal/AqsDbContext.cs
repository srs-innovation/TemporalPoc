using Microsoft.EntityFrameworkCore;

public class AqsDbContext : DbContext
{
    public AqsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AqsOrder> AqsOrders { get; set; }
    public DbSet<AqsLineItem> LineItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AqsOrder>().HasMany(o => o.LineItems).WithOne().HasForeignKey(li => li.TransactionId);
    }
}