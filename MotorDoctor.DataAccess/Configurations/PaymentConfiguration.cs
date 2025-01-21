using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotorDoctor.DataAccess.Configurations;

internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(x => x.Description).IsRequired(false).HasMaxLength(256);

        builder.HasOne(x => x.Order).WithOne(x => x.Payment)
            .HasForeignKey<Order>(x => x.PaymentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
