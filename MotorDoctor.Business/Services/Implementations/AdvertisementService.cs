using Microsoft.EntityFrameworkCore;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

internal class AdvertisementService : IAdvertisementService
{
    private readonly IAdvertisementRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;

    public AdvertisementService(IAdvertisementRepository repository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<bool> CreateAsync(AdvertisementCreateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        if (!dto.Image.ValidateSize(2))
        {
            ModelState.AddModelError("Image", "Şəkilin ölçüsü 2mb dan artıq olmamalıdır");
            return false;
        }
        if (!dto.Image.ValidateType())
        {
            ModelState.AddModelError("Image", "Yalnız şəkil formatında data daxil edə bilərsiniz");
            return false;
        }

        var advertisement = _mapper.Map<Advertisement>(dto);

        string imagePath = await _cloudinaryService.FileCreateAsync(dto.Image);
        advertisement.ImagePath = imagePath;

        await _repository.CreateAsync(advertisement);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var advertisement = await _repository.GetAsync(id);

        if (advertisement is null)
            throw new NotFoundException("Bu id-də məlumat mövcud deyil");


        _repository.Delete(advertisement);
        await _repository.SaveChangesAsync();
        await _cloudinaryService.FileDeleteAsync(advertisement.ImagePath);
    }

    public async Task<List<AdvertisementGetDto>> GetAllAsync()
    {
        var advertisements = await _repository.GetAll().ToListAsync();

        var dtos = _mapper.Map<List<AdvertisementGetDto>>(advertisements);

        return dtos;
    }

    public async Task<AdvertisementGetDto> GetAsync(int id)
    {
        var advertisement = await _repository.GetAsync(id);

        if (advertisement is null)
            throw new NotFoundException("Bu id-də məlumat mövcud deyil");

        var dto = _mapper.Map<AdvertisementGetDto>(advertisement);

        return dto;
    }

    public async Task<AdvertisementUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var advertisement = await _repository.GetAsync(id);

        if (advertisement is null)
            throw new NotFoundException("Bu id-də məlumat mövcud deyil");

        var dto = _mapper.Map<AdvertisementUpdateDto>(advertisement);

        return dto;
    }

    public async Task<bool> UpdateAsync(AdvertisementUpdateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var existAdvertisement = await _repository.GetAsync(dto.Id);

        if (existAdvertisement is null)
            throw new NotFoundException("Bu id-də məlumat mövcud deyil");

        if (!dto.Image?.ValidateSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Şəkilin ölçüsü 2mb dan artıq olmamalıdır");
            return false;
        }
        if (!dto.Image?.ValidateType() ?? false)
        {
            ModelState.AddModelError("Image", "Yalnız şəkil formatında data daxil edə bilərsiniz");
            return false;
        }

        existAdvertisement = _mapper.Map(dto, existAdvertisement);

        if (dto.Image is { })
        {
            string newImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);
            await _cloudinaryService.FileDeleteAsync(existAdvertisement.ImagePath);
            existAdvertisement.ImagePath = newImagePath;
        }

        _repository.Update(existAdvertisement);
        await _repository.SaveChangesAsync();

        return true;
    }
}
