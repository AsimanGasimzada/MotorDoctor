using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
{
    public void Configure(EntityTypeBuilder<ProductSize> builder)
    {
        builder.Property(x => x.Count).IsRequired();
        builder.Property(x => x.Size).IsRequired(false).HasMaxLength(256);
        builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(8,2)");
        builder.Property(x => x.Discount).HasDefaultValue(0);

        builder.HasIndex(x => new { x.ProductId, x.Size }).IsUnique();

        builder.ToTable(t => t.HasCheckConstraint("CK_ProductSize_Discount_Range", "[Discount] >= 0 AND [Discount] <= 100"));

    }
}
