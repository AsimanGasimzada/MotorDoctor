
namespace MotorDoctor.Business.Dtos;

public class HomeDto : IDto
{
    public List<SliderGetDto> Sliders { get; set; } = [];
    public List<AttendanceGetDto> Attendances { get; set; } = [];
    public List<CategoryGetDto> ParentCategories { get; set; } = [];
    public List<CategoryGetDto> FeaturedCategories { get; set; } = [];
    public List<BestSellerProductGetDto> BestSellerProducts { get; set; } = [];
    public List<BrandGetDto> Brands { get; set; } = [];
    public List<ProductGetDto> Products { get; set; } = [];
    public List<ParentCategoryDto> FeaturedParentCategories = [];
}
