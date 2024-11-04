namespace MotorDoctor.Business.Services.Abstractions;

public interface ICategoryService : IModifyService<CategoryCreateDto, CategoryUpdateDto>, IGetServiceWithLanguage<CategoryGetDto>
{
    Task<List<CategoryForProductGetDto>> GetAllForProductAsync();
    Task<bool> IsExistAsync(int id);
}
