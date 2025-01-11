namespace MotorDoctor.Business.Dtos;

public class DashboardGetDto : IDto
{
    public List<SalesDataDto> SalesData { get; set; } = [];
    public CurrentMonthSalesDataDto CurrentMonthSales { get; set; } = null!;
    public List<BestSellerProductGetDto> BestSellerProducts { get; set; } = [];
    public TopUserDto BestUser { get; set; } = null!;
    public int UserCount { get; set; }
    public int ProductCount { get; set; }
    public int AdvertisementViewCount { get; set; }
}
