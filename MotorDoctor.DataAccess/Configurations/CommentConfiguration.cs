using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(x => x.Text).IsRequired().HasMaxLength(512);
        builder.Property(x=>x.Rating).IsRequired();

        builder.ToTable(t => t.HasCheckConstraint("CK_Comment_Rating_Range", "[Rating] >= 0 AND [Rating] <= 5"));

    }
}
