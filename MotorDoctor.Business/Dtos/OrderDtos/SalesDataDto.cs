namespace MotorDoctor.Business.Dtos;

public class SalesDataDto : IDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalSales { get; set; }
}
