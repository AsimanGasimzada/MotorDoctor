using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class BranchCreateDto : IDto
{
    public IFormFile? Image { get; set; }
    public List<BranchDetailCreateDto> BranchDetails { get; set; } = [];
}

