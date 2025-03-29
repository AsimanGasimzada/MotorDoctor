using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Business.UIServices.Abstractions;
using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.UIServices.Implementations;

internal class LayoutService : ILayoutService
{
    private readonly IAttendanceService _attendanceService;
    private readonly ISettingService _settingService;
    private readonly IBasketService _basketService;
    private readonly Languages _language;

    public LayoutService(IAttendanceService attendanceService, ISettingService settingService, IBasketService basketService, ILanguageService languageService)
    {
        _attendanceService = attendanceService;
        _settingService = settingService;
        _basketService = basketService;
        _language = languageService.SelectedLanguage;
    }

    public async Task<List<AttendanceGetDto>> GetAttendancesAsync(Languages language = Languages.Azerbaijan)
    {
        return await _attendanceService.GetAllAsync(language);
    }

    public async Task<BasketGetDto> GetBasketAsync()
    {
        return await _basketService.GetBasketAsync(_language);
    }

    public string GetSelectedLanguage()
    {
        if (_language == Languages.English)
            return "en";
        else if (_language == Languages.Russian)
            return "ru";

        return "az";

    }

    public async Task<Dictionary<string, string>> GetSettingsAsync(Languages language = Languages.Azerbaijan)
    {
        return await _settingService.GetSettingsWithDictionaryAsync(language);
    }
}
