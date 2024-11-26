using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class BranchDetailCreateDtoValidator : AbstractValidator<BranchDetailCreateDto>
{
    public BranchDetailCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(64);
        RuleFor(x => x.WorkHours).NotEmpty().MaximumLength(256);
        RuleFor(x => x.Location).NotEmpty().MaximumLength(256);
    }
}