namespace MotorDoctor.Core.Entities;

public class About : BaseEntity
{
    public int OrderNo { get; set; }
    public string? ImagePath { get; set; } = null!;
    public ICollection<AboutDetail> AboutDetails { get; set; } = [];
}
