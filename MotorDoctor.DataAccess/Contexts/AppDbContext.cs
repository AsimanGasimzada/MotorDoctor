using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotorDoctor.DataAccess.DataInitializers;
using MotorDoctor.DataAccess.Interceptors;
using System.Reflection;

namespace MotorDoctor.DataAccess.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    private readonly BaseEntityInterceptor _baseEntityInterceptor;
    public AppDbContext(DbContextOptions<AppDbContext> options, BaseEntityInterceptor baseEntityInterceptor) : base(options)
    {
        _baseEntityInterceptor = baseEntityInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddSeedData();

        modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<ProductSize>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_baseEntityInterceptor);

        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Attendance> Attendances { get; set; } = null!;
    public DbSet<AttendanceDetail> AttendanceDetails { get; set; } = null!;
    public DbSet<Language> Languages { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<BrandDetail> BrandDetails { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<CategoryDetail> CategoryDetails { get; set; } = null!;
    public DbSet<Slider> Sliders { get; set; } = null!;
    public DbSet<SliderDetail> SliderDetails { get; set; } = null!;
    public DbSet<SettingDetail> SettingDetails { get; set; } = null!;
    public DbSet<Setting> Settings { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductSize> ProductSizes { get; set; } = null!;
    public DbSet<ProductImage> ProductImages { get; set; } = null!;
    public DbSet<ProductDetail> ProductDetails { get; set; } = null!;
    public DbSet<Branch> Branches { get; set; } = null!;
    public DbSet<BranchDetail> BranchDetails { get; set; } = null!;
    public DbSet<BasketItem> BasketItems { get; set; } = null!;
    public DbSet<Subscriber> Subscribers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<StatusDetail> StatusDetails { get; set; } = null!;
}
