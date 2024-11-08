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
        var category = await _repository.GetAsync(id, x => x.Include(x => x.Children).Include(x => x.Products));

        if (category is null)
            throw new NotFoundException("Bu Id-də kategoriya tapılmadı");

        if (category.Children.Count > 0)
            throw new InvalidInputException("Alt kategoriyaları olan kategoriyanı silə bilməzsiniz");

        if (category.Products.Count > 0)
            throw new InvalidInputException("Məhsulları olan kategoriyanı silə bilməzsiniz");


        if (!string.IsNullOrEmpty(category.ImagePath))
            await _cloudinaryService.FileDeleteAsync(category.ImagePath);

        _repository.Delete(category);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<CategoryGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
    {
        LanguageHelper.CheckLanguageId(ref language);
        var categories = await _repository.GetAll(x => x.Include(x => x.CategoryDetails.Where(x => x.LanguageId == (int)language)).ThenInclude(x => x.Language)
                                                                                                .Include(x => x.Children).Include(x => x.Parent!).Include(x => x.Products)).ToListAsync();

        var dtos = _mapper.Map<List<CategoryGetDto>>(categories);

        return dtos;
    }

    public async Task<List<CategoryRelationDto>> GetAllForProductAsync()
    {
        var categories = await _repository.GetFilter(x => x.ParentId != null, _getIncludeFunc(Languages.Azerbaijan)).ToListAsync();

        var dtos = _mapper.Map<List<CategoryRelationDto>>(categories);

        return dtos;
    }

    public async Task<CategoryGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan)
    {
        LanguageHelper.CheckLanguageId(ref language);
        var category = await _repository.GetAsync(id, x => x.Include(x => x.CategoryDetails.Where(x => x.LanguageId == (int)language)).Include(x => x.Children).Include(x => x.Parent!));

        if (category is null)
            throw new NotFoundException("Bu Id-də kategoriya tapılmadı");

        var dto = _mapper.Map<CategoryGetDto>(category);

        return dto;
    }

    public async Task<CategoryCreateDto> GetCreateDtoAsync()
    {
        var parentCategories = await _repository.GetFilter(x => x.ParentId == null, x => x.Include(x => x.CategoryDetails)).ToListAsync();

        var dtos = _mapper.Map<List<CategoryRelationDto>>(parentCategories);

        var dto = new CategoryCreateDto() { Categories = dtos, CategoryDetails = [new(), new(), new()] };


        return dto;
    }

    public async Task<CategoryCreateDto> GetCreateDtoAsync(CategoryCreateDto dto)
    {
        var parentCategories = await _repository.GetFilter(x => x.ParentId == null, x => x.Include(x => x.CategoryDetails)).ToListAsync();

        var dtos = _mapper.Map<List<CategoryRelationDto>>(parentCategories);

        dto.Categories = dtos;

        return dto;
    }

    public async Task<CategoryUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var category = await _repository.GetAsync(id, x => x.Include(x => x.CategoryDetails));

        if (category is null)
            throw new NotFoundException("Bu Id-də kategoriya tapılmadı");

        var dto = _mapper.Map<CategoryUpdateDto>(category);

        var parentCategories = await _repository.GetFilter(x => x.ParentId == null, x => x.Include(x => x.CategoryDetails)).ToListAsync();

        var dtos = _mapper.Map<List<CategoryRelationDto>>(parentCategories);

        dto.Categories = dtos;

        return dto;
    }

    public async Task<CategoryUpdateDto> GetUpdatedDtoAsync(CategoryUpdateDto dto)
    {
        var parentCategories = await _repository.GetFilter(x => x.ParentId == null, x => x.Include(x => x.CategoryDetails)).ToListAsync();

        var dtos = _mapper.Map<List<CategoryRelationDto>>(parentCategories);

        dto.Categories = dtos;

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
