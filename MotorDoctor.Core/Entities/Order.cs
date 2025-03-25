using MotorDoctor.Core.Enum;

namespace MotorDoctor.Core.Entities;

public class Order : BaseAuditableEntity
{
    public AppUser? AppUser { get; set; } = null!;
    public string? AppUserId { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public decimal DiscountedTotalPrice { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public PaymentTypes PaymentType { get; set; }
    public bool IsPaid { get; set; }
    public Payment? Payment { get; set; }
    public int? PaymentId { get; set; }
    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}