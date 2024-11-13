using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Abstractions;

public interface IWishlistService
{
    Task<bool> ToggleToWishlistAsync(int id);
    Task<List<WishlistItemGetDto>> GetWishlistAsync(Languages language = Languages.Azerbaijan);
}
