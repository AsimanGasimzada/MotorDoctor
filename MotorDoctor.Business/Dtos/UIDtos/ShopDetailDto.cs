namespace MotorDoctor.Business.Dtos;

public class ShopDetailDto : IDto
{
    public ProductGetDto Product { get; set; } = null!;
    public List<CommentGetDto> Comments { get; set; } = [];
    public bool IsAllowComment { get; set; } = false;
}
