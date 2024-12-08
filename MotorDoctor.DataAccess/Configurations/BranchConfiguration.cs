using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.Property(x => x.ImagePath).IsRequired(false).HasMaxLength(256);
        builder.Property(x => x.LocationPath).IsRequired().HasMaxLength(256);
    }
}
