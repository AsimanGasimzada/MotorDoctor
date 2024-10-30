using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(32);
        builder.Property(x=>x.Code).IsRequired().HasMaxLength(16);
        builder.Property(x=>x.ImagePath).IsRequired().HasMaxLength(256);
    }
}
