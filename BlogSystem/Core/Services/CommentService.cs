using AutoMapper;
using BlogSystem.Core.DTO;
using BlogSystem.Core.Interfaces.Repositories;
using BlogSystem.Core.Interfaces.Service;
using BlogSystem.Core.Models;
using FluentValidation;

namespace BlogSystem.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IEmailService _emailService;
        private readonly IValidator<CommentDto> _commentValidator;
        private readonly IUserService _userService;
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IEmailService emailService, IUserService userService, IValidator<CommentDto> commentValidator, IBlogService blogService, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _emailService = emailService;
            _userService = userService;
            _blogService = blogService;
            _commentValidator = commentValidator;
            _mapper = mapper;
        }

        public async Task<CommentDto> CreateComment(CommentDto userComment)
        {
            var comment = _mapper.Map<Comment>(userComment);
            ValidateBlogPost(userComment);

            await _commentRepository.Add(comment);

            var creatorEmail = await GetUserEmail(comment.BlogPostId);
            NotifyUser(creatorEmail, "Someone commented on your post", "Someone commented on your post : " + comment.Content);
            
            return _mapper.Map<CommentDto>(comment);
        }

        public bool ValidateBlogPost(CommentDto comment)
        {
            var validationResult = _commentValidator.Validate(comment);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return true;
        }

        public async Task<string> AddReply(CommentDto replyDto)
        {
            if (replyDto == null)
            {
                return "Reply can not be null!";
            }

            var reply = _mapper.Map<Comment>(replyDto);
       
            if (await _commentRepository.checkIfExists(replyDto.ParentCommentId) == false)
            {
                return "Parent comment not found";
            }

            reply.ParentCommentId = replyDto.ParentCommentId;
            var createdReply = await _commentRepository.Add(reply);

            var user = await GetCommentById(replyDto.ParentCommentId);
            
            NotifyUser(user.Email, "Someone replied to your comment" , "Someone replied to your comment : " + createdReply.Content);
            return user.Email;

        }

        public void NotifyUser(string email, string subject, string content)
        {
            _emailService.Notify(email, subject, content);
        }

        public async Task<string> GetUserEmail(int id)
        {
            var blogPost = await _blogService.GetBlogDataById(id);
            var creatorEmail = await _userService.GetUserEmailById(blogPost.CreatorId);

            return creatorEmail;

        }

        public async Task<IEnumerable<CommentDto>> GetAllComments()
        {
            var comments = await _commentRepository.GetAllComments();
            var commentList = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return commentList;

        }

        public Task<Comment> GetCommentById(int? commentId)
        {
            return _commentRepository.GetById(commentId);
        }

        public async Task sendEmailNotification(string email, string subject, string body)
        {
            _emailService.Notify(email, subject, body);
        }
    }
}
