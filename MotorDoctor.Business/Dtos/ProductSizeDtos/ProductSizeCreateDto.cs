using System.ComponentModel.DataAnnotations;

namespace MotorDoctor.Business.Dtos;

public class ProductSizeCreateDto : IDto
{
    public string? Size { get; set; }
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    [DataType(DataType.Currency)]
    [Range(0, 100)]
    public decimal Discount { get; set; }
    public int Count { get; set; }
}


