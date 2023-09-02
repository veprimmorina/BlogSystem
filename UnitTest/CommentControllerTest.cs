using AutoMapper;
using BlogSystem.Api.Controllers;
using BlogSystem.Api.DTO;
using BlogSystem.Core.Services;
using Moq;

namespace BlogSystem.Api.Tests
{
    public class CommentControllerTests
    {
        private readonly CommentController _controller;
        private readonly Mock<ICommentService> _commentServiceMock;

        public CommentControllerTests()
        {

            _commentServiceMock = new Mock<ICommentService>();
            
            _controller = new CommentController(
                _commentServiceMock.Object
            );

        }

        
        [Fact]
        public async Task AddReply_ValidData_ReturnsReply()
        {
            var replyDto = new CommentDTO(); 
            var reply = "Reply"; 

            _commentServiceMock.Setup(cs => cs.AddReply(replyDto))
                .ReturnsAsync(reply); 

            var result = await _controller.AddReply(replyDto);

            Assert.Equal("Reply", result);
        }

    }
}
