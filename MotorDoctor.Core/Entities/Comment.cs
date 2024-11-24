namespace MotorDoctor.Core.Entities;

public class Comment : BaseAuditableEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
    public string Text { get; set; } = null!;
    public int Rating { get; set; }
}
