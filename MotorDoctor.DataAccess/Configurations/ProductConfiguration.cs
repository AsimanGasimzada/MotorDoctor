using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Code).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Slug).IsRequired().HasMaxLength(150);

        builder.HasIndex(x => x.Slug).IsUnique();

    }
}
