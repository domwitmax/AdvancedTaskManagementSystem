using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Services.Comments.DTOs.CommentDTO, Repository.DTOs.CommentDTO>();
            CreateMap<Repository.DTOs.CommentDTO, Services.Comments.DTOs.CommentDTO>();

            CreateMap<Repository.DTOs.CommentDTO, CommentEntity>();
            CreateMap<CommentEntity, Repository.DTOs.CommentDTO>();
        }
    }
}
