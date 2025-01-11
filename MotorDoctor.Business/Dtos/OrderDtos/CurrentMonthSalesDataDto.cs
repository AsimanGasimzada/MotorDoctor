namespace MotorDoctor.Business.Dtos;

public class CurrentMonthSalesDataDto : IDto
{
    public decimal TotalSales { get; set; }
    public int OrderCount { get; set; }
}