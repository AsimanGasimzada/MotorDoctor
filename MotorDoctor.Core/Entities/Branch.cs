namespace MotorDoctor.Core.Entities;

public class Branch : BaseEntity
{
    public string? ImagePath { get; set; }
    public ICollection<BranchDetail> BranchDetails { get; set; } = [];

}
