using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.DataAccess.Configurations;

public class SettingConfigutation : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.Property(x => x.Key).IsRequired().HasMaxLength(64);

        builder.HasIndex(x => x.Key).IsUnique();
    }
}
