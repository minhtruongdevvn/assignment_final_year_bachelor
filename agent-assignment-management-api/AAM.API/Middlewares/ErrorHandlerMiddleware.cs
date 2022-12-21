using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;

namespace AAM.API;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Result();
            switch (error)
            {
                case ClientException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.WithError(e.Message, e.ErrorCode);
                    break;
                case SqlException e:
                    // custom application error
                    if (e.Procedure == "GetDataForPredictor")
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.WithError(e.Message, ErrorType.InvalidOperation);
                    }
                    else {
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    }
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    LoggerExtension.Logger.LogError(error.ToString());
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel);
            await response.WriteAsync(result);
        }
    }
}
