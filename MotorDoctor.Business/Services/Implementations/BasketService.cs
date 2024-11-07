using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.DataAccess.Localizers;
using MotorDoctor.DataAccess.Repositories.Abstractions;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MotorDoctor.Business.Services.Implementations;

public class BasketService : IBasketService
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

    public async Task<bool> AddToBasketAsync(int id)
    {
        var isExistProductSize = await _productSizeService.IsExistAsync(id);

        if (isExistProductSize is false)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        if (_checkAuthorized())
        {
            string userId = _getUserId();

            var existBasketItem = await _repository.GetAsync(x => x.ProductSizeId == id && x.AppUserId == userId);

            if (existBasketItem is { })
            {
                existBasketItem.Count++;

                _repository.Update(existBasketItem);
                await _repository.SaveChangesAsync();

                return true;
            }

            BasketItem basketItem = new() { AppUserId = userId, ProductSizeId = id, Count = 1 };

            await _repository.CreateAsync(basketItem);
            await _repository.SaveChangesAsync();

            return true;
        }
        else
        {
            var basketItems = _readBasketFromCookie();

            var existItem = basketItems.FirstOrDefault(x => x.Id == id);

            if (existItem is { })
                existItem.Count++;
            else
            {
                BasketItem newBasketItem = new() { ProductSizeId = id, Count = 1 };

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


            existBasketItem.Count--;

            _repository.Update(existBasketItem);
            await _repository.SaveChangesAsync();

            return true;

        }
        else
        {
            var basketItems = _readBasketFromCookie();

            var existItem = basketItems.FirstOrDefault(x => x.Id == id);

            if (existItem is null)
                throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));


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

            var existItem = basketItems.FirstOrDefault(x => x.Id == id);

            if (existItem is null)
                throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

            basketItems.Remove(existItem);

            _writeBasketInCookie(basketItems);
        }
    }

    public async Task<List<BasketItemGetDto>> GetBasketAsync()
    {
        if (_checkAuthorized())
        {
            var userId = _getUserId();

            var basketItems = await _repository.GetFilter(x => x.AppUserId == userId, x => x.Include(x => x.ProductSize)).ToListAsync();

            var dtos = _mapper.Map<List<BasketItemGetDto>>(basketItems);

            return dtos;
        }
        else
        {
            var basketItems = _readBasketFromCookie();

            var dtos = _mapper.Map<List<BasketItemGetDto>>(basketItems);

            foreach (var dto in dtos)
            {
                var productSize = await _productSizeService.GetAsync(dto.ProductSizeId);

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

    private List<BasketItem> _readBasketFromCookie()
    {
        string json = _contextAccessor.HttpContext.Request.Cookies[BASKET_KEY] ?? "";

        var basketItems = JsonConvert.DeserializeObject<List<BasketItem>>(json) ?? new();
        return basketItems;
    }

    private void _writeBasketInCookie(List<BasketItem> basketItems)
    {
        string newJson = JsonConvert.SerializeObject(basketItems);

        _contextAccessor.HttpContext.Response.Cookies.Append(BASKET_KEY, newJson);
    }


    private string _getUserId()
    {
        return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
    }


    private bool _checkAuthorized()
    {
        return _contextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false;
    }


}
