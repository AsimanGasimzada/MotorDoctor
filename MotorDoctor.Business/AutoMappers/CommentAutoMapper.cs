using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class CommentAutoMapper : Profile
{
    public CommentAutoMapper()
    {
        CreateMap<Comment, CommentCreateDto>().ReverseMap();
        CreateMap<Comment, CommentUpdateDto>().ReverseMap();
        CreateMap<Comment, CommentGetDto>().ReverseMap();
    }
}
