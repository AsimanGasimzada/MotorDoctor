using Microsoft.EntityFrameworkCore;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;
internal class DensityService : IDensityService
{
    private readonly IDensityRepository _repository;
    private readonly IMapper _mapper;

    public DensityService(IDensityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> CreateAsync(DensityCreateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var isExist = await _repository.IsExistAsync(x => x.Value.ToLower() == dto.Value.ToLower());

        if (isExist)
        {
            ModelState.AddModelError("Value", "Bu qatılıq artıq mövcuddur.");
            return false;
        }

        var density = _mapper.Map<Density>(dto);

        await _repository.CreateAsync(density);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var density = await _repository.GetAsync(id);

        if (density is null)
            throw new NotFoundException("Qatılıq tapılmadı.");

        _repository.Delete(density);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<DensityGetDto>> GetAllAsync()
    {
        var densisties = await _repository.GetAll().ToListAsync();

        var dtos = _mapper.Map<List<DensityGetDto>>(densisties);

        return dtos;
    }

    public async Task<List<DensityRelationDto>> GetAllForProductAsync()
    {
        var densities = await _repository.GetAll().ToListAsync();

        var dtos = _mapper.Map<List<DensityRelationDto>>(densities);

        return dtos;
    }

    public async Task<DensityGetDto> GetAsync(int id)
    {
        var density = await _repository.GetAsync(id);

        if (density is null)
            throw new NotFoundException("Qatılıq tapılmadı.");

        var dto = _mapper.Map<DensityGetDto>(density);

        return dto;
    }

    public async Task<DensityUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var density = await _repository.GetAsync(id);

        if (density is null)
            throw new NotFoundException("Qatılıq tapılmadı.");

        var dto = _mapper.Map<DensityUpdateDto>(density);

        return dto;
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var isExist = await _repository.IsExistAsync(x => x.Id == id);

        return isExist;
    }

    public async Task<bool> UpdateAsync(DensityUpdateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var existDensity = await _repository.GetAsync(dto.Id);

        if (existDensity is null)
            throw new NotFoundException("Qatılıq tapılmadı!");

        var isExist = await _repository.IsExistAsync(x => x.Value.ToLower() == dto.Value.ToLower() && x.Id != dto.Id);

        if (isExist)
        {
            ModelState.AddModelError("Value", "Bu qatılıq artıq mövcuddur.");
            return false;
        }

        existDensity = _mapper.Map(dto, existDensity);

        _repository.Update(existDensity);
        await _repository.SaveChangesAsync();

        return true;
    }
}
