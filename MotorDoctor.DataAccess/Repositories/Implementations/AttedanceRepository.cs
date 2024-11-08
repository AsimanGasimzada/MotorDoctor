using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;

public class AttedanceRepository : Repository<Attendance>, IAttedanceRepository
{
    public AttedanceRepository(AppDbContext context) : base(context)
    {
    }
}
