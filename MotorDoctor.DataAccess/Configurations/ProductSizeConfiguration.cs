using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
{
    public void Configure(EntityTypeBuilder<ProductSize> builder)
    {
        builder.Property(x => x.Count).IsRequired();
        builder.Property(x => x.Size).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(8,2)");

        builder.HasIndex(x => new { x.ProductId, x.Size }).IsUnique();
    }
}
