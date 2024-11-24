namespace MotorDoctor.Business.Services.Abstractions;

public interface IAboutService : IModifyService<AboutCreateDto, AboutUpdateDto>, IGetServiceWithLanguage<AboutGetDto>
{
}
