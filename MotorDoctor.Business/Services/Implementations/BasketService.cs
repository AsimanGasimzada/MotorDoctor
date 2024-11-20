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

internal class BasketService : IBasketService
{
    private readonly IBasketItemRepository _repository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IProductSizeService _productSizeService;
    private readonly ErrorLocalizer _errorLocalizer;
    private const string BASKET_KEY = "MotorDoctorBasket";

    public BasketService(IBasketItemRepository repository, IMapper mapper, IHttpContextAccessor contextAccessor, IProductSizeService productSizeService, ErrorLocalizer errorLocalizer)
    {
        _repository = repository;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
        _productSizeService = productSizeService;
        _errorLocalizer = errorLocalizer;
    }

    public async Task<bool> AddToBasketAsync(int id, int count = 1)
    {
        var isExistProductSize = await _productSizeService.IsExistAsync(id);

        if (isExistProductSize is false)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));


        if (count < 1)
            count = 1;

        if (_checkAuthorized())
        {
            string userId = _getUserId();

            var existBasketItem = await _repository.GetAsync(x => x.ProductSizeId == id && x.AppUserId == userId);

            if (existBasketItem is { })
            {
                existBasketItem.Count += count;

                _repository.Update(existBasketItem);
                await _repository.SaveChangesAsync();

                return true;
            }

            BasketItem basketItem = new() { AppUserId = userId, ProductSizeId = id, Count = count };

            await _repository.CreateAsync(basketItem);
            await _repository.SaveChangesAsync();

            return true;
        }
        else
        {
            var basketItems = _readBasketFromCookie();

            var existItem = basketItems.FirstOrDefault(x => x.ProductSizeId == id);

            if (existItem is { })
                existItem.Count += count;
            else
            {
                BasketItem newBasketItem = new() { ProductSizeId = id, Count = count };

                basketItems.Add(newBasketItem);
            }

            _writeBasketInCookie(basketItems);

            return true;
        }
    }


    public async Task<bool> DecreaseToBasketAsync(int id)
    {
        var isExistProductSize = await _productSizeService.IsExistAsync(id);

        if (isExistProductSize is false)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        if (_checkAuthorized())
        {
            string userId = _getUserId();

            var existBasketItem = await _repository.GetAsync(x => x.ProductSizeId == id && x.AppUserId == userId);

            if (existBasketItem is null)
                throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

            if (existBasketItem.Count <= 1)
                return true;

            existBasketItem.Count--;

            _repository.Update(existBasketItem);
            await _repository.SaveChangesAsync();

            return true;

        }
        else
        {
            var basketItems = _readBasketFromCookie();

            var existItem = basketItems.FirstOrDefault(x => x.ProductSizeId == id);

            if (existItem is null)
                throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

            if (existItem.Count <= 1)
                return true;

            existItem.Count--;

            _writeBasketInCookie(basketItems);

            return true;
        }
    }

    public async Task RemoveToBasketAsync(int id)
    {
        var isExistProductSize = await _productSizeService.IsExistAsync(id);

        if (isExistProductSize is false)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        if (_checkAuthorized())
        {
            string userId = _getUserId();

            var existBasketItem = await _repository.GetAsync(x => x.ProductSizeId == id && x.AppUserId == userId);

            if (existBasketItem is null)
                throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

            _repository.Delete(existBasketItem);
            await _repository.SaveChangesAsync();
        }
        else
        {
            List<BasketItem> basketItems = _readBasketFromCookie();

            var existItem = basketItems.FirstOrDefault(x => x.ProductSizeId == id);

            if (existItem is null)
                throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

            basketItems.Remove(existItem);

            _writeBasketInCookie(basketItems);
        }
    }

    public async Task<BasketGetDto> GetBasketAsync(Languages language = Languages.Azerbaijan)
    {
        if (_checkAuthorized())
        {
            var userId = _getUserId();

            LanguageHelper.CheckLanguageId(ref language);

            var basketItems = await _repository.GetFilter(x => x.AppUserId == userId,
                           x => x.Include(x => x.ProductSize).ThenInclude(x => x.Product)
                                      .ThenInclude(x => x.ProductDetails.Where(x => x.LanguageId == (int)language))
                                      .Include(x => x.ProductSize.Product.Category.CategoryDetails.Where(x => x.LanguageId == (int)language))
                                      .Include(x => x.ProductSize.Product.ProductImages)).ToListAsync();

            var dtos = _mapper.Map<List<BasketItemGetDto>>(basketItems);

            decimal totalPrice = dtos.Sum(x => (x.Count * x.ProductSize.Price));
            var basketDto = new BasketGetDto()
            {
                Items = dtos,
                Count = dtos.Count,
                Subtotal = totalPrice,
                Total = totalPrice,
                Discount = 0,
            };

            return basketDto;
        }
        else
        {
            var basketItems = _readBasketFromCookie();

            var dtos = _mapper.Map<List<BasketItemGetDto>>(basketItems);

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

            decimal totalPrice = dtos.Sum(x => (x.Count * x.ProductSize.Price));
            var basketDto = new BasketGetDto()
            {
                Items = dtos,
                Count = dtos.Count,
                Subtotal = totalPrice,
                Total = totalPrice,
                Discount = 0,
            };

            return basketDto;
        }

    }

    public async Task ClearBasketAsync()
    {
        if (!_checkAuthorized())
            throw new UnAuthorizedException(_errorLocalizer.GetValue(nameof(UnAuthorizedException)));

        string userId = _getUserId();

        var basketItems = await _repository.GetFilter(x => x.AppUserId == userId).ToListAsync();

        foreach (var basketItem in basketItems)
        {
            _repository.Delete(basketItem);
        }

        await _repository.SaveChangesAsync();
    }


    private List<BasketItem> _readBasketFromCookie()
    {
        string json = _contextAccessor.HttpContext?.Request.Cookies[BASKET_KEY] ?? "";

        var basketItems = JsonConvert.DeserializeObject<List<BasketItem>>(json) ?? new();
        return basketItems;
    }

    private void _writeBasketInCookie(List<BasketItem> basketItems)
    {
        string newJson = JsonConvert.SerializeObject(basketItems);

        _contextAccessor.HttpContext?.Response.Cookies.Append(BASKET_KEY, newJson);
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