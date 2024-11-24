using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;

internal class AboutRepository : Repository<About>, IAboutRepository
{
    public AboutRepository(AppDbContext context) : base(context)
    {
    }
}
