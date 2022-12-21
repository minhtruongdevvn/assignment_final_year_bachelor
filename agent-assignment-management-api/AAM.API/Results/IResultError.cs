namespace AAM.API;

public interface IResultError
{
    string Error { get; }
    ErrorType Code { get; }
}
