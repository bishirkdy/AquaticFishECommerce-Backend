using AquaticFishECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AquaticFishECommerce.Persistence.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImage");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ImageUrl)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.PublicId)
                .IsRequired();

            builder.Property(p => p.IsPrimary)
                .IsRequired()
                .HasDefaultValue(false);

            //Relationship for product
            builder.HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
