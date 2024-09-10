using FluentValidation;
using NoTelegram.API.Contracts.User;
using NoTelegram.Core.Models;

namespace NoTelegram.API.Validators.User
{
    public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
    {
        public EditUserRequestValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty()
                .MaximumLength(Users.MAX_USER_NAME_LENGTH)
                .WithMessage($"Длина имени не должна превышать {Users.MAX_USER_NAME_LENGTH} символов");

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}