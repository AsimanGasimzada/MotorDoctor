using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class BranchUpdateDto : IDto
{
    public int Id { get; set; }
    public string? ImagePath { get; set; }
    public IFormFile? Image { get; set; }
    public string LocationPath { get; set; } = null!;
    public List<BranchDetailUpdateDto> BranchDetails { get; set; } = [];
}

