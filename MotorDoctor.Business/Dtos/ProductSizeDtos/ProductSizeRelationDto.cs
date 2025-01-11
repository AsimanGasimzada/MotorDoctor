namespace MotorDoctor.Business.Dtos;

public class ProductSizeRelationDto : IDto
{
    public string Size { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int Count { get; set; }
    public int ProductId { get; set; }
    public ProductGetDto Product { get; set; } = null!;
}


