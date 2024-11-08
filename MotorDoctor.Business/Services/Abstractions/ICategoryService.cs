namespace MotorDoctor.Business.Services.Abstractions;

public interface ICategoryService : IModifyService<CategoryCreateDto, CategoryUpdateDto>, IGetServiceWithLanguage<CategoryGetDto>
{
    Task<List<CategoryRelationDto>> GetAllForProductAsync();
    Task<bool> IsExistAsync(int id);
    Task<CategoryCreateDto> GetCreateDtoAsync();
    Task<CategoryCreateDto> GetCreateDtoAsync(CategoryCreateDto dto);
    Task<CategoryUpdateDto> GetUpdatedDtoAsync(CategoryUpdateDto dto);
}
