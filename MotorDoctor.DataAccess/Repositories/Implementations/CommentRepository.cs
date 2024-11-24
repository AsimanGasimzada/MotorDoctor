using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Repositories.Abstractions;

namespace MotorDoctor.DataAccess.Repositories.Implementations;

internal class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {
    }
}
