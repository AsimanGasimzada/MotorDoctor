namespace MotorDoctor.Business.Services.Abstractions;

public interface ICategoryService : IModifyService<CategoryCreateDto, CategoryUpdateDto>, IGetServiceWithLanguage<CategoryGetDto>
{
}
