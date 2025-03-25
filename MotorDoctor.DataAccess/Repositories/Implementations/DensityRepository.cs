using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;
internal class DensityRepository : Repository<Density>, IDensityRepository
{
    public DensityRepository(AppDbContext context) : base(context)
    {
    }
}
