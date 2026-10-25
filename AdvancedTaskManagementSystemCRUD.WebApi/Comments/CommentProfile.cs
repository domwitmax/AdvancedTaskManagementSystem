using AdvancedTaskManagementSystemCRUD.Application.Services.Comments.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AdvancedTaskManagementSystemCRUD.WebApi.Request;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Comments;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<AddCommentRequest, CommentDTO>()
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
        CreateMap<UpdateContentCommentRequest, CommentDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CommentId));
        
        CreateMap<CommentEntity, CommentDTO>();
        CreateMap<CommentDTO, CommentEntity>();
    }
}
