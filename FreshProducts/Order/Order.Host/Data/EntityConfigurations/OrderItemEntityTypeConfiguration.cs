using Order.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Host.Data.EntityConfigurations;

public class OrderEntityConfiguration
    : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.ToTable("OrderItemEntity");

        builder
            .HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .IsRequired();

        builder.Property(cb => cb.Price)
            .IsRequired();

        builder.Property(cb => cb.OrderId)
            .IsRequired();

		builder.Property(x => x.Name)
            .HasMaxLength(65)
            .IsRequired();

		builder.Property(x => x.ProductId)
			.IsRequired();

		builder.Property(x => x.Amount)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .HasOne<OrderEntity>()
            .WithMany(order => order.Items)
            .HasForeignKey(item => item.OrderId);
	}
}