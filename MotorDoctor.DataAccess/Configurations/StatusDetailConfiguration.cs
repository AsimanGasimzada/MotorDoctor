using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class StatusDetailConfiguration : IEntityTypeConfiguration<StatusDetail>
{
    public void Configure(EntityTypeBuilder<StatusDetail> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(64);

        builder.HasIndex(x => new { x.LanguageId, x.StatusId }).IsUnique();
    }
}