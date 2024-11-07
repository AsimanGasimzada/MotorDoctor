namespace MotorDoctor.Business.Dtos;

public class ProductSizeWithBasketGetDto : IDto
{
    public int Id { get; set; }
    public string Size { get; set; } = null!;
    public decimal Price { get; set; }
    public int Count { get; set; }
    public int ProductId { get; set; }
    public ProductGetDto Product { get; set; } = null!;
}


