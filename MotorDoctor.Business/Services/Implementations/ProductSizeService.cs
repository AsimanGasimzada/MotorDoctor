using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Entities;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

internal class ProductSizeService : IProductSizeService
{
    private readonly IProductSizeRepository _repository;
    private readonly IMapper _mapper;

    public ProductSizeService(IProductSizeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductSizeRelationDto?> GetAsync(int id, Languages language = Languages.Azerbaijan)
    {
        LanguageHelper.CheckLanguageId(ref language);
        var productSize = await _repository.GetAsync(id, x => x.Include(x => x.Product)
                                                .ThenInclude(x => x.ProductDetails.Where(x => x.LanguageId == (int)language))
                                                .Include(x=>x.Product.Category)
                                                .Include(x=>x.Product.ProductImages));

        if (productSize is null)
            return null;

        var dto = _mapper.Map<ProductSizeRelationDto>(productSize);

        return dto;
    }

    public async Task<bool> IsExistAsync(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }


}
