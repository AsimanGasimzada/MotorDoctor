using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class CategoryCreateDto : IDto
{
    public IFormFile? Image { get; set; }
    public int? ParentId { get; set; }
    public List<CategoryDetailCreateDto> CategoryDetails { get; set; } = [];
}
