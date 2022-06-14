using API.Models;
using API.Models.UserRequests;
using API.Models.UserRequests.UserRequestModels;
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