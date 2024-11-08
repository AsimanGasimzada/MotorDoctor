namespace MotorDoctor.Business.Services.Abstractions;

public interface ISubscriberService : IModifyService<SubscriberCreateDto, SubscriberUpdateDto>, IGetService<SubscriberGetDto>
{
}
