using Dor.Blog.Application.DTO;
using FluentValidation;

namespace Dor.Blog.Application.Validations
{
    public  class UserDTOValidator : AbstractValidator<UserDTO>
    {
        /// <summary>
        /// some validations are performed by the usermanager
        /// </summary>
        public UserDTOValidator()
        {
            RuleFor(c => c.RolName).NotNull().NotEmpty();            
        }
    }
}
