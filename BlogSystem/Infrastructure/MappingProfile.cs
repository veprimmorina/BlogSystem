using AutoMapper;
using BlogSystem.Areas.Identity.Data;
using BlogSystem.Core.DTO;
using BlogSystem.Core.Models;

namespace BlogSystem.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<BlogPostDto, BlogPost>();
            CreateMap<BlogPost, BlogPostDto>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();
            CreateMap<ReplyCommentDTO, Comment>();
            CreateMap<Comment, ReplyCommentDTO>();
            CreateMap<UserDTO, ApplicationUser>();
            CreateMap<ApplicationUser, UserDTO>();

        }
    }
}
