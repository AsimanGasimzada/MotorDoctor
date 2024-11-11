using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class AttedanceDetailConfiguration : IEntityTypeConfiguration<AttendanceDetail>
{
    public void Configure(EntityTypeBuilder<AttendanceDetail> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Description).IsRequired(false).HasMaxLength(512);

        builder.HasIndex(x => new { x.LanguageId, x.AttedanceId }).IsUnique();
    }
}