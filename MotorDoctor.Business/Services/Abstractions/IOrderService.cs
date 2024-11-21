using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Abstractions;

public interface IOrderService : IModifyService<OrderCreateDto, OrderUpdateDto>, IGetServiceWithLanguage<OrderGetDto>
{
    Task<List<OrderGetDto>> GetUserOrdersAsync(Languages language = Languages.Azerbaijan);
    Task<OrderGetDto> GetUserOrderAsync(int id, Languages language = Languages.Azerbaijan);
    Task<OrderCreateDto> GetUserUnSubmitOrderAsync(Languages language = Languages.Azerbaijan);
    Task CancelOrderAsync(int id);
    Task RepairOrderAsync(int id);
    Task NextOrderStatusAsync(int id);
    Task PrevOrderStatusAsync(int id);
}
