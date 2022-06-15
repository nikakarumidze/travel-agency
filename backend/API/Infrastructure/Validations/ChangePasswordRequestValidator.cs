using System.Text.RegularExpressions;
using API.Infrastructure.APIResources;
using API.Models;
using API.Models.UserRequests;
using API.Models.UserRequests.UserRequestModels;
using FluentValidation;

namespace API.Infrastructure.Validations;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequestModel>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.Username)
            .Matches(Regexes.Name)
            .WithMessage(ErrorMessages.InvalidUsername);
        RuleFor(x => x.NewPassword)
            .Matches(Regexes.Password)
            .WithMessage(ErrorMessages.InvalidPassword);
        RuleFor(x => x.OldPassword)
            .Matches(Regexes.Password)
            .WithMessage(ErrorMessages.InvalidPassword);
    }
}