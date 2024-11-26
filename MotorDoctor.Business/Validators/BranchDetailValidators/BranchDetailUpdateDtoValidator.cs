using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class BranchDetailUpdateDtoValidator : AbstractValidator<BranchDetailUpdateDto>
{
    public BranchDetailUpdateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(64);
        RuleFor(x => x.WorkHours).NotEmpty().MaximumLength(256);
        RuleFor(x => x.Location).NotEmpty().MaximumLength(256);
    }
}
