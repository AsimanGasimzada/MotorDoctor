namespace MotorDoctor.Business.Services.Abstractions;

public interface ISubscriberService : IModifyService<SubscriberCreateDto, SubscriberUpdateDto>, IGetService<SubscriberGetDto>
{
    Task<bool> SendEmailToSubscribres(SubscriberEmailDto dto, ModelStateDictionary ModelState);
}
