using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Abstractions;

public interface IWishlistService
{
    Task<bool> ToggleToWishlistAsync(int id);
    Task<bool> IsExistAsync(int id);
    Task<bool> IsExistByProductIdAsync(int id);
    Task<List<WishlistItemGetDto>> GetWishlistAsync(Languages language = Languages.Azerbaijan);
}
