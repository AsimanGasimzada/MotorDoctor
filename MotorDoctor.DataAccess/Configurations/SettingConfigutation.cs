using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class SettingConfigutation : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.Property(x => x.Key).IsRequired().HasMaxLength(64);

        builder.HasIndex(x => x.Key).IsUnique();
    }
}
