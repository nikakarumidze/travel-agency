using Services.Localisations;

namespace Services.Exceptions;

public class NotFoundException : Exception
{
    public readonly string Code = ExceptionMessages.ObjectNotFound;
    public NotFoundException(string message) : base(message) { }
}