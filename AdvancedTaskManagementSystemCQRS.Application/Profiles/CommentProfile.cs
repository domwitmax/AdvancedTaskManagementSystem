using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.AddComment;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;

namespace AdvancedTaskManagementSystemCQRS.Application.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<AddCommentCommand, CommentEntity>();
            CreateMap<CommentEntity, Repository.Comments.DTOs.CommentDTO>();
            CreateMap<Repository.Comments.DTOs.CommentDTO, CommentDTO>();
        }
    }
}
