using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.Property(x => x.Path).IsRequired().HasMaxLength(256);
        builder.Property(x => x.IsMain).HasDefaultValue(false);

        builder.HasIndex(x => new { x.ProductId, x.IsMain })
                   .IsUnique()
                   .HasFilter("[IsMain] = 1");
    }
}
