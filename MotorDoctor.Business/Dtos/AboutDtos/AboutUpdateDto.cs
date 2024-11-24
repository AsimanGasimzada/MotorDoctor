using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class AboutUpdateDto : IDto
{
    public int Id { get; set; }
    public int OrderNo { get; set; }
    public string? ImagePath { get; set; } = null!;
    public IFormFile? Image { get; set; } = null!;
    public List<AboutDetailCreateDto> AboutDetails { get; set; } = new List<AboutDetailCreateDto>();
}