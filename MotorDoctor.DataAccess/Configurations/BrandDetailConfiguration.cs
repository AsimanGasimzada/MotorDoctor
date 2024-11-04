using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

public class BrandDetailConfiguration : IEntityTypeConfiguration<BrandDetail>
{
    public void Configure(EntityTypeBuilder<BrandDetail> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(512);

        builder.HasIndex(x => new { x.LanguageId, x.BrandId }).IsUnique();
    }
}
