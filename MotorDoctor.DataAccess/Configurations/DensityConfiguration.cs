using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

public class DensityConfiguration : IEntityTypeConfiguration<Density>
{
    public void Configure(EntityTypeBuilder<Density> builder)
    {
        builder.Property(x => x.Value).IsRequired().HasMaxLength(128);
    }
}
