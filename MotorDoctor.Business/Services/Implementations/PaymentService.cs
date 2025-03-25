using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Repositories.Abstractions;
using Newtonsoft.Json;
using System.Text;

namespace MotorDoctor.Business.Services.Implementations;

internal class PaymentService : IPaymentService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _configuration;
    private readonly PaymentConfigurationDto _paymentConfigurationDto;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IUrlHelper _urlHelper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ILanguageService _languageService;
    private readonly IPaymentRepository _repository;

    public PaymentService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, IHttpContextAccessor contextAccessor, IHttpClientFactory httpClientFactory, ILanguageService languageService, IPaymentRepository repository)
    {
        _webHostEnvironment = webHostEnvironment;
        _configuration = configuration;
        _paymentConfigurationDto = _configuration.GetSection("PaymentSettings").Get<PaymentConfigurationDto>() ?? new();
        _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext ?? new());
        _contextAccessor = contextAccessor;
        _httpClientFactory = httpClientFactory;
        _languageService = languageService;
        _repository = repository;
    }

    public async Task<bool> CheckPaymentAsync(PaymentCheckDto dto)
    {
        var payment = await _repository.GetAsync(x => x.ConfirmToken == dto.Token && x.ReceptId == dto.ID && x.PaymentStatus == PaymentStatuses.Pending, include: x => x.Include(x => x.Order));

        if (payment is null)
            throw new NotFoundException();

        if (dto.STATUS == PaymentStatuses.FullyPaid)
            payment.Order.IsPaid = true;

        payment.PaymentStatus = dto.STATUS;

        _repository.Update(payment);
        await _repository.SaveChangesAsync();

        return dto.STATUS == PaymentStatuses.FullyPaid;
    }

    public async Task<PaymentResponseDto> CreateAsync(PaymentCreateDto dto)
    {
        string confirmToken = Guid.NewGuid().ToString();

        UrlActionContext context = new()
        {
            Action = "CheckPayment",
            Controller = "Order",
            Values = new { Token = confirmToken },
            Protocol = _contextAccessor.HttpContext?.Request.Scheme
        };

        var redirectUrl = _urlHelper.Action(context);

        string selectedCulture = _languageService.GetSelectedCulture();

        string amount = dto.Amount.ToString().Replace(',', '.');


        string requestBody = $@"
    {{
        ""order"": {{
            ""typeRid"": ""Order_SMS"",
            ""amount"": {amount},
            ""currency"": ""AZN"",
            ""language"": ""{selectedCulture}"",
            ""description"": ""{dto.Description}"",
            ""hppRedirectUrl"": ""{redirectUrl}"",
            ""hppCofCapturePurposes"": [""Cit""]
        }}
    }}";

        var httpClient = _httpClientFactory.CreateClient("KapitalBankClient");
        var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_paymentConfigurationDto.Username}:{_paymentConfigurationDto.Password}"));
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);


        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(_paymentConfigurationDto.BaseUrl, content);

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.StatusCode.ToString());

        var responseContent = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<PaymentResponseDto>(responseContent) ?? new();

        Payment payment = new()
        {
            Amount = dto.Amount,
            Description = dto.Description,
            OrderId = dto.OrderId,
            ReceptId = result.Order.Id,
            SecretId = result.Order.Secret,
            PaymentStatus = PaymentStatuses.Pending,
            ConfirmToken = confirmToken
        };

        await _repository.CreateAsync(payment);
        await _repository.SaveChangesAsync();

        result.Id = payment.Id;

        return result;
    }
}
