using API.Infrastructure.APIResources;
using API.Models.UserRequests;
using API.Models.UserRequests.ApartmentRequestModels;
using FluentValidation;

namespace API.Infrastructure.Validations;

public class UpdateApartmentRequestValidator : AbstractValidator<UpdateApartmentRequestModel>
{
    public UpdateApartmentRequestValidator()
    {
        RuleFor(x=>x.CityName)
            .NotNull()
            .WithMessage(ErrorMessages.InputNull);
        RuleFor(x=>x.Address)
            .NotNull()
            .WithMessage(ErrorMessages.InputNull);
        RuleFor(x=>x.BedsNumber)
            .NotNull()
            .WithMessage(ErrorMessages.InputNull);
        RuleFor(x => x.OwnerId)
            .NotNull()
            .WithMessage(ErrorMessages.InputNull);
        RuleFor(x => x.DistanceToCenter)
            .NotNull()
            .WithMessage(ErrorMessages.InputNull);
    }
}