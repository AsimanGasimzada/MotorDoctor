using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Enum;
using MotorDoctor.Presentation.Extensions;

namespace MotorDoctor.Presentation.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICommentService _commentService;
    private readonly ILanguageService _languageService;
    private readonly Languages _language;

    public ProductController(IProductService productService, ICommentService commentService, ILanguageService languageService)
    {
        _productService = productService;
        _commentService = commentService;
        _languageService = languageService;
        _language = _languageService.RenderSelectedLanguage();
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




}
