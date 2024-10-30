using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.DataAccess.Configurations;

public class SettingDetailConfiguration : IEntityTypeConfiguration<SettingDetail>
{
    public void Configure(EntityTypeBuilder<SettingDetail> builder)
    {
        builder.Property(x => x.Value).IsRequired().HasMaxLength(1024);

        builder.HasIndex(x => new { x.LanguageId, x.SettingId }).IsUnique();
    }
}