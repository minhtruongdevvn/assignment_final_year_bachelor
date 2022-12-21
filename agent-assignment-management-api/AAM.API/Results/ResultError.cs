namespace AAM.API;
public class ResultError : IResultError
{
    public string Error { get; set; } = string.Empty;

    public ErrorType Code { get; set; }

    public ResultError()
    {

    }

    public ResultError(string error, ErrorType code)
    {
        Error = error;
        Code = code;
    }

    public override string ToString()
    {
        return $"Error[{Code}]: {Error}";
    }
}
