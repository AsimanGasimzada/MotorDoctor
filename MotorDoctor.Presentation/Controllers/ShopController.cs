using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Business.Extensions;
using MotorDoctor.Core.Enum;
using MotorDoctor.Presentation.Extensions;
using Newtonsoft.Json;

namespace MotorDoctor.Presentation.Controllers;

public class ShopController : Controller
{
    private readonly IProductService _productService;
    private readonly IBrandService _brandService;
    private readonly IAdvertisementService _advertisementService;
    private readonly ICategoryService _categoryService;
    private readonly ICommentService _commentService;
    private readonly IDensityService _densityService;
    private readonly Languages _language;

    public ShopController(IProductService productService, IBrandService brandService, ICategoryService categoryService, ICommentService commentService, ILanguageService languageService, IAdvertisementService advertisementService, IDensityService densityService)
    {
        _productService = productService;
        _brandService = brandService;
        _categoryService = categoryService;
        _commentService = commentService;
        _language = languageService.SelectedLanguage;
        _advertisementService = advertisementService;
        _densityService = densityService;
    }

    public async Task<IActionResult> Index(int page = 1, int? categoryId = null, ShopFilterDto? dto = null)
    {
        if (dto is null)
            dto = new();

        if (categoryId is not null)
        {
            var category = await _categoryService.GetAsync((int)categoryId);

            dto.ProductFilterDto = new()
            {
                CategoryIds = category.Children?.Count > 0 ? category.Children.Select(x => x.Id).ToList() : [category.Id],
                MaxPrice = 2500
            };
        }


        var shopFilterDto = await _productService.GetAllWithPageAsync(dto.ProductFilterDto, _language, page);
        shopFilterDto.Categories = await _categoryService.GetParentCategoriesForFilterAsync();
        shopFilterDto.Brands = await _brandService.GetAllForProductAsync();
        shopFilterDto.Densities = await _densityService.GetAllForProductAsync();
        shopFilterDto.Advertisements = await _advertisementService.GetAllAsync();

        return View(shopFilterDto);
    }


    public async Task<IActionResult> FilterProducts(int page = 1, ShopFilterDto? dto = null)
    {
        if (dto is null)
            dto = new();

        var json = Request.Cookies["filterDto"] ?? "";

        var productFilterDto = JsonConvert.DeserializeObject<ProductFilterDto>(json) ?? new();

        dto.ProductFilterDto = productFilterDto;

        var shopFilterDto = await _productService.GetAllWithPageAsync(dto.ProductFilterDto, _language, page);
        shopFilterDto.Categories = await _categoryService.GetParentCategoriesForFilterAsync();
        shopFilterDto.Densities = await _densityService.GetAllForProductAsync();
        shopFilterDto.Brands = await _brandService.GetAllForProductAsync();
        shopFilterDto.Advertisements = await _advertisementService.GetAllAsync();

        return View("Index", shopFilterDto);
    }

    [HttpPost]
    public async Task<IActionResult> FilterProducts(ShopFilterDto dto, int page = 1)
    {
        var shopFilterDto = await _productService.GetAllWithPageAsync(dto.ProductFilterDto, _language, page);
        shopFilterDto.Categories = await _categoryService.GetParentCategoriesForFilterAsync();
        shopFilterDto.Densities = await _densityService.GetAllForProductAsync();
        shopFilterDto.Brands = await _brandService.GetAllForProductAsync();
        shopFilterDto.Advertisements = await _advertisementService.GetAllAsync();

        var json = JsonConvert.SerializeObject(shopFilterDto.ProductFilterDto);

        Response.Cookies.Append("filterDto", json);

        return View("Index", shopFilterDto);
    }



    public async Task<IActionResult> DeleteComment(int id)
    {
        await _commentService.DeleteAsync(id);

        string returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);
    }



    [HttpPost]
    public async Task<IActionResult> CreateComment(CommentCreateDto dto)
    {
        var result = await _commentService.CreateAsync(dto, ModelState);

        string returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);
    }


}
