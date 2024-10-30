using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class SliderCreateDto : IDto
{
    public IFormFile Image { get; set; } = null!;
    public List<SliderDetailCreateDto> SliderDetails { get; set; } = [];
}
