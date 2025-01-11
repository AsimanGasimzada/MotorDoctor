namespace MotorDoctor.Business.Dtos;

public class AdvertisementGetDto : IDto
{
    public int Id { get; set; }
    public string ImagePath { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int ViewCount { get; set; }
}
