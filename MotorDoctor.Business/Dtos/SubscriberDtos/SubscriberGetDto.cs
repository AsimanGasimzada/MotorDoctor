namespace MotorDoctor.Business.Dtos;

public class SubscriberGetDto : IDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
}
