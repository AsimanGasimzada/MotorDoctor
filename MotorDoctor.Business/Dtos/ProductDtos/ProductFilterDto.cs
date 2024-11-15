using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Dtos;

public class ProductFilterDto : IDto
{
    public List<int> CategoryIds { get; set; } = [];
    public List<int> BrandIds { get; set; } = [];
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public SortTypes SortType { get; set; } = SortTypes.Latest;
    public string? Search { get; set; }
}
