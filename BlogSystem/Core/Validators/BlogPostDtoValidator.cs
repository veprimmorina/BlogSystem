using BlogSystem.Core.DTO;
using FluentValidation;

namespace BlogSystem.Core.Validators
{
    public class BlogPostDtoValidator : AbstractValidator<BlogPostDto>
    {
        public BlogPostDtoValidator()
        {
            RuleFor(dto => dto.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(dto => dto.Content).NotEmpty().WithMessage("Content is required.");
        }
    }
}
