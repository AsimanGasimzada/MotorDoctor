using Microsoft.Extensions.Caching.Memory;
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
    private readonly IMemoryCache _memoryCache;
    public HomeService(IAttendanceService attendanceService, IBrandService brandService, ICategoryService categoryService, ISliderService sliderService, IProductService productService, IAboutService aboutService, IMemoryCache memoryCache)
    {
        _attendanceService = attendanceService;
        _brandService = brandService;
        _categoryService = categoryService;
        _sliderService = sliderService;
        _productService = productService;
        _aboutService = aboutService;
        _memoryCache = memoryCache;
    }

    public async Task<HomeDto> GetHomeDtoAsync(Languages language = Languages.Azerbaijan)
    {
        // Define a cache key based on the language
        string cacheKey = $"HomeDto_{language}";

        HomeDto dto = new();


        if (!_memoryCache.TryGetValue(cacheKey, out dto!))
        {
            var attendances = await _attendanceService.GetAllAsync(language);
            var brands = await _brandService.GetAllAsync(language);
            var parentCategories = await _categoryService.GetParentCategoriesAsync(language);
            var sliders = await _sliderService.GetAllAsync(language);
            var bestSellerProducts = await _productService.GetBestProductsAsync(language);
            var featuredCategories = await _categoryService.GetFeaturedCategoriesAsync(language);
            var featuredParentCategories = (await _categoryService.GetParentCategoriesAsync(language)).Take(5).ToList();
            var about = (await _aboutService.GetAllAsync(language)).FirstOrDefault();

            dto = new HomeDto()
            {
                Attendances = attendances,
                Brands = brands,
                ParentCategories = parentCategories,
                Sliders = sliders,
                BestSellerProducts = bestSellerProducts,
                FeaturedCategories = featuredCategories,
                FeaturedParentCategories = featuredParentCategories,
                About = about
            };

            _memoryCache.Set(cacheKey, dto, TimeSpan.FromHours(24));
        }

        return dto;
    }


    public void ClearInMemoryCache()
    {
        foreach (var language in Enum.GetNames(typeof(Languages)))
        {
            string cacheKey = $"HomeDto_{language}";
            _memoryCache.Remove(cacheKey);
        }
    }
}
