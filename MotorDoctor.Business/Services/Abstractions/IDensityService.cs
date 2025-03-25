namespace MotorDoctor.Business.Services.Abstractions;
public interface IDensityService : IModifyService<DensityCreateDto, DensityUpdateDto>, IGetService<DensityGetDto>
{
    Task<bool> IsExistAsync(int id);
    Task<List<DensityRelationDto>> GetAllForProductAsync();

}
