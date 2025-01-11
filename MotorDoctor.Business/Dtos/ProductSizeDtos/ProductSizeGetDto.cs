namespace MotorDoctor.Business.Dtos;

public class ProductSizeGetDto : IDto
{
    public int Id { get; set; }
    public string Size { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Count { get; set; }
}
