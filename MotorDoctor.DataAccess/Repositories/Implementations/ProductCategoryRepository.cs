using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;

internal class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(AppDbContext context) : base(context)
    {
    }
}
