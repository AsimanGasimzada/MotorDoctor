using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Abstractions;

public interface IProductService : IModifyService<ProductCreateDto, ProductUpdateDto>, IGetServiceWithLanguage<ProductGetDto>
{
    Task<ProductCreateDto> GetCreatedDtoAsync();
    Task<ProductCreateDto> GetCreatedDtoAsync(ProductCreateDto dto);
    Task<ProductUpdateDto> GetUpdatedDtoAsync(ProductUpdateDto dto);
    Task<PaginateDto<ProductGetDto>> GetAllWithPageAsync(Languages language = Languages.Azerbaijan, int page = 1);
}
