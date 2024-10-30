using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Abstractions;

public interface ISettingService : IModifyService<SettingCreateDto, SettingUpdateDto>, IGetServiceWithLanguage<SettingGetDto>
{
    Task<Dictionary<string, string>> GetSettingsWithDictionaryAsync(Languages language);
}
