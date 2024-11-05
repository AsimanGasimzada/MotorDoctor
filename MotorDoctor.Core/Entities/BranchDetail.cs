namespace MotorDoctor.Core.Entities;

public class BranchDetail : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string WorkHours { get; set; } = null!;

    public int LanguageId { get; set; }
    public Language Language { get; set; } = null!;
    public int BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
}
