using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.UIServices.Abstractions;

public interface IContactService
{
    Task<ContactDto> GetContactDtoAsync(Languages language = Languages.Azerbaijan);
    Task<bool> SendEmailAsync(ContactDto dto, ModelStateDictionary ModelState);
}
