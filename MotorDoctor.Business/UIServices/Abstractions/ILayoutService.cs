using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.UIServices.Abstractions;

public interface ILayoutService
{
    Task<Dictionary<string, string>> GetSettingsAsync(Languages language= Languages.Azerbaijan);
    Task<List<AttendanceGetDto>> GetAttendancesAsync(Languages language = Languages.Azerbaijan);
}
