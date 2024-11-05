namespace MotorDoctor.Business.Services.Abstractions;

public interface IBranchService : IModifyService<BranchCreateDto, BranchUpdateDto>, IGetServiceWithLanguage<BranchGetDto>
{
}
