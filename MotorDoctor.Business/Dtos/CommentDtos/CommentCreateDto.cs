namespace MotorDoctor.Business.Dtos;

public class CommentCreateDto : IDto
{
    public int ProductId { get; set; }
    public string Text { get; set; } = null!;
    public int Rating { get; set; }
}
