using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Business.UIServices.Abstractions;
using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.UIServices.Implementations;

internal class HomeService : IHomeService
{
    private readonly IAttendanceService _attendanceService;
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    private readonly ISliderService _sliderService;
    private readonly IProductService _productService;
    private readonly IAboutService _aboutService;
    public HomeService(IAttendanceService attendanceService, IBrandService brandService, ICategoryService categoryService, ISliderService sliderService, IProductService productService, IAboutService aboutService)
    {
        _attendanceService = attendanceService;
        _brandService = brandService;
        _categoryService = categoryService;
        _sliderService = sliderService;
        _productService = productService;
        _aboutService = aboutService;
    }

    public async Task<HomeDto> GetHomeDtoAsync(Languages language = Languages.Azerbaijan)
    {
        var attendances = await _attendanceService.GetAllAsync(language);
        var brands = await _brandService.GetAllAsync(language);
        var categories = await _categoryService.GetAllAsync(language);
        var parentCategories = await _categoryService.GetParentCategoriesAsync(language);
        var sliders = await _sliderService.GetAllAsync(language);
        var products = await _productService.GetAllAsync(language);
        var bestSellerProducts = await _productService.GetBestProductsAsync(language);
        var featuredCategories = await _categoryService.GetFeaturedCategoriesAsync(language);
        var featuredParentCategories = (await _categoryService.GetParentCategoriesAsync(language)).Take(5).ToList();
        var about = (await _aboutService.GetAllAsync(language)).FirstOrDefault();

        HomeDto dto = new HomeDto()
        {
            Attendances = attendances,
            Brands = brands,
            ParentCategories = parentCategories,
            Products = products,
            Sliders = sliders,
            BestSellerProducts = bestSellerProducts,
            FeaturedCategories = featuredCategories,
            FeaturedParentCategories = featuredParentCategories,
            About = about
        };

        return dto;
    }
}
