using API.Infrastructure.APIResources;
using API.Models;
using FluentValidation;

namespace API.Infrastructure.Validations;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequestModel>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.UserName)
            .Matches(Regexes.Name)
            .WithMessage(ErrorMessages.InvalidUsername);
        RuleFor(x => x.Password)
            .Matches(Regexes.Password)
            .WithMessage(ErrorMessages.InvalidPassword);
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage(ErrorMessages.InvalidEmail);
        RuleFor(x => x.Firstname)
            .Matches(Regexes.Name)
            .WithMessage(ErrorMessages.InvalidName);
        RuleFor(x => x.Lastname)
            .Matches(Regexes.Name);
    }
}