using Services.Localisations;

namespace Services.Exceptions;

public class InvalidCredentialsException : Exception
{
    public readonly string Code = ExceptionMessages.InvalidCredentials;
    public InvalidCredentialsException(string message) : base(message) { }
}