using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Business.Extensions;

namespace MotorDoctor.Presentation.Controllers;

public class ShopController : Controller
{
    private readonly IProductService _productService;
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;

    public ShopController(IProductService productService, IBrandService brandService, ICategoryService categoryService)
    {
        _productService = productService;
        _brandService = brandService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {

        var shopFilterDto = await _productService.GetAllWithPageAsync(null, Constants.SelectedLanguage, page);
        shopFilterDto.Categories = await _categoryService.GetAllForProductAsync();
        shopFilterDto.Brands = await _brandService.GetAllForProductAsync();

        return View(shopFilterDto);
    }
    [HttpPost]
    public async Task<IActionResult> Index(ShopFilterDto dto, int page = 1)
    {

        var shopFilterDto = await _productService.GetAllWithPageAsync(dto.ProductFilterDto, Constants.SelectedLanguage, page);
        shopFilterDto.Categories = await _categoryService.GetAllForProductAsync();
        shopFilterDto.Brands = await _brandService.GetAllForProductAsync();

        return View(shopFilterDto);
    }
}
