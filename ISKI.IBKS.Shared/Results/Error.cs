namespace ISKI.IBKS.Shared.Results;

public record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Girdi null olamaz.");
    
    public static Error Create(string code, string message) => new(code, message);
}

public record ValidationError(string PropertyName, string ErrorMessage);
