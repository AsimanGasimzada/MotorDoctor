using Microsoft.EntityFrameworkCore;
using MotorDoctor.Core.Entities;
using MotorDoctor.DataAccess.DataInitializers;
using System.Reflection;

namespace MotorDoctor.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddSeedData();
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Language> Languages { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<CategoryDetail> CategoryDetails { get; set; } = null!;
    public DbSet<Slider> Sliders { get; set; } = null!;
    public DbSet<SliderDetail> SliderDetails { get; set; } = null!;
    public DbSet<SettingDetail> SettingDetails { get; set; } = null!;
    public DbSet<Setting> Settings { get; set; } = null!;
}
