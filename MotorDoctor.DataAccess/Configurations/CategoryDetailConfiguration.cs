using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class CategoryDetailConfiguration : IEntityTypeConfiguration<CategoryDetail>
{
    public void Configure(EntityTypeBuilder<CategoryDetail> builder)
    {
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(32);
        builder.Property(x=>x.Description).IsRequired().HasMaxLength(512);

        builder.HasIndex(x=> new {x.LanguageId,x.CategoryId}).IsUnique();
    }
}