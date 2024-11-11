using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;

internal class ProductSizeRepository : Repository<ProductSize>, IProductSizeRepository
{
    public ProductSizeRepository(AppDbContext context) : base(context)
    {
    }
}
