﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Localizers;
using MotorDoctor.DataAccess.Repositories.Abstractions;
using System.Security.Claims;

namespace MotorDoctor.Business.Services.Implementations;

internal class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IStatusService _statusService;
    private readonly IBasketService _basketService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ErrorLocalizer _errorLocalizer;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repository, IMapper mapper, IBasketService basketService, IHttpContextAccessor contextAccessor, ErrorLocalizer errorLocalizer, IStatusService statusService)
    {
        _repository = repository;
        _mapper = mapper;
        _basketService = basketService;
        _contextAccessor = contextAccessor;
        _errorLocalizer = errorLocalizer;
        _statusService = statusService;
    }

    public async Task<bool> CreateAsync(OrderCreateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        if (!_checkAuthorized())
            throw new UnAuthorizedException(_errorLocalizer.GetValue(nameof(UnAuthorizedException)));

        var basket = await _basketService.GetBasketAsync();

        if (basket.Count is 0)
            throw new EmptyBasketException(_errorLocalizer.GetValue(nameof(EmptyBasketException)));

        dto.OrderItems = _mapper.Map<List<OrderItemCreateDto>>(basket);

        var order = _mapper.Map<Order>(dto);

        var status = await _statusService.GetFirstAsync();
        order.StatusId = status.Id;

        string userId = _getUserId();
        order.AppUserId= userId;

        await _repository.CreateAsync(order);
        await _repository.SaveChangesAsync();


        await _basketService.ClearBasketAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _repository.GetAsync(id);

        if (order is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        _repository.Delete(order);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<OrderGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
    {
        var orders = await _repository.GetAll(_getIncludeFunc(language)).ToListAsync();

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);

        return dtos;
    }

    public async Task<OrderGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan)
    {
        var order = await _repository.GetAsync(id, _getIncludeFunc(language));

        if (order is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var dto = _mapper.Map<OrderGetDto>(order);

        return dto;
    }

    public async Task<OrderUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var order = await _repository.GetAsync(id, _getIncludeFunc(Languages.Azerbaijan));

        if (order is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var dto = _mapper.Map<OrderUpdateDto>(order);

        return dto;
    }

    public async Task<bool> UpdateAsync(OrderUpdateDto dto, ModelStateDictionary ModelState) //bug
    {
        if (ModelState.IsValid)
            return false;

        var existOrder = await _repository.GetAsync(dto.Id, _getIncludeFunc(Languages.Azerbaijan));

        if (existOrder is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        existOrder = _mapper.Map(dto, existOrder);

        _repository.Update(existOrder);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<List<OrderGetDto>> GetUserOrdersAsync(Languages language = Languages.Azerbaijan)
    {
        if (!_checkAuthorized())
            throw new UnAuthorizedException(_errorLocalizer.GetValue(nameof(UnAuthorizedException)));

        string userId = _getUserId();

        var orders = await _repository.GetFilter(x => x.AppUserId == userId, _getIncludeFunc(language)).ToListAsync();

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);

        return dtos;
    }

    public async Task<OrderGetDto> GetUserOrderAsync(int id, Languages language = Languages.Azerbaijan)
    {
        if (!_checkAuthorized())
            throw new UnAuthorizedException(_errorLocalizer.GetValue(nameof(UnAuthorizedException)));

        string userId = _getUserId();

        var order = await _repository.GetAsync(x => x.AppUserId == userId && x.Id == id, _getIncludeFunc(language));

        if (order is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));


        var dto = _mapper.Map<OrderGetDto>(order);

        return dto;
    }



    private string _getUserId()
    {
        return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
    }

    private bool _checkAuthorized()
    {
        return _contextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false;
    }


    private Func<IQueryable<Order>, IIncludableQueryable<Order, object>> _getIncludeFunc(Languages language)
    {
        LanguageHelper.CheckLanguageId(ref language);
        return x => x.Include(x => x.OrderItems).ThenInclude(x => x.ProductSize)
                            .Include(x => x.Status);
    }
}