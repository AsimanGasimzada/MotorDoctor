namespace MotorDoctor.Business.Services.Abstractions;

public interface IGetService<TGetDto>
    where TGetDto : IDto
{
    Task<TGetDto> GetAsync(int id);
    Task<List<TGetDto>> GetAllAsync();
}
