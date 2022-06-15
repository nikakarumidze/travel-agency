using Services.Localisations;

namespace Services.Exceptions;

public class ApartmentNotAvailableException : Exception
{
    public readonly string Code = ExceptionMessages.ApartmentNotAvailable;
    public ApartmentNotAvailableException(string message) : base(message) { }
}