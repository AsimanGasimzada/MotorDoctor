using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.Services.Implementations;

internal class DashboardService : IDashboardService
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IAdvertisementService _advertisementService;


    public DashboardService(IOrderService orderService, IProductService productService, UserManager<AppUser> userManager, IAdvertisementService advertisementService)
    {
        _orderService = orderService;
        _productService = productService;
        _userManager = userManager;
        _advertisementService = advertisementService;
    }

    public async Task<DashboardGetDto> GetDashboardAsync()
    {
        var salesDatas = await _orderService.GetMonthlySalesWithYearAsync();
        var currentMonthSalesData = await _orderService.GetCurrentMonthsSalesAsync();
        var bestProducts = await _productService.GetBestProductsAsync();
        var topUser = await _orderService.GetTopUserOfCurrentMonthAsync();
        var userCount = await _userManager.Users.CountAsync();
        var productCount = await _productService.GetAllProductCount();
        var advertisementViewCount = await _advertisementService.GetAllAdvertisementViewCount();

        DashboardGetDto dto = new()
        {
            BestSellerProducts = bestProducts,
            CurrentMonthSales = currentMonthSalesData,
            SalesData = salesDatas,
            BestUser = topUser,
            UserCount = userCount,
            ProductCount = productCount,
            AdvertisementViewCount = advertisementViewCount,
        };

        return dto;
    }
}
