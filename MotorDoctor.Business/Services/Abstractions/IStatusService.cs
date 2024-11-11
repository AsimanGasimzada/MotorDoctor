using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Abstractions;

public interface IStatusService : IGetServiceWithLanguage<StatusGetDto>
{
    Task<StatusGetDto> GetFirstAsync(Languages language = Languages.Azerbaijan);
}
