namespace MotorDoctor.Business.Dtos;

public class ProductSizeCreateDto : IDto
{
    public string Size { get; set; } = null!;
    public decimal Price { get; set; }
    public int Count { get; set; }
}


