using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Localizers;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

internal class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;
    private readonly ErrorLocalizer _errorLocalizer;

    public ProductService(IProductRepository repository, IMapper mapper, ICloudinaryService cloudinaryService, ICategoryService categoryService, IBrandService brandService, ErrorLocalizer errorLocalizer)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _categoryService = categoryService;
        _brandService = brandService;
        _errorLocalizer = errorLocalizer;
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

    public async Task DecreaseSalesCountAsync(int productSizeId, int count = 1)
    {
        var product = await _repository.GetAsync(x => x.ProductSizes.Any(x => x.Id == productSizeId), x => x.Include(x => x.ProductSizes));

        if (product is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        if (count < 0)
            count = 0;

        product.SalesCount -= count;
        product.ProductSizes.FirstOrDefault(x => x.Id == productSizeId)!.Count -= count;

        _repository.Update(product);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _repository.GetAsync(id);

        if (product is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

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

    public async Task<ShopFilterDto> GetAllWithPageAsync(ProductFilterDto? filterDto, Languages language = Languages.Azerbaijan, int page = 1)
    {
        var query = _repository.GetAll(_getIncludeFunc(language));

        query = _repository.OrderByDescending(query, x => x.UpdatedAt);

        if (filterDto is { })
        {
            if (!string.IsNullOrWhiteSpace(filterDto.Search))
                query = query.Where(x => x.ProductDetails.Any(x => x.Name.Contains(filterDto.Search)));

            if (filterDto.CategoryIds.Count is not 0)
                query = query.Where(x => filterDto.CategoryIds.Any(c => c == x.CategoryId) || filterDto.CategoryIds.Any(c => c == x.Category.ParentId));

            if (filterDto.BrandIds.Count is not 0)
                query = query.Where(x => filterDto.BrandIds.Any(c => c == x.BrandId));

            if (filterDto.MaxPrice is not 0)
                query = query.Where(x => x.ProductSizes.Any(x => x.Price < filterDto.MaxPrice));

            if (filterDto.MinPrice is not 0)
                query = query.Where(x => x.ProductSizes.Any(x => x.Price > filterDto.MinPrice));

            switch (filterDto.SortType)
            {
                case SortTypes.Latest:
                    break;

                case SortTypes.Oldest:
                    query = query.OrderBy(x => x.UpdatedAt);
                    break;

                case SortTypes.Popularity:
                    query = query.OrderByDescending(x => x.SalesCount);
                    break;

                case SortTypes.PriceDescending:
                    query = query.OrderByDescending(x => x.ProductSizes.FirstOrDefault() != null ? x.ProductSizes.FirstOrDefault()!.Price : x.Id);
                    break;

                case SortTypes.PriceAscending:
                    query = query.OrderBy(x => x.ProductSizes.FirstOrDefault() != null ? x.ProductSizes.FirstOrDefault()!.Price : x.Id);
                    break;
            }

            if (filterDto.MaxPrice is 0)
                filterDto.MaxPrice = 1000;
        }

        int count = await query.CountAsync();

        int pageCount = (int)Math.Ceiling((decimal)count / 9);

        if (page > pageCount)
            page = pageCount;

        if (page < 1)
            page = 1;

        query = _repository.Paginate(query, 9, page);

        var products = await query.ToListAsync();

        var dtos = _mapper.Map<List<ProductGetDto>>(products);

        PaginateDto<ProductGetDto> paginateDto = new()
        {
            Items = dtos,
            CurrentPage = page,
            PageCount = pageCount,
        };

        ShopFilterDto dto = new() { ProductFilterDto = filterDto, Products = paginateDto };

        return dto;
    }

    public async Task<PaginateDto<ProductGetDto>> GetAllWithPageAsync(Languages language = Languages.Azerbaijan, int page = 1)
    {
        var query = _repository.GetAll(_getIncludeFunc(language));

        query = _repository.OrderByDescending(query, x => x.UpdatedAt);


        int count = await query.CountAsync();

        int pageCount = (int)Math.Ceiling((decimal)count / 10);

        if (page > pageCount)
            page = pageCount;

        if (page < 1)
            page = 1;


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
        var product = await _repository.GetAsync(id, _getIncludeFunc(language));

        if (product is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        var dto = _mapper.Map<ProductGetDto>(product);

        return dto;
    }

    public async Task<List<BestSellerProductGetDto>> GetBestProductsAsync(Languages language = Languages.Azerbaijan)
    {
        var query = _repository.GetAll(x => x.Include(x => x.Category)
                                    .ThenInclude(x => x.Parent!).Include(x => x.ProductDetails.Where(x => x.LanguageId == (int)language)));

        query = _repository.OrderBy(query, x => x.SalesCount);

        var bestSellerProduct = await query.FirstOrDefaultAsync();

        if (bestSellerProduct is null)
            return new();

        var secondBestSellerProduct = await query.FirstOrDefaultAsync(x => x != bestSellerProduct && x.CategoryId != bestSellerProduct.CategoryId && x.Category.ParentId != bestSellerProduct.Category.ParentId);

        if (secondBestSellerProduct is null)
            return new();

        List<Product> list = [bestSellerProduct, secondBestSellerProduct];

        var dtos = _mapper.Map<List<BestSellerProductGetDto>>(list);

        return dtos;
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
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

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

    public async Task IncreaseSalesCountAsync(int productSizeId, int count = 1)
    {
        var product = await _repository.GetAsync(x => x.ProductSizes.Any(x => x.Id == productSizeId), x => x.Include(x => x.ProductSizes));

        if (product is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

        if (count < 0)
            count = 0;

        product.SalesCount += count;
        product.ProductSizes.FirstOrDefault(x => x.Id == productSizeId)!.Count += count;

        _repository.Update(product);
        await _repository.SaveChangesAsync();

    }

    public async Task<bool> UpdateAsync(ProductUpdateDto dto, ModelStateDictionary ModelState)
    {
        #region Validations

        if (!ModelState.IsValid)
            return false;

        var existProduct = await _repository.GetAsync(dto.Id, _getIncludeFunc());

        if (existProduct is null)
            throw new NotFoundException(_errorLocalizer.GetValue(nameof(NotFoundException)));

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

        //removeSizes
        foreach (var productSize in existProduct.ProductSizes.ToList())
        {
            if (dto.ProductSizes.Any(x => x.Id == productSize.Id))
                continue;

            existProduct.ProductSizes.Remove(productSize);
        }

        //updateSizes
        foreach (var sizeDto in dto.ProductSizes)
        {
            var existSize = existProduct.ProductSizes.FirstOrDefault(x => x.Id == sizeDto.Id);

            if (existSize is null)
            {
                var newSize = _mapper.Map<ProductSize>(sizeDto);
                existProduct.ProductSizes.Add(newSize);
                continue;
            }

            existSize = _mapper.Map(sizeDto, existSize);
        }


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
        LanguageHelper.CheckLanguageId(ref language);
        return x => x.Include(x => x.ProductDetails.Where(x => x.LanguageId == (int)language)).Include(x => x.ProductImages).Include(x => x.ProductSizes).Include(x => x.Category.CategoryDetails.Where(x => x.LanguageId == (int)language)).Include(x => x.Brand.BrandDetails.Where(x => x.LanguageId == (int)language));
    }
    private Func<IQueryable<Product>, IIncludableQueryable<Product, object>> _getIncludeFunc()
    {
        return x => x.Include(x => x.ProductDetails).Include(x => x.ProductImages).Include(x => x.ProductSizes).Include(x => x.Category).Include(x => x.Brand);
    }
}
