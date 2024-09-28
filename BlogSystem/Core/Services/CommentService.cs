using AutoMapper;
using BlogSystem.Core.DTO;
using BlogSystem.Core.Models;
using BlogSystem.Infrastructure.Repositories;
using FluentValidation;

namespace BlogSystem.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IEmailService _emailService;
        private readonly IValidator<CommentDTO> _commentValidator;
        private readonly IUserService _userService;
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IEmailService emailService, IUserService userService, IValidator<CommentDTO> commentValidator, IBlogService blogService, IMapper mapper)
        {

            _commentRepository = commentRepository;
            _emailService = emailService;
            _userService = userService;
            _blogService = blogService;
            _commentValidator = commentValidator;
            _mapper = mapper;

        }

        public async Task<CommentDTO> CreateComment(CommentDTO userComment)
        {

            var comment = _mapper.Map<Comment>(userComment);

            validateBlogPost(userComment);

            await _commentRepository.Add(comment);

            var creatorEmail = await getUserEmail(comment.BlogPostId);

            notifyUser(creatorEmail, "Someone commented on your post", "Someone commented on your post : " + comment.Content);
            
            return _mapper.Map<CommentDTO>(comment);

        }

        public bool validateBlogPost(CommentDTO comment)
        {

            var validationResult = _commentValidator.Validate(comment);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return true;

        }

        public async Task<String> AddReply(CommentDTO replyDto)
        {

            var reply = _mapper.Map<Comment>(replyDto);
       
                if (await _commentRepository.checkIfExists(replyDto.ParentCommentId) == false)
                {
                    throw new Exception("Parent comment not found");
                }

            reply.ParentCommentId = replyDto.ParentCommentId;
            var createdReply = await _commentRepository.Add(reply);

            var user = await getCommentById(replyDto.ParentCommentId);
            
            notifyUser(user.Email, "Someone replied to your comment" , "Someone replied to your comment : " + createdReply.Content);
            return user.Email;

        }

        public void notifyUser(string email, string subject, string content) {

            _emailService.notify(email, subject, content);

        }

        public async Task<string> getUserEmail(int id)
        {

            var blogPost = await _blogService.GetBlogDataById(id);
            var creatorEmail = await _userService.GetUserEmailById(blogPost.CreatorId);
            return creatorEmail;

        }

        public async Task<IEnumerable<CommentDTO>> GetAllComments()
        {

            var comments = await _commentRepository.GetAllComments();

            var CommentDTOs = _mapper.Map<IEnumerable<CommentDTO>>(comments);

            return CommentDTOs;

        }

        public Task<Comment> getCommentById(int? commentId)
        {

            return _commentRepository.GetById(commentId);

        }

        public async Task sendEmailNotification(string email, string subject, string body)
        {

            _emailService.notify(email, subject, body);

        }

    }

}
