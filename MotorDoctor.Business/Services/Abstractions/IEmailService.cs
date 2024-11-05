namespace MotorDoctor.Business.Services.Abstractions;

public interface IEmailService
{
    Task SendEmailAsync(EmailSendDto dto);
}
