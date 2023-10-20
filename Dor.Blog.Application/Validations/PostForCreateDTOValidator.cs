using Dor.Blog.Application.DTO;
using FluentValidation;

namespace Dor.Blog.Application.Validations
{
    public class PostForCreateDTOValidator : AbstractValidator<PostForCreateDTO>
    {
        public PostForCreateDTOValidator() { 
            RuleFor(c => c.Title).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(c => c.Content).NotNull().NotEmpty().MinimumLength(10);
            RuleFor(c => c.UserId).NotNull().NotEmpty().MinimumLength(10);
        }
    }
}
