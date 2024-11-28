using FluentValidation;

namespace MotorDoctor.Business.Validators;

public class AttendanceUpdateDtoValidator : AbstractValidator<AttendanceUpdateDto>
{
    public AttendanceUpdateDtoValidator()
    {
        RuleForEach(x => x.AttendanceDetails).SetValidator(new AttendanceDetailUpdateDtoValidator());
    }
}
