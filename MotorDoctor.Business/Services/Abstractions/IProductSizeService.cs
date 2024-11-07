namespace MotorDoctor.Business.Services.Abstractions;

public interface IProductSizeService
{
    Task<bool> IsExistAsync(int id);
    Task<ProductSizeWithBasketGetDto?> GetAsync(int id);
}
