using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.AddComment;
using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.UpdateContentComment;
using AdvancedTaskManagementSystemCQRS.WebApi.Request;
using AutoMapper;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Comments;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<AddCommentRequest, AddCommentCommand>()
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
        CreateMap<UpdateContentCommentRequest, UpdateContentCommentCommand>();
    }
}
