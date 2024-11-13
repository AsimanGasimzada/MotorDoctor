using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Abstractions;

public interface ICategoryService : IModifyService<CategoryCreateDto, CategoryUpdateDto>, IGetServiceWithLanguage<CategoryGetDto>
{
    Task<List<CategoryRelationDto>> GetAllForProductAsync();
    Task<List<CategoryGetDto>> GetBestCategoriesAsync(Languages language = Languages.Azerbaijan);
    Task<List<CategoryGetDto>> GetFeaturedCategoriesAsync(Languages language = Languages.Azerbaijan);
    Task<List<ParentCategoryDto>> GetParentCategoriesAsync(Languages language= Languages.Azerbaijan);
    Task<bool> IsExistAsync(int id);
    Task<CategoryCreateDto> GetCreateDtoAsync();
    Task<CategoryCreateDto> GetCreateDtoAsync(CategoryCreateDto dto);
    Task<CategoryUpdateDto> GetUpdatedDtoAsync(CategoryUpdateDto dto);
}
