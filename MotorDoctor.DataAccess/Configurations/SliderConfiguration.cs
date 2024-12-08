using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class SliderConfiguration : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.Property(x => x.ButtonPath).IsRequired(false).HasMaxLength(256);
        builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(256);
    }
}
