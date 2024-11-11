using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;

internal class StatusRepository : Repository<Status>, IStatusRepository
{
    public StatusRepository(AppDbContext context) : base(context)
    {
    }
}
