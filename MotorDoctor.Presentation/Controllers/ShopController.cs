using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Business.Extensions;
using MotorDoctor.Core.Enum;
using MotorDoctor.Presentation.Extensions;

namespace MotorDoctor.Presentation.Controllers;

public class ShopController : Controller
{
    private readonly IProductService _productService;
    private readonly IBrandService _brandService;
    private readonly IAdvertisementService _advertisementService;
    private readonly ICategoryService _categoryService;
    private readonly ICommentService _commentService;
    private readonly ILanguageService _languageService;
    private readonly Languages _language;

    public ShopController(IProductService productService, IBrandService brandService, ICategoryService categoryService, ICommentService commentService, ILanguageService languageService, IAdvertisementService advertisementService)
    {
        _productService = productService;
        _brandService = brandService;
        _categoryService = categoryService;
        _commentService = commentService;
        _languageService = languageService;
        _language = _languageService.RenderSelectedLanguage();
        _advertisementService = advertisementService;
    }

    public async Task<IActionResult> Index(int page = 1, int? categoryId = null)
    {
        ProductFilterDto? filterDto = null;

        if (categoryId is not null)
        {
            var category = await _categoryService.GetAsync((int)categoryId);

            filterDto = new()
            {
                CategoryIds = category.Children?.Count > 0 ? category.Children.Select(x => x.Id).ToList() : [category.Id],
                MaxPrice = 2500
            };
        }


        var shopFilterDto = await _productService.GetAllWithPageAsync(filterDto, _language, page);
        shopFilterDto.Categories = await _categoryService.GetParentCategoriesForFilterAsync();
        shopFilterDto.Brands = await _brandService.GetAllForProductAsync();
        shopFilterDto.Advertisements = await _advertisementService.GetAllAsync();

        return View(shopFilterDto);
    }

    [HttpPost]
    public async Task<IActionResult> Index(ShopFilterDto dto, int page = 1)
    {

        var shopFilterDto = await _productService.GetAllWithPageAsync(dto.ProductFilterDto, _language, page);
        shopFilterDto.Categories = await _categoryService.GetParentCategoriesForFilterAsync();
        shopFilterDto.Brands = await _brandService.GetAllForProductAsync();
        shopFilterDto.Advertisements = await _advertisementService.GetAllAsync();

        return View(shopFilterDto);
    }
    public async Task<IActionResult> DeleteComment(int id)
    {
        await _commentService.DeleteAsync(id);

        string returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);
    }

    public async Task<IActionResult> Detail(string? slug, int id)
    {
        var product = await _productService.GetAsync(id, _language);
        var comments = await _commentService.GetProductCommentsAsync(id);
        var isAllowComment = await _commentService.CheckIsAllowCommentAsync(id);

        ShopDetailDto dto = new()
        {
            Product = product,
            Comments = comments,
            IsAllowComment = isAllowComment
        };

        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(CommentCreateDto dto)
    {
        var result = await _commentService.CreateAsync(dto, ModelState);

        string returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);
    }



  
}
