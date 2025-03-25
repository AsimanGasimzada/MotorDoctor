using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Street).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Region).IsRequired().HasMaxLength(64);
        builder.Property(x => x.City).IsRequired().HasMaxLength(64);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(64);

        builder.HasOne(x=>x.Payment).WithOne(x=>x.Order).OnDelete(DeleteBehavior.Cascade);
    }
}
