namespace MotorDoctor.Business.Dtos;

public class CommentGetDto : IDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Text { get; set; } = null!;
    public int Rating { get; set; }
    public UserGetDto AppUser { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}


