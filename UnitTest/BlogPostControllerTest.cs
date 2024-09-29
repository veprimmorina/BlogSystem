using System;
using System.Threading.Tasks;
using AutoMapper;
using BlogSystem.Api.Controllers;
using BlogSystem.Controllers;
using BlogSystem.Core.DTO;
using BlogSystem.Core.Interfaces.Service;
using BlogSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BlogSystem.Api.Tests
{
    public class BlogPostsControllerTests
    {
        private readonly BlogPostsController _controller;
        private readonly Mock<IBlogService> _blogServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILikeService> _likeServiceMock;
        private readonly Mock<IUserService> _userServiceMock;

        public BlogPostsControllerTests()
        {

            _blogServiceMock = new Mock<IBlogService>();
            _mapperMock = new Mock<IMapper>();
            _likeServiceMock = new Mock<ILikeService>();
            _userServiceMock = new Mock<IUserService>();

            _controller = new BlogPostsController(
                _blogServiceMock.Object,
                _mapperMock.Object,
                _likeServiceMock.Object,
                _userServiceMock.Object
            );

        }

        [Fact]
        public async Task CreateBlogPost_ValidBlogPost_ReturnsOk()
        {

            var blogDto = new BlogPostDto
            {
                Title = "Sample Blog Post",
                Content = "This is the content of the blog post.",
            };

            var blogPost = new BlogPost
            {
                Id = 1, 
                Title = "Sample Blog Post",
                Content = "This is the content of the blog post.",
               
            };

            _mapperMock.Setup(m => m.Map<BlogPostDto, BlogPost>(blogDto))
                .Returns(blogPost);

            _blogServiceMock.Setup(bs => bs.CreateBlogPost(blogPost))
                .Returns(blogPost); 

            var result = await _controller.CreateBlogPost(blogDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseMessage = Assert.IsType<string>(okResult.Value);
            Assert.Equal("Success", responseMessage);

        }


        [Fact]
        public async Task GetBlogPost_NonExistingId_ReturnsNotFound()
        {

            var nonExistingBlogPostId = 99; 

            _blogServiceMock.Setup(bs => bs.GetBlogDataById(nonExistingBlogPostId))
                .ReturnsAsync((BlogPost)null); 

            var result = await _controller.GetBlogPost(nonExistingBlogPostId);

            Assert.IsType<NotFoundResult>(result);

        }

        

        [Fact]
        public async Task UpdateBlogPost_ValidData_ReturnsNoContent()
        {

            var postId = 1; 
            var updateDto = new BlogPost(); 

            _blogServiceMock.Setup(bs => bs.UpdateBlogPost(postId, updateDto))
                .ReturnsAsync(true); 

            var result = await _controller.UpdateBlogPost(postId, updateDto);

            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public async Task DeleteBlogPost_ValidData_ReturnsNoContent()
        {

            var postId = 1; 

            _blogServiceMock.Setup(bs => bs.DeleteBlogPost(postId))
                .ReturnsAsync(true); 

            var result = await _controller.DeleteBlogPost(postId);

            Assert.IsType<NoContentResult>(result);

        }

    }
}
