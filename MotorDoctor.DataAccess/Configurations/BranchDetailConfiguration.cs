using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class BranchDetailConfiguration : IEntityTypeConfiguration<BranchDetail>
{
    public void Configure(EntityTypeBuilder<BranchDetail> builder)
    {
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(64);
        builder.Property(x=>x.Location).IsRequired().HasMaxLength(256);
        builder.Property(x=>x.WorkHours).IsRequired().HasMaxLength(256);
        builder.Property(x=>x.PhoneNumber).IsRequired().HasMaxLength(64);

        builder.HasIndex(x => new { x.LanguageId, x.BranchId }).IsUnique();
    }
}