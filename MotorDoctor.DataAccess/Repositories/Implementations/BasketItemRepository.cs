using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;

internal class BasketItemRepository:Repository<BasketItem>,IBasketItemRepository
{
    public BasketItemRepository(AppDbContext context):base(context)
    {
    }
}
