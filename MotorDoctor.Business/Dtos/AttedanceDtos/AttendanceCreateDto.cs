using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class AttendanceCreateDto : IDto
{
    public IFormFile Image { get; set; } = null!;
    public List<AttedanceDetailCreateDto> AttendanceDetails { get; set; } = [];
}
