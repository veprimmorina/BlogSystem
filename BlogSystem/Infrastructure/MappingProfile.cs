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
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<ReplyCommentDto, Comment>();
            CreateMap<Comment, ReplyCommentDto>();
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserDto>();

        }
    }
}
