namespace MotorDoctor.Business.UIServices.Abstractions;

public interface IContactService
{
    Task<ContactDto> GetContactDtoAsync();
    Task<bool> SendEmailAsync(ContactDto dto, ModelStateDictionary ModelState);
}
