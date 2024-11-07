using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;

    public CategoryService(ICategoryRepository repository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<bool> CreateAsync(CategoryCreateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

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

        foreach (var detail in dto.CategoryDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.CategoryDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }
        if (dto.ParentId is not null)
        {
            var isExistParent = await _repository.IsExistAsync(x => x.Id == dto.ParentId && x.ParentId == null);

            if (isExistParent is false)
            {
                ModelState.AddModelError("ParentId", "Bu Id-də kategoriya tapılmadı");
                return false;
            }
        }

        var category = _mapper.Map<Category>(dto);

        if (dto.Image is { })
        {
            string imagePath = await _cloudinaryService.FileCreateAsync(dto.Image);
            category.ImagePath = imagePath;
        }

        await _repository.CreateAsync(category);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _repository.GetAsync(id, x => x.Include(x => x.Children));

        if (category is null)
            throw new NotFoundException("Bu Id-də kategoriya tapılmadı");

        foreach (var child in category.Children)
            child.ParentId = null;

        if (!string.IsNullOrEmpty(category.ImagePath))
            await _cloudinaryService.FileDeleteAsync(category.ImagePath);

        _repository.Delete(category);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<CategoryGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
    {
 
        var categories = await _repository.GetAll(_getIncludeFunc(language)).ToListAsync();

        var dtos = _mapper.Map<List<CategoryGetDto>>(categories);

        return dtos;
    }

    public async Task<List<CategoryForProductGetDto>> GetAllForProductAsync()
    {
        var categories = await _repository.GetFilter(x => x.ParentId != null, _getIncludeFunc(Languages.Azerbaijan)).ToListAsync();

        var dtos = _mapper.Map<List<CategoryForProductGetDto>>(categories);

        return dtos;
    }

    public async Task<CategoryGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan)
    {
        var category = await _repository.GetAsync(id, x => x.Include(x => x.CategoryDetails.Where(x => x.LanguageId == (int)language)).Include(x => x.Children));

        if (category is null)
            throw new NotFoundException("Bu Id-də kategoriya tapılmadı");

        var dto = _mapper.Map<CategoryGetDto>(category);

        return dto;
    }

    public async Task<CategoryUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var category = await _repository.GetAsync(id, x => x.Include(x => x.CategoryDetails));

        if (category is null)
            throw new NotFoundException("Bu Id-də kategoriya tapılmadı");

        var dto = _mapper.Map<CategoryUpdateDto>(category);

        return dto;
    }

    public async Task<bool> IsExistAsync(int id) //for productService
    {
        var isExist = await _repository.IsExistAsync(x => x.Id == id && x.ParentId != null);

        return isExist;
    }

    public async Task<bool> UpdateAsync(CategoryUpdateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;


        var existCategory = await _repository.GetAsync(x => x.Id == dto.Id, x => x.Include(x => x.CategoryDetails).Include(x => x.Children));

        if (existCategory is null)
            throw new NotFoundException("Bu id-də məlumat mövcud deyi");

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

        foreach (var detail in dto.CategoryDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.CategoryDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }
        if (dto.ParentId is not null)
        {
            var isExistParent = await _repository.IsExistAsync(x => x.Id == dto.ParentId && x.ParentId == null);

            if (isExistParent is false)
            {
                ModelState.AddModelError("ParentId", "Bu Id-də kategoriya tapılmadı");
                return false;
            }
        }

        if (dto.Image is { })
        {
            string newImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);
            if (!string.IsNullOrWhiteSpace(existCategory.ImagePath))
                await _cloudinaryService.FileDeleteAsync(existCategory.ImagePath);
            existCategory.ImagePath = newImagePath;
        }

        existCategory = _mapper.Map(dto, existCategory);

        _repository.Update(existCategory);
        await _repository.SaveChangesAsync();

        return true;
    }


    private Func<IQueryable<Category>, IIncludableQueryable<Category, object>> _getIncludeFunc(Languages language)
    {
        LanguageHelper.CheckLanguageId(ref language);
        return x => x.Include(x => x.CategoryDetails.Where(x => x.LanguageId == (int)language)).ThenInclude(x => x.Language);
    }
}
