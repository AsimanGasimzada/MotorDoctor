using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class AdvertisementCreateDto : IDto
{
    public IFormFile Image { get; set; } = null!;
    public string Url { get; set; } = null!;
    public bool ForMobile { get; set; } = false;
}
