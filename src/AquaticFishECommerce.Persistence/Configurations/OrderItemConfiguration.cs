using AquaticFishECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaticFishECommerce.Persistence.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Price)
                .HasPrecision(18, 2);

            builder.Property(oi => oi.Discount)
                .HasPrecision(5, 2);

            builder.Property(oi => oi.Quantity)
                .IsRequired();

            builder.Property(oi => oi.OrderStatus)
                .IsRequired();

            builder.Property(oi => oi.CancelledAt)
                .IsRequired(false);

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}