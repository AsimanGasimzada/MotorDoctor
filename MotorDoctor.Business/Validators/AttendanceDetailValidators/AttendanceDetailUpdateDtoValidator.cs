using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class AttendanceDetailUpdateDtoValidator : AbstractValidator<AttedanceDetailUpdateDto>
{
    public AttendanceDetailUpdateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(512);
    }
}
