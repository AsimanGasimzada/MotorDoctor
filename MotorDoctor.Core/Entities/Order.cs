namespace MotorDoctor.Core.Entities;

public class Order : BaseAuditableEntity
{
    public AppUser? AppUser { get; set; } = null!;
    public string? AppUserId { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
