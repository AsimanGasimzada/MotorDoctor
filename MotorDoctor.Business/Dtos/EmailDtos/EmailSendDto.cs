using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MotorDoctor.Business.Dtos;

public class EmailSendDto
{
    [Required(ErrorMessage = "Göndəriləcək e-poçt sahəsi boş ola bilməz.")]
    [EmailAddress(ErrorMessage = "Düzgün e-poçt ünvanı daxil edin.")]
    public string ToEmail { get; set; } = null!;

    [Required(ErrorMessage = "Mövzu sahəsi boş ola bilməz.")]
    public string Subject { get; set; } = null!;

    [Required(ErrorMessage = "Mətn sahəsi boş ola bilməz.")]
    public string Body { get; set; } = null!;

    public List<IFormFile> Attachments { get; set; } = new();
}
