using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class AttendanceDetailCreateDtoValidator : AbstractValidator<AttedanceDetailCreateDto>
{
    public AttendanceDetailCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(512);
    }
}
