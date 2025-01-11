
namespace MotorDoctor.Business.Dtos;

public class HomeDto : IDto
{
    public List<SliderGetDto> Sliders { get; set; } = [];
    public List<AttendanceGetDto> Attendances { get; set; } = [];
    public List<ParentCategoryDto> ParentCategories { get; set; } = [];
    public List<CategoryFeatureGetDto> FeaturedCategories { get; set; } = [];
    public List<BestSellerProductGetDto> BestSellerProducts { get; set; } = [];
    public List<BrandGetDto> Brands { get; set; } = [];
    public List<ParentCategoryDto> FeaturedParentCategories = [];
    public AboutGetDto? About { get; set; }
}
