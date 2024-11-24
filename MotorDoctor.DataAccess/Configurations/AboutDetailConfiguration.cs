using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MotorDoctor.DataAccess.Configurations;

internal class AboutDetailConfiguration : IEntityTypeConfiguration<AboutDetail>
{
    public void Configure(EntityTypeBuilder<AboutDetail> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(9196);

        builder.HasIndex(x => new { x.LanguageId, x.AboutId }).IsUnique();
    }
}
