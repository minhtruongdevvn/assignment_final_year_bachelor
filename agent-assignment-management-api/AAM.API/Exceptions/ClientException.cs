namespace AAM.API;

public class ClientException : Exception
{
    public ErrorType ErrorCode { get; set; }
    public dynamic? Value { get; set; }
    public ClientException() : base() { }
    public ClientException(string message, ErrorType code = ErrorType.CannotExecuteAction) : base(message)
    {
        ErrorCode = code;
    }
    public ClientException(string message, dynamic value, ErrorType code = ErrorType.CannotExecuteAction) : base(message)
    {
        ErrorCode = code;
        Value = value;
    }
}

