namespace AAM.API;

public class Result : IResult
{
    public List<IResultError> Errors { get; set; } = new List<IResultError>();

    public Result()
    {
        Succeeded = true;
    }

    public Dictionary<string, object> Metadata { get; internal set; } = new Dictionary<string, object>();
    public bool Succeeded { get; internal set; }

    public void AddError(string errorMessage)
    {
        Succeeded = false;
        Errors.Add(new ResultError(errorMessage, ErrorType.CannotExecuteAction));
    }
    public void AddErrors(IEnumerable<IResultError> errors)
    {
        Succeeded = false;
        Errors.AddRange(errors);
    }
    public void AddError(string errorMessage, ErrorType errorCode)
    {
        Succeeded = false;
        Errors.Add(new ResultError(errorMessage, errorCode));
    }

    public void AddMetadata(string key, object value)
    {
        if (Metadata == null)
            Metadata = new Dictionary<string, object>();

        Metadata.Add(key, value);
    }

    public T GetMetadata<T>(string key) where T : struct
    {
        return (T)Metadata[key];
    }
}

public class Result<T> : Result, IResult<T>
{
    public Result() : base() { }

    public T Data { get; set; } = default(T)!;
}
