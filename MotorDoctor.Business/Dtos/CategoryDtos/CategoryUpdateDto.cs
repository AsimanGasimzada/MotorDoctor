using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class CategoryUpdateDto : IDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public IFormFile? Image { get; set; }
    public string? ImagePath { get; set; }
    public List<CategoryDetailUpdateDto> CategoryDetails { get; set; } = [];
}
