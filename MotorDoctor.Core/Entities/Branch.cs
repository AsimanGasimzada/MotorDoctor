namespace MotorDoctor.Core.Entities;

public class Branch : BaseEntity
{
    public string? ImagePath { get; set; }
    public string LocationPath { get; set; } = null!;
    public ICollection<BranchDetail> BranchDetails { get; set; } = [];

}
