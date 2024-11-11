using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Abstractions;

public interface IProductSizeService
{
    Task<bool> IsExistAsync(int id);
    Task<ProductSizeRelationDto?> GetAsync(int id, Languages language = Languages.Azerbaijan);
}
