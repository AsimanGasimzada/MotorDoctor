using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class AttendanceUpdateDtoValidator : AbstractValidator<AttendanceUpdateDto>
{
    public AttendanceUpdateDtoValidator()
    {
        RuleFor(x => x.Image).NotEmpty();

        RuleForEach(x => x.AttendanceDetails).SetValidator(new AttendanceDetailUpdateDtoValidator());
    }
}
