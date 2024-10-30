using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.ParentId).IsRequired(false);
        builder.Property(x => x.ImagePath).IsRequired(false).HasMaxLength(256);

        //builder.HasMany(x => x.Children)
        //    .WithOne(x => x.Parent)
        //      .HasForeignKey(x => x.ParentId)
        //      .OnDelete(DeleteBehavior.SetNull)
        //      .IsRequired(false);


    }
}
