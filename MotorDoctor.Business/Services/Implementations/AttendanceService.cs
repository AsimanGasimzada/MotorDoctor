using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Localizers;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;
internal class AttendanceService : IAttendanceService
{
    private readonly IAttedanceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ErrorLocalizer _errorLocalizer;

    public AttendanceService(IAttedanceRepository repository, IMapper mapper, ICloudinaryService cloudinaryService, ErrorLocalizer errorLocalizer)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _errorLocalizer = errorLocalizer;
    }

    public async Task<bool> CreateAsync(AttendanceCreateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        if (!dto.Image.ValidateSize(2))
        {
            ModelState.AddModelError("Image", "Şəklin həcmi 2 mb-dan çox olmamalıdır.");
            return false;
        }
        if (!dto.Image.ValidateType())
        {
            ModelState.AddModelError("Image", "Yalnız şəkil formatı daxil edin");
            return false;
        }

        foreach (var detail in dto.AttendanceDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.AttendanceDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }

        var attendance = _mapper.Map<Attendance>(dto);

        string imagePath = await _cloudinaryService.FileCreateAsync(dto.Image);

        attendance.ImagePath = imagePath;

        await _repository.CreateAsync(attendance);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var attedance = await _repository.GetAsync(id);

        if (attedance is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        _repository.Delete(attedance);
        await _repository.SaveChangesAsync();

        await _cloudinaryService.FileDeleteAsync(attedance.ImagePath);
    }

    public async Task<List<AttendanceGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
    {
        var attendances = await _repository.GetAll(_getIncludeFunc(language)).ToListAsync();

        var dtos = _mapper.Map<List<AttendanceGetDto>>(attendances);

        return dtos;
    }

    public async Task<AttendanceGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan)
    {
        var attendance = await _repository.GetAsync(id, _getIncludeFunc(language));

        if (attendance is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var dto = _mapper.Map<AttendanceGetDto>(attendance);

        return dto;
    }

    public async Task<AttendanceUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var attendance = await _repository.GetAsync(id, x => x.Include(x => x.AttendanceDetails));

        if (attendance is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var dto = _mapper.Map<AttendanceUpdateDto>(attendance);

        return dto;
    }

    public async Task<bool> UpdateAsync(AttendanceUpdateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var existAttendance = await _repository.GetAsync(dto.Id, x => x.Include(x => x.AttendanceDetails));

        if (existAttendance is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        if (!dto.Image?.ValidateSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Şəklin həcmi 2 mb-dan çox olmamalıdır.");
            return false;
        }
        if (!dto.Image?.ValidateType() ?? false)
        {
            ModelState.AddModelError("Image", "Yalnız şəkil formatı daxil edin");
            return false;
        }

        foreach (var detail in dto.AttendanceDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.AttendanceDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }

        existAttendance = _mapper.Map(dto, existAttendance);

        if (dto.Image is { })
        {
            string newImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);
            await _cloudinaryService.FileDeleteAsync(existAttendance.ImagePath);
            existAttendance.ImagePath = newImagePath;
        }

        _repository.Update(existAttendance);
        await _repository.SaveChangesAsync();

        return true;
    }


    private Func<IQueryable<Attendance>, IIncludableQueryable<Attendance, object>> _getIncludeFunc(Languages language)
    {
        LanguageHelper.CheckLanguageId(ref language);
        return x => x.Include(x => x.AttendanceDetails.Where(x => x.LanguageId == (int)language)).ThenInclude(x => x.Language);
    }
}
