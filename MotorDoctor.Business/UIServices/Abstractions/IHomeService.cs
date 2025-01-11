using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.UIServices.Abstractions;

public interface IHomeService
{
    Task<HomeDto> GetHomeDtoAsync(Languages language = Languages.Azerbaijan);
    void ClearInMemoryCache();
}
