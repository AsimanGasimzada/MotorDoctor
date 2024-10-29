using Microsoft.EntityFrameworkCore;

namespace MotorDoctor.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
