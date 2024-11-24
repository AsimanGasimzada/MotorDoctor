using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MotorDoctor.DataAccess.Configurations;

internal class AboutConfiguration : IEntityTypeConfiguration<About>
{
    public void Configure(EntityTypeBuilder<About> builder)
    {
        builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(256);
        builder.Property(x => x.OrderNo).IsRequired();
    }
}
