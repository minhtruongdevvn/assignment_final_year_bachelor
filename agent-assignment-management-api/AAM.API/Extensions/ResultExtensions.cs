﻿namespace AAM.API;

public static class ResultExtensions
{
    public static Result WithMessage(this Result result, string message)
    {
        return result;
    }

    public static Result WithError(this Result result, string error)
    {
        result.AddError(error);
        return result;
    }

    public static Result WithError(this Result result, string error, ErrorType code)
    {
        result.AddError(error, code);
        return result;
    }

    public static Result WithErrors(this Result result, List<IResultError> errors)
    {
        result.AddErrors(errors);
        return result;
    }

    public static Result WithErrors(this Result result, List<ResultError> errors)
    {
        result.AddErrors(errors);
        return result;
    }

    public static Result Failed(this Result result)
    {
        result.Succeeded = false;
        return result;
    }

    public static Result Successful(this Result result)
    {
        result.Succeeded = true;
        return result;
    }

    public static Result<T> WithMessage<T>(this Result<T> result, string message)
    {
        return result;
    }

    public static Result<T> WithData<T>(this Result<T> result, T data)
    {
        result.Data = data;
        return result;
    }

    public static Result<T> WithError<T>(this Result<T> result, string error)
    {
        result.AddError(error);
        return result;
    }

    public static Result<T> WithErrors<T>(this Result<T> result, List<ResultError> errors)
    {
        result.AddErrors(errors);
        return result;
    }

    public static Result<T> WithError<T>(this Result<T> result, string error, ErrorType code)
    {
        result.AddError(error, code);
        return result;
    }

    public static Result<T> Failed<T>(this Result<T> result)
    {
        result.Succeeded = false;
        return result;
    }

    public static Result<T> Successful<T>(this Result<T> result)
    {
        result.Succeeded = true;
        return result;
    }
}

