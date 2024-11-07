using AutoMapper;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.Business.Services.Implementations;

public class ProductSizeService : IProductSizeService
{
    private readonly IProductSizeRepository _repository;
    private readonly IMapper _mapper;

    public ProductSizeService(IProductSizeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductSizeWithBasketGetDto?> GetAsync(int id)
    {
        var productSize=await _repository.GetAsync(id);

        if (productSize is null)
            return null;

        var dto=_mapper.Map<ProductSizeWithBasketGetDto>(productSize);

        return dto;
    }

    public async Task<bool> IsExistAsync(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }
}
