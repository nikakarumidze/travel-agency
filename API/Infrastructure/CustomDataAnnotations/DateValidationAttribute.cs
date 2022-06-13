using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API.Infrastructure.CustomDataAnnotations;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class DateValidationAttribute : ValidationAttribute, IClientModelValidator
{
    public DateValidationAttribute(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public override bool IsValid(object value)
    {
        var datetime = (DateTime)value;
        return datetime > DateTime.Now && datetime < DateTime.Now.AddYears(5);
    }
    public void AddValidation(ClientModelValidationContext context)
    {
        var error = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
        context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-val-error", error);
    }
}