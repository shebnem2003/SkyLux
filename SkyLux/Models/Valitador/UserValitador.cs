using FluentValidation;
using SkyLux.Models;

namespace Proje.Models.Valitador
{
    public class UserValitador : AbstractValidator<User>
    {
        public UserValitador()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.UserSurname).NotEmpty().WithMessage("UserSurname cannot be empty");
            RuleFor(x => x.UserPassword).NotEmpty().WithMessage("UserPassword cannot be empty");
        }
    }
}
