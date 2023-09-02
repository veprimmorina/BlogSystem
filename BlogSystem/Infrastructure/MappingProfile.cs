using AutoMapper;
using BlogSystem.Api.DTO;
using BlogSystem.Areas.Identity.Data;
using BlogSystem.Core.Models;

namespace BlogSystem.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<BlogPostDTO, BlogPost>();
            CreateMap<BlogPost, BlogPostDTO>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();
            CreateMap<ReplyCommentDTO, Comment>();
            CreateMap<Comment, ReplyCommentDTO>();
            CreateMap<UserDTO, ApplicationUser>();
            CreateMap<ApplicationUser, UserDTO>();

        }
    }
}
