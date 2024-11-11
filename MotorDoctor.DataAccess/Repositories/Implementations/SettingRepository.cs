using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;

internal class SettingRepository : Repository<Setting>, ISettingRepository
{
    public SettingRepository(AppDbContext context) : base(context)
    {
    }
}
