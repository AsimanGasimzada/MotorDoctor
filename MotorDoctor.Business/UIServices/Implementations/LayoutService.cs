using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Business.UIServices.Abstractions;
using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.UIServices.Implementations;

internal class LayoutService : ILayoutService
{
    private readonly IAttendanceService _attendanceService;
    private readonly ISettingService _settingService;

    public LayoutService(IAttendanceService attendanceService, ISettingService settingService)
    {
        _attendanceService = attendanceService;
        _settingService = settingService;
    }

    public async Task<List<AttendanceGetDto>> GetAttendancesAsync(Languages language = Languages.Azerbaijan)
    {
        return await _attendanceService.GetAllAsync(language);
    }

    public string GetSelectedLanguage()
    {
        if (Constants.SelectedLanguage == Languages.English)
            return "en";
        else if (Constants.SelectedLanguage == Languages.Russian)
            return "ru";

        return "az";

    }

    public async Task<Dictionary<string, string>> GetSettingsAsync(Languages language = Languages.Azerbaijan)
    {
        return await _settingService.GetSettingsWithDictionaryAsync(language);
    }
}
