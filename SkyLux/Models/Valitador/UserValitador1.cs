using FluentValidation;

namespace SkyLux.Models.Valitador
{
    public class UserValitador1 : AbstractValidator<UserLogin>
    {
        public UserValitador1()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.UserPassword).NotEmpty().WithMessage("UserPassword cannot be empty");
        }
    }
}
