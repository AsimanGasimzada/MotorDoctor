using System.ComponentModel.DataAnnotations;

namespace MotorDoctor.Business.Dtos;

public class AboutDetailCreateDto : IDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int LanguageId { get; set; }
}
