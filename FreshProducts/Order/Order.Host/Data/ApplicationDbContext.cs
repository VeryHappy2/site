using Order.Host.Data.Entities;
using Order.Host.Data.EntityConfigurations;

namespace Order.Host.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<OrderEntity> OrderEntity { get; set; }
	public DbSet<OrderItemEntity> OrderItemEntity { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrderEntityConfiguration());
        builder.UseHiLo();
    }
}
