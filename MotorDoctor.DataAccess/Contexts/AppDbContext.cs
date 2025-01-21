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
        modelBuilder.Entity<Comment>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Order>().HasQueryFilter(x => !x.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_baseEntityInterceptor);

        base.OnConfiguring(optionsBuilder);
    }

    public required DbSet<About> Abouts { get; set; }
    public required DbSet<AboutDetail> AboutDetails { get; set; }
    public required DbSet<Advertisement> Advertisements { get; set; }
    public required DbSet<Attendance> Attendances { get; set; }
    public required DbSet<AttendanceDetail> AttendanceDetails { get; set; }
    public required DbSet<BasketItem> BasketItems { get; set; }
    public required DbSet<Branch> Branches { get; set; }
    public required DbSet<BranchDetail> BranchDetails { get; set; }
    public required DbSet<Brand> Brands { get; set; }
    public required DbSet<BrandDetail> BrandDetails { get; set; }
    public required DbSet<Category> Categories { get; set; }
    public required DbSet<CategoryDetail> CategoryDetails { get; set; }
    public required DbSet<Comment> Comments { get; set; }
    public required DbSet<Language> Languages { get; set; }
    public required DbSet<Order> Orders { get; set; }
    public required DbSet<OrderItem> OrderItems { get; set; }
    public required DbSet<Product> Products { get; set; }
    public required DbSet<ProductDetail> ProductDetails { get; set; }
    public required DbSet<ProductImage> ProductImages { get; set; }
    public required DbSet<ProductSize> ProductSizes { get; set; }
    public required DbSet<ProductCategory> ProductCategories { get; set; }
    public required DbSet<Payment> Payments { get; set; }
    public required DbSet<Setting> Settings { get; set; }
    public required DbSet<SettingDetail> SettingDetails { get; set; }
    public required DbSet<Slider> Sliders { get; set; }
    public required DbSet<SliderDetail> SliderDetails { get; set; }
    public required DbSet<Status> Statuses { get; set; }
    public required DbSet<StatusDetail> StatusDetails { get; set; }
    public required DbSet<Subscriber> Subscribers { get; set; }
    public required DbSet<WishlistItem> WishlistItems { get; set; }

}
