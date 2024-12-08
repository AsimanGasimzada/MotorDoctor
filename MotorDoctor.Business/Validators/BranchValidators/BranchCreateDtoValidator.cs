using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class BranchCreateDtoValidator : AbstractValidator<BranchCreateDto>
{
    public BranchCreateDtoValidator()
    {
        RuleForEach(x => x.BranchDetails).SetValidator(new BranchDetailCreateDtoValidator());
        RuleFor(x => x.LocationPath).NotNull().MaximumLength(256);
    }
}