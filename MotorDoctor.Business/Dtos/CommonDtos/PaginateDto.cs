namespace MotorDoctor.Business.Dtos;

public class PaginateDto<T> where T : class, IDto
{
    public List<T> Items { get; set; } = [];
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}
