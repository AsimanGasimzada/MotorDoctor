using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Abstractions;

public interface IBasketService
{
    Task<bool> AddToBasketAsync(int id);
    Task<bool> DecreaseToBasketAsync(int id);
    Task RemoveToBasketAsync(int id);
    Task<List<BasketItemGetDto>> GetBasketAsync(Languages language = Languages.Azerbaijan);
    Task ClearBasketAsync();
}
