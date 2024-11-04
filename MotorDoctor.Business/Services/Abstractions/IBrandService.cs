namespace MotorDoctor.Business.Services.Abstractions;

public interface IBrandService : IModifyService<BrandCreateDto, BrandUpdateDto>, IGetServiceWithLanguage<BrandGetDto>
{
    Task<List<BrandForProductGetDto>> GetAllForProductAsync();
    Task<bool> IsExistAsync(int id);

}
