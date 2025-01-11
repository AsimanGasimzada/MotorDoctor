namespace MotorDoctor.Business.Services.Abstractions;

public interface IDashboardService
{
    Task<DashboardGetDto> GetDashboardAsync();
}
