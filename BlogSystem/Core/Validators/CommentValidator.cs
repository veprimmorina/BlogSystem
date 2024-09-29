using BlogSystem.Core.DTO;
using FluentValidation;

namespace BlogSystem.Core.Validators

{
    public class CommentValidator : AbstractValidator<CommentDto>
    {
        public CommentValidator()
        {
            RuleFor(dto => dto.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(dto => dto.AuthorName).NotEmpty().WithMessage("Author name is required.");
        }

    }
}
