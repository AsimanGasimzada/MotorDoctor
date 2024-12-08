using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class AboutCreateDto : IDto
{
    public int OrderNo { get; set; }
    public IFormFile? Image { get; set; } = null!;
    public List<AboutDetailCreateDto> AboutDetails { get; set; } = new List<AboutDetailCreateDto>();
}