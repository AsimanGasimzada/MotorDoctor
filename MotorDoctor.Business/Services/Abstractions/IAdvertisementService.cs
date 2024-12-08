namespace MotorDoctor.Business.Services.Abstractions;

public interface IAdvertisementService : IGetService<AdvertisementGetDto>, IModifyService<AdvertisementCreateDto, AdvertisementUpdateDto>
{
}

