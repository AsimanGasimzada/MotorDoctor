namespace MotorDoctor.Business.Dtos;

public class CommentUpdateDto : IDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Text { get; set; } = null!;
    public int Rating { get; set; }
}


