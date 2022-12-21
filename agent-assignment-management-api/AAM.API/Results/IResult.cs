namespace AAM.API;

public interface IResult
{
    bool Succeeded { get; }
}

public interface IResult<out T> : IResult
{
    T Data { get; }
}
