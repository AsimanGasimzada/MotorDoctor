namespace MotorDoctor.Core.Entities;

public class Density : BaseAuditableEntity
{
    public string Value { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = [];
}
