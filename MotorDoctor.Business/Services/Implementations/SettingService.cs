using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

internal class SettingService : ISettingService
{
    private readonly ISettingRepository _repository;
    private readonly IMapper _mapper;

    public SettingService(ISettingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> CreateAsync(SettingCreateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        foreach (var detail in dto.SettingDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.SettingDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }


        var isExist = await _repository.IsExistAsync(x => x.Key.ToUpper() == dto.Key.ToUpper());

        if (isExist)
        {
            ModelState.AddModelError("Key", "Bu açar söz artıq mövcuddur");
            return false;
        }


        var setting = _mapper.Map<Setting>(dto);

        await _repository.CreateAsync(setting);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var setting = await _repository.GetAsync(id);

        if (setting is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        _repository.Delete(setting);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<SettingGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
    {
        LanguageHelper.CheckLanguageId(ref language);

        var settings = await _repository.GetAll(_getIncludeFunc(language)).ToListAsync();

        var dtos = _mapper.Map<List<SettingGetDto>>(settings);

        return dtos;
    }

    public async Task<SettingGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan)
    {
        var setting = await _repository.GetAsync(id, _getIncludeFunc(language));

        if (setting is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var dto = _mapper.Map<SettingGetDto>(setting);

        return dto;
    }

    public async Task<Dictionary<string, string>> GetSettingsWithDictionaryAsync(Languages language=Languages.Azerbaijan)
    {
        var settings = await _repository.GetAll(_getIncludeFunc(language))
                                        .ToDictionaryAsync(x => x.Key, x => x.SettingDetails.FirstOrDefault()?.Value ?? "");

        return settings;
    }

    public async Task<SettingUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var setting = await _repository.GetAsync(id, x => x.Include(x => x.SettingDetails));

        if (setting is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var dto = _mapper.Map<SettingUpdateDto>(setting);

        return dto;
    }

    public async Task<bool> UpdateAsync(SettingUpdateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        foreach (var detail in dto.SettingDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.SettingDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }

        var existSetting = await _repository.GetAsync(dto.Id, x => x.Include(x => x.SettingDetails));

        if (existSetting is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");


        existSetting = _mapper.Map(dto, existSetting);

        _repository.Update(existSetting);
        await _repository.SaveChangesAsync();

        return true;
    }

    private Func<IQueryable<Setting>, IIncludableQueryable<Setting, object>> _getIncludeFunc(Languages language)
    {
        LanguageHelper.CheckLanguageId(ref language);
        return x => x.Include(x => x.SettingDetails.Where(x => x.LanguageId == (int)language)).ThenInclude(x => x.Language);
    }
}
