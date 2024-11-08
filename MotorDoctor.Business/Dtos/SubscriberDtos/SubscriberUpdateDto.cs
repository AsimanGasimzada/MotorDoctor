using System.ComponentModel.DataAnnotations;

namespace MotorDoctor.Business.Dtos;

public class SubscriberUpdateDto : IDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
}
