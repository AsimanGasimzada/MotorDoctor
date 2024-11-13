using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

public class StatusService : IStatusService
{
    private readonly IStatusRepository _repository;
    private readonly IMapper _mapper;

    public StatusService(IStatusRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<StatusGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
    {
        var statuses = await _repository.GetAll(_getIncludeFunc(language)).ToListAsync();

        var dtos = _mapper.Map<List<StatusGetDto>>(statuses);

        return dtos;
    }

    public async Task<StatusGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan)
    {
        var status = await _repository.GetAsync(id, _getIncludeFunc(language));

        if (status is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var dto = _mapper.Map<StatusGetDto>(status);

        return dto;
    }
    public async Task<StatusGetDto> GetFirstAsync(Languages language = Languages.Azerbaijan)
    {
        var status = await _repository.GetAsync(x => x.Id > 0, include: _getIncludeFunc(language));

        if (status is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var dto = _mapper.Map<StatusGetDto>(status);

        return dto;
    }

    public async Task<StatusGetDto> GetLastAsync(Languages language = Languages.Azerbaijan)
    {
        var statuses = await _repository.GetAll(include: _getIncludeFunc(language)).ToListAsync(); ;

        var status = statuses.LastOrDefault();

        if (status is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var dto = _mapper.Map<StatusGetDto>(status);

        return dto;
    }

    private Func<IQueryable<Status>, IIncludableQueryable<Status, object>> _getIncludeFunc(Languages language)
    {
        LanguageHelper.CheckLanguageId(ref language);
        return x => x.Include(x => x.StatusDetails.Where(x => x.LanguageId == (int)language)).ThenInclude(x => x.Language);
    }
}
