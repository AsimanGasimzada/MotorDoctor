namespace MotorDoctor.Business.Services.Abstractions;

public interface IBrandService : IModifyService<BrandCreateDto, BrandUpdateDto>, IGetServiceWithLanguage<BrandGetDto>
{
    Task<List<BrandRelationDto>> GetAllForProductAsync();
    Task<bool> IsExistAsync(int id);

}
