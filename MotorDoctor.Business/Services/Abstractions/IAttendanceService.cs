namespace MotorDoctor.Business.Services.Abstractions;

public interface IAttendanceService:IModifyService<AttendanceCreateDto, AttendanceUpdateDto>,IGetServiceWithLanguage<AttendanceGetDto>
{
}
