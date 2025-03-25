using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class AdvertisementUpdateDto : IDto
{
    public int Id { get; set; }
    public string? ImagePath { get; set; }
    public IFormFile? Image { get; set; }
    public string Url { get; set; } = null!;
    public bool ForMobile { get; set; } = false;
}
