using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;

    public ProductService(IProductRepository repository, IMapper mapper, ICloudinaryService cloudinaryService, ICategoryService categoryService, IBrandService brandService)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _categoryService = categoryService;
        _brandService = brandService;
    }


    public async Task<bool> CreateAsync(ProductCreateDto dto, ModelStateDictionary ModelState)
    {

        #region Validations
        if (!ModelState.IsValid)
            return false;

        var isExistCategory = await _categoryService.IsExistAsync(dto.CategoryId);

        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "Belə Kateqoriya mövcud deyil zəhmət olmasa yenidən daxil edin");
            return false;
        }

        var isExistBrand = await _brandService.IsExistAsync(dto.BrandId);

        if (!isExistBrand)
        {
            ModelState.AddModelError("BrandId", "Belə Brend mövcud deyil zəhmət olmasa yenidən daxil edin");
            return false;
        }

        foreach (var detail in dto.ProductDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.ProductDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }

        if (dto.ProductSizes.Count == 0)
        {
            ModelState.AddModelError("ProductSizes", "Minimum 1 ölçü daxil etməlisiniz");
            return false;
        }


        if (!dto.MainImage.ValidateType())
        {
            ModelState.AddModelError("MainImage", "Yalnız şəkil formatında dəyər daxil edə bilərsiniz");
            return false;
        }

        if (!dto.MainImage.ValidateSize(2))
        {
            ModelState.AddModelError("MainImage", "Şəkilin ölçüsü 2 mb dan artıq ola bilməz");
            return false;
        }

        foreach (var formFile in dto.Images)
        {
            if (!formFile.ValidateType())
            {
                ModelState.AddModelError("Images", "Yalnız şəkil formatında dəyər daxil edə bilərsiniz");
                return false;
            }

            if (!formFile.ValidateSize(2))
            {
                ModelState.AddModelError("Images", "Şəkilin ölçüsü 2 mb dan artıq ola bilməz");
                return false;
            }
        }

        #endregion

        var product = _mapper.Map<Product>(dto);

        product.ProductImages = [];


        string mainImagePath = await _cloudinaryService.FileCreateAsync(dto.MainImage);
        ProductImage mainImage = new() { Path = mainImagePath, IsMain = true };
        product.ProductImages.Add(mainImage);

        foreach (var file in dto.Images)
        {
            string imagePath = await _cloudinaryService.FileCreateAsync(file);
            ProductImage image = new() { Path = imagePath };
            product.ProductImages.Add(image);
        }

        await _repository.CreateAsync(product);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _repository.GetAsync(id);

        if (product is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        _repository.Delete(product);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<ProductGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan)
    {
        LanguageHelper.CheckLanguageId(ref language);
        var products = await _repository.GetAll(_getIncludeFunc(language)).ToListAsync();


        var dtos = _mapper.Map<List<ProductGetDto>>(products);

        return dtos;
    }

    public async Task<PaginateDto<ProductGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan, int page = 1)
    {
        LanguageHelper.CheckLanguageId(ref language);
        var query = _repository.GetAll(_getIncludeFunc(language));

        int count = query.Count();

        int pageCount = (int)Math.Ceiling((decimal)count / 10);

        if (page > pageCount)
            page = pageCount;

        if (page < 1)
            page = 1;

        query = _repository.OrderByDescending(query, x => x.UpdatedAt);

        query = _repository.Paginate(query, 10, page);

        var products = await query.ToListAsync();

        var dtos = _mapper.Map<List<ProductGetDto>>(products);

        PaginateDto<ProductGetDto> dto = new()
        {
            Items = dtos,
            CurrentPage = page,
            PageCount = pageCount,
        };

        return dto;
    }

    public async Task<ProductGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan)
    {
        LanguageHelper.CheckLanguageId(ref language);
        var product = await _repository.GetAsync(id, _getIncludeFunc(language));

        if (product is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var dto = _mapper.Map<ProductGetDto>(product);

        return dto;
    }

    public async Task<ProductCreateDto> GetCreatedDtoAsync()
    {
        var categories = await _categoryService.GetAllForProductAsync();
        var brands = await _brandService.GetAllForProductAsync();

        ProductCreateDto dto = new()
        {
            Categories = categories,
            Brands = brands,
            ProductDetails = [new(), new(), new()],
            ProductSizes = [new(), new()],
        };

        return dto;
    }
    public async Task<ProductCreateDto> GetCreatedDtoAsync(ProductCreateDto dto)
    {
        var categories = await _categoryService.GetAllForProductAsync();
        var brands = await _brandService.GetAllForProductAsync();

        dto.Categories = categories;
        dto.Brands = brands;


        return dto;
    }

    public async Task<ProductUpdateDto> GetUpdatedDtoAsync(int id)
    {
        var product = await _repository.GetAsync(id, _getIncludeFunc());

        if (product is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var dto = _mapper.Map<ProductUpdateDto>(product);

        var categories = await _categoryService.GetAllForProductAsync();
        var brands = await _brandService.GetAllForProductAsync();

        dto.Categories = categories;
        dto.Brands = brands;

        return dto;
    }

    public async Task<ProductUpdateDto> GetUpdatedDtoAsync(ProductUpdateDto dto)
    {
        var categories = await _categoryService.GetAllForProductAsync();
        var brands = await _brandService.GetAllForProductAsync();

        dto.Categories = categories;
        dto.Brands = brands;

        return dto;
    }

    public async Task<bool> UpdateAsync(ProductUpdateDto dto, ModelStateDictionary ModelState)
    {
        #region Validations

        if (!ModelState.IsValid)
            return false;

        var existProduct = await _repository.GetAsync(dto.Id, _getIncludeFunc());

        if (existProduct is null)
            throw new NotFoundException("Bu id-də məlumat tapılmadı");

        var isExistCategory = await _categoryService.IsExistAsync(dto.CategoryId);

        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "Belə Kateqoriya mövcud deyil zəhmət olmasa yenidən daxil edin");
            return false;
        }

        var isExistBrand = await _brandService.IsExistAsync(dto.BrandId);

        if (!isExistBrand)
        {
            ModelState.AddModelError("BrandId", "Belə Brend mövcud deyil zəhmət olmasa yenidən daxil edin");
            return false;
        }

        foreach (var detail in dto.ProductDetails)
        {
            var isExistLanguage = LanguageHelper.CheckLanguageId(detail.LanguageId);

            if (!isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }

            isExistLanguage = dto.ProductDetails.Any(x => x.LanguageId == detail.LanguageId && x != detail);

            if (isExistLanguage)
            {
                ModelState.AddModelError("", "Nə isə yanlış oldu, yenidən sınayın");
                return false;
            }
        }

        if (dto.ProductSizes.Count == 0)
        {
            ModelState.AddModelError("ProductSizes", "Minimum 1 ölçü daxil etməlisiniz");
            return false;
        }


        if (!dto.MainImage?.ValidateType() ?? false)
        {
            ModelState.AddModelError("MainImage", "Yalnız şəkil formatında dəyər daxil edə bilərsiniz");
            return false;
        }

        if (!dto.MainImage?.ValidateSize(2) ?? false)
        {
            ModelState.AddModelError("MainImage", "Şəkilin ölçüsü 2 mb dan artıq ola bilməz");
            return false;
        }

        foreach (var formFile in dto.Images)
        {
            if (!formFile.ValidateType())
            {
                ModelState.AddModelError("Images", "Yalnız şəkil formatında dəyər daxil edə bilərsiniz");
                return false;
            }

            if (!formFile.ValidateSize(2))
            {
                ModelState.AddModelError("Images", "Şəkilin ölçüsü 2 mb dan artıq ola bilməz");
                return false;
            }
        }

        #endregion

        existProduct = _mapper.Map(dto, existProduct);

        //existProduct.ProductSizes = _mapper.Map(dto.ProductSizes, existProduct.ProductSizes);

        //remove deletedImages
        foreach (var image in existProduct.ProductImages.ToList())
        {
            if (image.IsMain || dto.ImageIds.Any(x => x == image.Id))
                continue;

            existProduct.ProductImages.Remove(image);
            await _cloudinaryService.FileDeleteAsync(image.Path);
        }

        //added new Images
        foreach (var newImage in dto.Images)
        {
            string newImagePath = await _cloudinaryService.FileCreateAsync(newImage);

            ProductImage productImage = new() { Path = newImagePath };
            existProduct.ProductImages.Add(productImage);
        }

        //update mainImage
        if (dto.MainImage is { })
        {
            string newMainImagePath = await _cloudinaryService.FileCreateAsync(dto.MainImage);

            var mainImage = existProduct.ProductImages.FirstOrDefault(x => x.IsMain);

            if (mainImage is { })
                mainImage.Path = newMainImagePath;
            else
            {
                mainImage = new() { IsMain = true, Path = newMainImagePath };

                existProduct.ProductImages.Add(mainImage);
            }
        }

        _repository.Update(existProduct);
        await _repository.SaveChangesAsync();

        return true;
    }

    private Func<IQueryable<Product>, IIncludableQueryable<Product, object>> _getIncludeFunc(Languages language)
    {

        return x => x.Include(x => x.ProductDetails.Where(x => x.LanguageId == (int)language)).Include(x => x.ProductImages).Include(x => x.ProductSizes).Include(x => x.Category).Include(x => x.Brand);
    }
    private Func<IQueryable<Product>, IIncludableQueryable<Product, object>> _getIncludeFunc()
    {

        return x => x.Include(x => x.ProductDetails).Include(x => x.ProductImages).Include(x => x.ProductSizes).Include(x => x.Category).Include(x => x.Brand);
    }
}
