namespace Domain.Utils;

public record Error
{
    private const string SEPARATOR = "||";
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }
    public string? InvalidField { get; }

    private Error(string code, string message, ErrorType type, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }

    public static Error Validation(string code, string message, string? invalidField = null) =>
        new(code, message, ErrorType.Validation, invalidField);

    public static Error NotFound(string code, string message, string? invalidField = null) =>
        new(code, message, ErrorType.NotFound, invalidField);

    public static Error Conflict(string code, string message, string? invalidField = null) =>
        new(code, message, ErrorType.Conflict, invalidField);
    
    public static Error Forbidden(string code, string message, string? invalidField = null) =>
        new(code, message, ErrorType.Forbidden, invalidField);
    public static Error Failure(string code, string message, string? invalidField = null) =>
        new(code, message, ErrorType.Failure, invalidField);


    public ErrorList ToErrorList() => new([this]);

 
}

public enum ErrorType
{
    Validation,
    
    NotFound,
    Conflict,
    Forbidden,
    Failure
}
