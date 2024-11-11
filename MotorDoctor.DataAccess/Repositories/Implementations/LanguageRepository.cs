using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;

internal class LanguageRepository : Repository<Language>, ILanguageRepository
{
    public LanguageRepository(AppDbContext context) : base(context)
    {
    }
}
