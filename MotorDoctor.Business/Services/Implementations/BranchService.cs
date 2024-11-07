using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;

    public BranchService(IBranchRepository repository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<bool> CreateAsync(BranchCreateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        if (!dto.Image?.ValidateType() ?? false)
        {
            ModelState.AddModelError("Image", "Yalnız şəkil formatında dəyər daxil edə bilərsiniz");
            return false;
        }

        if (!dto.Image?.ValidateSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Şəkilin ölçüsü 2 mb dan artıq ola bilməz");
            return false;
        }

        foreach (var detail in dto.BranchDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.BranchDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }

        var branch = _mapper.Map<Branch>(dto);

        if (dto.Image is { })
        {
            string imagePath = await _cloudinaryService.FileCreateAsync(dto.Image);
            branch.ImagePath = imagePath;
        }

        await _repository.CreateAsync(branch);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var branch = await _repository.GetAsync(id);

        if (branch is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        _repository.Delete(branch);
        await _repository.SaveChangesAsync();

        if (!string.IsNullOrEmpty(branch.ImagePath))
            await _cloudinaryService.FileDeleteAsync(branch.ImagePath);
    }

    public async Task<List<BranchGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
    {
        var branches = await _repository.GetAll(_getIncludeFunc(language)).ToListAsync();

        var dtos = _mapper.Map<List<BranchGetDto>>(branches);

        return dtos;
    }

    public async Task<BranchGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan)
    {
        var branch = await _repository.GetAsync(id, _getIncludeFunc(language));

        if (branch is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var dto = _mapper.Map<BranchGetDto>(branch);

        return dto;
    }

    public async Task<BranchUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var branch = await _repository.GetAsync(id, x => x.Include(x => x.BranchDetails));

        if (branch is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var dto = _mapper.Map<BranchUpdateDto>(branch);

        return dto;
    }

    public async Task<bool> UpdateAsync(BranchUpdateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var existBranch=await _repository.GetAsync(dto.Id,x=>x.Include(x=>x.BranchDetails));

        if(existBranch is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        if (!dto.Image?.ValidateType() ?? false)
        {
            ModelState.AddModelError("Image", "Yalnız şəkil formatında dəyər daxil edə bilərsiniz");
            return false;
        }

        if (!dto.Image?.ValidateSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Şəkilin ölçüsü 2 mb dan artıq ola bilməz");
            return false;
        }

        foreach (var detail in dto.BranchDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.BranchDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }

        existBranch=_mapper.Map(dto,existBranch);

        if(dto.Image is { })
        {
            string newImagePath=await _cloudinaryService.FileCreateAsync(dto.Image);

            if(!string.IsNullOrEmpty(existBranch.ImagePath))
            {
                await _cloudinaryService.FileDeleteAsync(existBranch.ImagePath);
            }

            existBranch.ImagePath = newImagePath;
        }

        _repository.Update(existBranch);
        await _repository.SaveChangesAsync();   

        return true;
    }

    private Func<IQueryable<Branch>, IIncludableQueryable<Branch, object>> _getIncludeFunc(Languages language)
    {
        LanguageHelper.CheckLanguageId(ref language);
        return x => x.Include(x => x.BranchDetails.Where(x => x.LanguageId == (int)language)).ThenInclude(x => x.Language);
    }
}
