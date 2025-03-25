using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    private readonly IProductService _productService;
    private readonly IBasketService _basketService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ErrorLocalizer _errorLocalizer;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly IEmailService _emailService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IPaymentService _paymentService;

    public OrderService(IOrderRepository repository, IMapper mapper, IBasketService basketService, IHttpContextAccessor contextAccessor, ErrorLocalizer errorLocalizer, IStatusService statusService, IProductService productService, IAuthService authService, IEmailService emailService, UserManager<AppUser> userManager, IPaymentService paymentService)
    {
        _repository = repository;
        _mapper = mapper;
        _basketService = basketService;
        _contextAccessor = contextAccessor;
        _errorLocalizer = errorLocalizer;
        _statusService = statusService;
        _productService = productService;
        _authService = authService;
        _emailService = emailService;
        _userManager = userManager;
        _paymentService = paymentService;
    }
    public async Task CancelOrderAsync(int id)
    {
        var order = await _repository.GetAsync(id, x => x.Include(x => x.OrderItems));

        if (order is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var status = await _statusService.GetLastAsync();

        if (order.StatusId == status.Id)
            return;

        order.StatusId = status.Id;

        _repository.Update(order);
        await _repository.SaveChangesAsync();


        foreach (var item in order.OrderItems)
            await _productService.DecreaseSalesCountAsync(item.ProductSizeId, item.Count);


    }


    public async Task RepairOrderAsync(int id)
    {
        var order = await _repository.GetAsync(id, x => x.Include(x => x.OrderItems));

        if (order is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var status = await _statusService.GetFirstAsync();

        if (order.StatusId == status.Id)
            return;

        order.StatusId = status.Id;

        _repository.Update(order);
        await _repository.SaveChangesAsync();


        foreach (var item in order.OrderItems)
            await _productService.IncreaseSalesCountAsync(item.ProductSizeId, item.Count);


    }

    public async Task<bool> CreateAsync(OrderCreateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var basket = await _basketService.GetBasketAsync();

        if (basket.Count is 0)
            throw new EmptyBasketException(_errorLocalizer.GetValue(nameof(EmptyBasketException)));

        dto.OrderItems = _mapper.Map<List<OrderItemCreateDto>>(basket.Items);

        var order = _mapper.Map<Order>(dto);

        order.TotalPrice = basket.Total;
        order.DiscountedTotalPrice = basket.DiscountedTotal;

        var status = await _statusService.GetFirstAsync();
        order.StatusId = status.Id;

        string userId = _getUserId()!;
        var user = await _userManager.FindByIdAsync(userId);

        var isExistPaymentType = Enum.GetNames(typeof(PaymentTypes)).Any(x => x == dto.PaymentType.ToString());

        if (!isExistPaymentType)
        {
            ModelState.AddModelError("PaymentType", _errorLocalizer.GetValue("InvalidPaymentType"));
            return false;
        }

        //if (user is null)
        //    throw new UnAuthorizedException();

        order.AppUserId = userId;

        await _repository.CreateAsync(order);
        await _repository.SaveChangesAsync();

        foreach (var item in dto.OrderItems)
            await _productService.IncreaseSalesCountAsync(item.ProductSizeId, item.Count);

        await _basketService.ClearBasketAsync();
        string emailBody = $@"
<!DOCTYPE html>
<html lang=""az"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Yeni Sifariş Bildirişi</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #ffffff;
            margin: 0;
            padding: 0;
            color: #000000;
        }}
        .email-container {{
            max-width: 600px;
            margin: 20px auto;
            background: #ffffff;
            border: 1px solid #000000;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            overflow: hidden;
        }}
        .header {{
            background-color: #000000;
            color: #ffffff;
            text-align: center;
            padding: 20px;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
            text-transform: uppercase;
        }}
        .content {{
            padding: 20px;
        }}
        .content p {{
            margin: 10px 0;
            line-height: 1.6;
        }}
        .content .order-details {{
            background: #f9f9f9;
            padding: 10px;
            border: 1px solid #000000;
            margin-top: 10px;
        }}
        .footer {{
            background-color: #f9f9f9;
            text-align: center;
            padding: 10px;
            font-size: 14px;
            color: #000000;
        }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <div class=""header"">
            <h1>MotorDoctor</h1>
        </div>
        <div class=""content"">
            <p>Hörmətli Admin,</p>
            <p>Vebsaytda yeni sifariş yerləşdirilib. Sifariş detalları aşağıdadır:</p>
            <div class=""order-details"">
                <p><strong>Müştərinin Adı:</strong> {user?.UserName}</p>
                <p><strong>E-poçt:</strong> {user?.Email}</p>
                <p><strong>Telefon:</strong> {dto.PhoneNumber}</p>
                <p><strong>Sifariş Tarixi:</strong> {order.CreatedAt.ToShortDateString()}</p>
                <p><a href=""https://motordoctor.az/admin/order"" style=""color: #000000; text-decoration: underline;"">Sifarişin detalları üçün keçid</a></p>
            </div>
            <p>Zəhmət olmasa, sifarişi nəzərdən keçirin və lazımi tədbirlər görün.</p>
        </div>
        <div class=""footer"">
            <p>Bu avtomatik göndərilən məktubdur. Zəhmət olmasa, cavab göndərməyin.</p>
        </div>
    </div>
</body>
</html>";


        //await _emailService.SendEmailAsync(new() { ToEmail = "admin@motordoctor.az", Subject = "Yeni sifariş", Body = emailBody });

        if (dto.PaymentType is PaymentTypes.Cart)
        {

            PaymentCreateDto paymentDto = new()
            {
                Description = "Motordoctor Odenis",
                Amount = order.DiscountedTotalPrice,
                OrderId = order.Id,
            };

            var responseDto = await _paymentService.CreateAsync(paymentDto);

            order.PaymentId = responseDto.Id;

            _repository.Update(order);
            await _repository.SaveChangesAsync();

            if (_contextAccessor.HttpContext is not null)
            {
                string paymentUrl = $"{responseDto.Order.HppUrl}?id={responseDto.Order.Id}&password={responseDto.Order.Password}";
                _contextAccessor.HttpContext.Response.Cookies.Append("paymentUrl", paymentUrl, new CookieOptions() { Expires = DateTime.UtcNow.AddMinutes(1) });
            }

        }

        return true;
    }


    public async Task NextOrderStatusAsync(int id)
    {
        var order = await _repository.GetAsync(id, x => x.Include(x => x.OrderItems));

        if (order is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        if (order.StatusId == 4)
            return;

        if (order.StatusId < 3)
            order.StatusId++;

        _repository.Update(order);
        await _repository.SaveChangesAsync();
    }

    public async Task PrevOrderStatusAsync(int id)
    {
        var order = await _repository.GetAsync(id, x => x.Include(x => x.OrderItems));

        if (order is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        if (order.StatusId == 4)
            return;

        if (order.StatusId > 1)
            order.StatusId--;

        _repository.Update(order);
        await _repository.SaveChangesAsync();
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
        var query = _repository.GetAll(_getIncludeFunc(language));

        var orders = await _repository.OrderByDescending(query, x => x.CreatedAt).ToListAsync();

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
        string? userId = _getUserId();

        var query = _repository.GetFilter(x => x.AppUserId == userId, _getIncludeFunc(language));

        var orders = await _repository.OrderByDescending(query, x => x.CreatedAt).ToListAsync();

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);

        return dtos;
    }

    public async Task<OrderGetDto> GetUserOrderAsync(int id, Languages language = Languages.Azerbaijan)
    {
        if (!_checkAuthorized())
            throw new UnAuthorizedException(_errorLocalizer.GetValue(nameof(UnAuthorizedException)));

        string userId = _getUserId()!;

        var order = await _repository.GetAsync(x => x.AppUserId == userId && x.Id == id, _getIncludeFunc(language));

        if (order is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));


        var dto = _mapper.Map<OrderGetDto>(order);

        return dto;
    }


    public async Task<OrderCreateDto> GetUserUnSubmitOrderAsync(Languages language = Languages.Azerbaijan)
    {
        var basket = await _basketService.GetBasketAsync();

        OrderCreateDto dto = new();

        if (_checkAuthorized())
        {
            var userId = _getUserId()!;

            var user = await _authService.GetUserAsync(userId);

            dto.Name = user.Name;
            dto.Surname = user.Surname;
        }

        dto.OrderItems = _mapper.Map<List<OrderItemCreateDto>>(basket.Items);
        dto.Total = basket.Total;
        dto.DiscountedTotal = basket.DiscountedTotal;


        return dto;
    }

    private string? _getUserId()
    {
        return _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
    }

    private bool _checkAuthorized()
    {
        return _contextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }


    private Func<IQueryable<Order>, IIncludableQueryable<Order, object>> _getIncludeFunc(Languages language)
    {
        LanguageHelper.CheckLanguageId(ref language);
        return x => x.Include(x => x.OrderItems).ThenInclude(x => x.ProductSize.Product.ProductDetails.Where(x => x.LanguageId == (int)language))
                            .Include(x => x.OrderItems).ThenInclude(x => x.ProductSize.Product.ProductImages)
                            .Include(x => x.Status.StatusDetails.Where(x => x.LanguageId == (int)language))
                            .Include(x => x.AppUser!)
                            .Include(x => x.Payment!);
    }

    public async Task AutoFillStaticDiscountedPrices()
    {
        var orders = await _repository.GetAll().ToListAsync();

        foreach (var order in orders)
        {
            order.DiscountedTotalPrice = order.TotalPrice;

            _repository.Update(order);
        }

        await _repository.SaveChangesAsync();
    }


    public async Task<List<SalesDataDto>> GetMonthlySalesWithYearAsync()
    {
        var orders = _repository.GetAll();

        var salesData = await orders
         .GroupBy(o => new { o.CreatedAt.Year, o.CreatedAt.Month })
         .Select(g => new SalesDataDto
         {
             Year = g.Key.Year,
             Month = g.Key.Month,
             TotalSales = g.Sum(o => o.DiscountedTotalPrice)
         })
         .ToListAsync();

        // Satış olmayan ayları sıfırla doldur
        var startDate = salesData.Min(s => new DateTime(s.Year, s.Month, 1));
        var endDate = salesData.Max(s => new DateTime(s.Year, s.Month, 1));

        var allMonths = Enumerable.Range(0, (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month + 1)
            .Select(offset => new DateTime(startDate.Year, startDate.Month, 1).AddMonths(offset))
            .Select(date => new SalesDataDto
            {
                Year = date.Year,
                Month = date.Month,
                TotalSales = 0 // Default olaraq 0
            })
            .ToList();

        foreach (var sale in salesData)
        {
            var match = allMonths.FirstOrDefault(m => m.Year == sale.Year && m.Month == sale.Month);
            if (match != null)
            {
                match.TotalSales = sale.TotalSales;
            }
        }

        return allMonths.OrderBy(x => x.Year).ThenBy(x => x.Month).ToList();
    }

    public async Task<CurrentMonthSalesDataDto> GetCurrentMonthsSalesAsync()
    {
        var orders = await _repository.GetFilter(x => x.CreatedAt.Month == DateTime.Now.Month && x.CreatedAt.Year == DateTime.Now.Year, include: x => x.Include(x => x.OrderItems)).ToListAsync();

        CurrentMonthSalesDataDto dto = new()
        {
            TotalSales = orders.Sum(x => x.DiscountedTotalPrice),
            OrderCount = orders.Count()
        };

        return dto;
    }

    public async Task<TopUserDto> GetTopUserOfCurrentMonthAsync()
    {
        var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        var orders = _repository.GetAll(include: x => x.Include(x => x.AppUser!));

        var topUser = await orders
            .Where(o => o.CreatedAt >= startOfMonth && o.CreatedAt <= endOfMonth && o.AppUserId != null)
            .GroupBy(o => new { o.AppUserId, o.AppUser!.Name, o.AppUser.Surname, o.AppUser!.PhoneNumber })
            .Select(g => new TopUserDto
            {
                Id = g.Key.AppUserId!,
                FullName = g.Key.Name + " " + g.Key.Surname,
                OrderCount = g.Count(),
                TotalSpent = g.Sum(o => o.TotalPrice),
                PhoneNumber = g.Key.PhoneNumber
            })
            .OrderByDescending(x => x.OrderCount)
            .ThenByDescending(x => x.TotalSpent)
            .FirstOrDefaultAsync();

        if (topUser is null)
            throw new NotFoundException();

        return topUser;
    }

    public async Task<bool> ConfirmPaymentAsync(int id)
    {
        var order = await _repository.GetAsync(x => !x.IsPaid && x.Id == id);

        if (order is null)
            throw new NotFoundException();

        order.IsPaid=true;
         
        _repository.Update(order);
        await _repository.SaveChangesAsync();

        return true;
    }

}
