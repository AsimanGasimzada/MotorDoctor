using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Localizers;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

internal class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ErrorLocalizer _errorLocalizer;

    public CategoryService(ICategoryRepository repository, IMapper mapper, ICloudinaryService cloudinaryService, ErrorLocalizer errorLocalizer)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _errorLocalizer = errorLocalizer;
    }

    public async Task<bool> CreateAsync(CategoryCreateDto dto, ModelStateDictionary ModelState)
    {
        if (!ModelState.IsValid)
            return false;

        if (dto.ParentId is null && dto.Image is null)
        {
            ModelState.AddModelError("Image", "Əsas kategoriyanın şəkli mütləq olmalıdır.");
            return false;
        }

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
        var category = await _repository.GetAsync(id, x => x.Include(x => x.Children).Include(x => x.ProductCategories));

        if (category is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        if (category.Children.Count > 0)
            throw new InvalidInputException(_errorLocalizer.GetValue(nameof(InvalidInputException)));

        if (category.ProductCategories.Count > 0)
            throw new InvalidInputException(_errorLocalizer.GetValue(nameof(InvalidInputException)));


        if (!string.IsNullOrEmpty(category.ImagePath))
            await _cloudinaryService.FileDeleteAsync(category.ImagePath);

        _repository.Delete(category);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<CategoryGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
    {
        LanguageHelper.CheckLanguageId(ref language);
        var categories = await _repository.GetAll(x => x.Include(x => x.CategoryDetails.Where(x => x.LanguageId == (int)language)).ThenInclude(x => x.Language)
                                                                                                .Include(x => x.Children).Include(x => x.Parent!).Include(x => x.ProductCategories)).ToListAsync();

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
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        await _updateViewCount(category);

        var dto = _mapper.Map<CategoryGetDto>(category);

        return dto;
    }

    public async Task<List<CategoryGetDto>> GetBestCategoriesAsync(Languages language = Languages.Azerbaijan)
    {
        var query = _repository.GetAll(x => x.Include(x => x.ProductCategories).ThenInclude(x => x.Product)
                         .ThenInclude(p => p.ProductDetails.Where(pd => pd.LanguageId == (int)language))
                         .Include(x => x.ProductCategories)
                         .ThenInclude(x => x.Product)
                         .ThenInclude(p => p.ProductSizes));

        query = _repository.OrderByDescending(query, x => x.ProductCategories.Count());

        var result = await query.Take(2).ToListAsync();

        var dtos = _mapper.Map<List<CategoryGetDto>>(result);

        return dtos;
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
    public async Task<List<CategoryFeatureGetDto>> GetFeaturedCategoriesAsync(Languages language = Languages.Azerbaijan)
    {
        var parentCategoriesQuery = _repository.GetFilter(
          x => x.ParentId == null,
          include: x => x
           .Include(x => x.Children)
           .ThenInclude(c => c.ProductCategories)
           .ThenInclude(pc => pc.Product)
           .ThenInclude(p => p.ProductImages)
           .Include(x => x.Children)
           .ThenInclude(c => c.ProductCategories)
           .ThenInclude(pc => pc.Product)
           .ThenInclude(p => p.ProductSizes)
           .Include(x => x.Children)
           .ThenInclude(c => c.ProductCategories)
           .ThenInclude(pc => pc.Product)
           .ThenInclude(p => p.ProductDetails.Where(pd => pd.LanguageId == (int)language))
           .Include(x => x.CategoryDetails.Where(cd => cd.LanguageId == (int)language))
        );


        var parentCategories = await parentCategoriesQuery.OrderByDescending(x => x.Children.Count).Take(3).ToListAsync();


        var dtos = _mapper.Map<List<CategoryFeatureGetDto>>(parentCategories);

        return dtos;
    }

    public async Task<List<ParentCategoryDto>> GetParentCategoriesAsync(Languages language = Languages.Azerbaijan)
    {
        var categories = await _repository.GetFilter(x => x.ParentId == null,
                                    x => x.Include(x => x.Children).ThenInclude(x => x.ProductCategories)
                                    .Include(x => x.CategoryDetails.Where(x => x.LanguageId == (int)language))).ToListAsync();

        categories = categories.Where(x => x.Children.Sum(x => x.ProductCategories.Count) > 0).ToList();

        var dtos = _mapper.Map<List<ParentCategoryDto>>(categories);

        return dtos;
    }


    public async Task<List<ParentCategoryForFilterDto>> GetParentCategoriesForFilterAsync(Languages language = Languages.Azerbaijan)
    {
        var categories = await _repository.GetFilter(x => x.ParentId == null,
                                   x => x.Include(x => x.Children).ThenInclude(x => x.CategoryDetails)
                                   .Include(x => x.CategoryDetails.Where(x => x.LanguageId == (int)language))).ToListAsync();

        var dtos = _mapper.Map<List<ParentCategoryForFilterDto>>(categories);

        return dtos;
    }


    public async Task<CategoryUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var category = await _repository.GetAsync(id, x => x.Include(x => x.CategoryDetails).Include(x => x.Children));

        if (category is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var dto = _mapper.Map<CategoryUpdateDto>(category);

        if (category.ParentId == null && category.Children.Count != 0)
            return dto;

        var parentCategories = await _repository.GetFilter(x => x.ParentId == null && x.Id != id, x => x.Include(x => x.CategoryDetails)).ToListAsync();

        var dtos = _mapper.Map<List<CategoryRelationDto>>(parentCategories);

        dto.Categories = dtos;

        return dto;
    }

    public async Task<CategoryUpdateDto> GetUpdatedDtoAsync(CategoryUpdateDto dto)
    {
        var category = await _repository.GetAsync(dto.Id, x => x.Include(x => x.CategoryDetails).Include(x => x.Children));

        if (category is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        if (category.ParentId == null && category.Children.Count != 0)
            return dto;


        var parentCategories = await _repository.GetFilter(x => x.ParentId == null && x.Id != dto.Id, x => x.Include(x => x.CategoryDetails)).ToListAsync();

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


        var existCategory = await _repository.GetAsync(x => x.Id == dto.Id, x => x.Include(x => x.CategoryDetails).Include(x => x.Children).Include(x => x.ProductCategories));

        if (existCategory is null)
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
            if (existCategory.ParentId == null && existCategory.Children.Count != 0)
            {
                ModelState.AddModelError("ParentId", "Base categorynin base i ola bilməz");
                return false;
            }


            var isExistParent = await _repository.IsExistAsync(x => x.Id == dto.ParentId && x.ParentId == null && x.Id != existCategory.Id);

            if (isExistParent is false)
            {
                ModelState.AddModelError("ParentId", "Bu Id-də kategoriya tapılmadı");
                return false;
            }
        }

        if (dto.ParentId is null)
        {
            if (existCategory.ParentId != null && existCategory.ProductCategories.Count != 0)
            {
                ModelState.AddModelError("ParentId", "Bu kateqoriyada məhsul mövcuddur, ona görə əsas kateqoriya edə bilməzsiniz");
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
    private async Task _updateViewCount(Category category)
    {
        category.ViewCount++;
        _repository.Update(category);
        await _repository.SaveChangesAsync();
    }
}
