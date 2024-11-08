using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class AttendanceUpdateDto : IDto
{
    public int Id { get; set; }
    public IFormFile? Image { get; set; }
    public string? ImagePath { get; set; }
    public List<AttedanceDetailUpdateDto> AttendanceDetails { get; set; } = [];
}
