using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class BranchUpdateDtoValidator : AbstractValidator<BranchUpdateDto>
{
    public BranchUpdateDtoValidator()
    {
        RuleForEach(x => x.BranchDetails).SetValidator(new BranchDetailUpdateDtoValidator());
    }
}
