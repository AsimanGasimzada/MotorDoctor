using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Localizers;
using MotorDoctor.DataAccess.Repositories.Abstractions;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MotorDoctor.Business.Services.Implementations;

internal class WishlistService : IWishlistService
{
    private readonly IWishlistItemRepository _repository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IProductSizeService _productSizeService;
    private readonly IProductService _productService;
    private readonly ErrorLocalizer _errorLocalizer;
    private const string WISHLIST_KEY = "MotorDoctorWishlist";

    public WishlistService(IWishlistItemRepository repository, IMapper mapper, IHttpContextAccessor contextAccessor, IProductSizeService productSizeService, ErrorLocalizer errorLocalizer, IProductService productService)
    {
        _repository = repository;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
        _productSizeService = productSizeService;
        _errorLocalizer = errorLocalizer;
        _productService = productService;
    }

    public async Task<bool> ToggleToWishlistAsync(int id)
    {
        var isExistProductSize = await _productSizeService.IsExistAsync(id);

        if (isExistProductSize is false)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        if (_checkAuthorized())
        {
            string userId = _getUserId();

            var existWishlistItem = await _repository.GetAsync(x => x.ProductSizeId == id && x.AppUserId == userId);

            if (existWishlistItem is { })
            {

                _repository.Delete(existWishlistItem);
                await _repository.SaveChangesAsync();

                return true;
            }

            WishlistItem wishlistItem = new() { AppUserId = userId, ProductSizeId = id };

            await _repository.CreateAsync(wishlistItem);
            await _repository.SaveChangesAsync();

            return true;
        }
        else
        {
            var wishlistItems = _readWishlistFromCookie();

            var existItem = wishlistItems.FirstOrDefault(x => x.ProductSizeId == id);

            if (existItem is { })
                wishlistItems.Remove(existItem);
            else
            {
                WishlistItem newWishlistItem = new() { ProductSizeId = id };

                wishlistItems.Add(newWishlistItem);
            }

            _writeWishlistInCookie(wishlistItems);

            return true;
        }
    }


    public async Task<List<WishlistItemGetDto>> GetWishlistAsync(Languages language = Languages.Azerbaijan)
    {
        if (_checkAuthorized())
        {
            var userId = _getUserId();

            LanguageHelper.CheckLanguageId(ref language);

            var wishlistItems = await _repository.GetFilter(x => x.AppUserId == userId,
                           x => x.Include(x => x.ProductSize).ThenInclude(x => x.Product)
                                      .ThenInclude(x => x.ProductDetails.Where(x => x.LanguageId == (int)language))
                                      .Include(x => x.ProductSize.Product.ProductImages)
                                      .Include(x => x.ProductSize.Product.Category.CategoryDetails.Where(x => x.LanguageId == (int)language))).ToListAsync();

            var dtos = _mapper.Map<List<WishlistItemGetDto>>(wishlistItems);

            return dtos;
        }
        else
        {
            var wishlistItems = _readWishlistFromCookie();

            var dtos = _mapper.Map<List<WishlistItemGetDto>>(wishlistItems);

            foreach (var dto in dtos)
            {
                var productSize = await _productSizeService.GetAsync(dto.ProductSizeId, language);

                if (productSize is null)
                {
                    dtos.Remove(dto);
                    continue;
                }

                dto.ProductSize = productSize;
            }

            return dtos;
        }

    }



    public async Task<bool> IsExistAsync(int id)
    {
        var wishlist = await GetWishlistAsync();

        var isExist = wishlist.Any(x => x.ProductSizeId == id);

        return isExist;
    }

    public async Task<bool> IsExistByProductIdAsync(int id)
    {
        var wishlist = await GetWishlistAsync();

        var isExist = wishlist.Any(x => x.ProductSize.ProductId == id);

        return isExist;
    }

    private List<WishlistItem> _readWishlistFromCookie()
    {
        string json = _contextAccessor.HttpContext?.Request.Cookies[WISHLIST_KEY] ?? "";

        var WishlistItems = JsonConvert.DeserializeObject<List<WishlistItem>>(json) ?? new();
        return WishlistItems;
    }

    private void _writeWishlistInCookie(List<WishlistItem> WishlistItems)
    {
        string newJson = JsonConvert.SerializeObject(WishlistItems);

        _contextAccessor.HttpContext?.Response.Cookies.Append(WISHLIST_KEY, newJson);
    }


    private string _getUserId()
    {
        return _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
    }


    private bool _checkAuthorized()
    {
        return _contextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }


}
