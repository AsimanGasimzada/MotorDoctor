using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class SliderDetailConfiguration : IEntityTypeConfiguration<SliderDetail>
{
    public void Configure(EntityTypeBuilder<SliderDetail> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(128);
        builder.Property(x => x.ButtonTitle).IsRequired().HasMaxLength(32);

        builder.HasIndex(x => new { x.LanguageId, x.SliderId }).IsUnique();
    }
}