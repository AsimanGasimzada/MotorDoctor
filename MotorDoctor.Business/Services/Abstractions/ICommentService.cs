namespace MotorDoctor.Business.Services.Abstractions;

public interface ICommentService : IModifyService<CommentCreateDto, CommentUpdateDto>, IGetService<CommentGetDto>
{
    Task<List<CommentGetDto>> GetProductCommentsAsync(int productId);
    Task<bool> CheckIsAllowCommentAsync(int productId);
}
