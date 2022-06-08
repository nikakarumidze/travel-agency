using API.Models;
using FluentValidation;

namespace API.Infrastructure.Validations;

public class UserLogInRequestValidator : AbstractValidator<LogInRequestModel>
{
    public UserLogInRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty();
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}