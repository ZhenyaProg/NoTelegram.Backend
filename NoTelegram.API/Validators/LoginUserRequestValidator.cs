using FluentValidation;
using NoTelegram.API.Contracts;
using NoTelegram.Core.Models;

namespace NoTelegram.API.Validators
{
    public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(user => user.Password)
                .NotEmpty()
                .MaximumLength(Users.MAX_PASSWORD_LENGTH)
                .WithMessage($"Длина пароля не должна превышать {Users.MAX_PASSWORD_LENGTH} символов");

            RuleFor(user => user.Login)
                .NotEmpty()
                .EmailAddress()
                .When(user => user.LoginType == "email");

            RuleFor(user => user.Login)
                .NotEmpty()
                .MaximumLength(Users.MAX_USER_NAME_LENGTH)
                .When(user => user.LoginType == "un")
                .WithMessage($"Длина имени не должна превышать {Users.MAX_USER_NAME_LENGTH} символов");
        }
    }
}