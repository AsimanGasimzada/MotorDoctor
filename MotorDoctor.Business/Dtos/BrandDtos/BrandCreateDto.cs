using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class BrandCreateDto : IDto
{
    public IFormFile Image { get; set; } = null!;
    public List<BrandDetailCreateDto> BrandDetails { get; set; } = [];
}
