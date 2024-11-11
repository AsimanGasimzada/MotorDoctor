using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;

internal class SliderRepository : Repository<Slider>, ISliderRepository
{
    public SliderRepository(AppDbContext context) : base(context)
    {
    }
}
