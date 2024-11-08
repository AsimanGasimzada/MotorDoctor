using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class SubscriberEmailDto : IDto
{
    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public List<IFormFile>? Attachments { get; set; } = new();
}