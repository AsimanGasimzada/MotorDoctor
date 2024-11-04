﻿using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;

    public BrandService(IBrandRepository repository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<bool> CreateAsync(BrandCreateDto dto, ModelStateDictionary ModelState)
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

        foreach (var detail in dto.BrandDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.BrandDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }

        var brand = _mapper.Map<Brand>(dto);

        string imagePath = await _cloudinaryService.FileCreateAsync(dto.Image);

        brand.ImagePath = imagePath;

        await _repository.CreateAsync(brand);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var brand = await _repository.GetAsync(x => x.Id == id);

        if (brand is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        _repository.Delete(brand);
        await _repository.SaveChangesAsync();
        await _cloudinaryService.FileDeleteAsync(brand.ImagePath);
    }

    public async Task<List<BrandGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
    {
        LanguageHelper.CheckLanguageId(ref language);
        var brands = await _repository.GetAll(_getIncludeFunc(language)).ToListAsync();

        var dtos = _mapper.Map<List<BrandGetDto>>(brands);

        return dtos;
    }

    public async Task<List<BrandForProductGetDto>> GetAllForProductAsync()
    {
        var brands = await _repository.GetAll(_getIncludeFunc(Languages.Azerbaijan)).ToListAsync();

        var dtos = _mapper.Map<List<BrandForProductGetDto>>(brands);

        return dtos;
    }

    public async Task<BrandGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan)
    {
        LanguageHelper.CheckLanguageId(ref language);
        var brand = await _repository.GetAsync(id, _getIncludeFunc(language));

        if (brand is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var dto = _mapper.Map<BrandGetDto>(brand);

        return dto;
    }

    public async Task<BrandUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var brand = await _repository.GetAsync(id, x => x.Include(x => x.BrandDetails));


        if (brand is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var dto = _mapper.Map<BrandUpdateDto>(brand);

        return dto;
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var isExist = await _repository.IsExistAsync(x => x.Id == id);

        return isExist;
    }

    public async Task<bool> UpdateAsync(BrandUpdateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        var existBrand = await _repository.GetAsync(dto.Id, x => x.Include(x => x.BrandDetails));

        if (existBrand is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

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

        foreach (var detail in dto.BrandDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.BrandDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }

        existBrand = _mapper.Map(dto, existBrand);

        if (dto.Image is { })
        {
            string newImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);
            await _cloudinaryService.FileDeleteAsync(existBrand.ImagePath);
            existBrand.ImagePath = newImagePath;
        }

        _repository.Update(existBrand);
        await _repository.SaveChangesAsync();

        return true;

    }

    private Func<IQueryable<Brand>, IIncludableQueryable<Brand, object>> _getIncludeFunc(Languages language)
    {

        return x => x.Include(x => x.BrandDetails.Where(x => x.LanguageId == (int)language)).ThenInclude(x => x.Language);
    }
}