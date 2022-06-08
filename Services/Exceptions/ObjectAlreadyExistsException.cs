using Services.Localisations;

namespace Services.Exceptions;

public class ObjectAlreadyExistsException : Exception
{
    public readonly string Code = ExceptionMessages.ObjectAlreadyExists;
    public ObjectAlreadyExistsException(string message) : base(message) { }

}