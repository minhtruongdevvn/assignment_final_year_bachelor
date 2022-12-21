using AAM.Application;
using Flurl;
using Flurl.Http;
using System.Net;

namespace AAM.API;

public class OperatorService : IOperatorService
{
    private readonly AccessTokenProvider _accessTokenProvider;
    private readonly ILogger _logger;

    public OperatorService(AccessTokenProvider accessTokenProvider, ILogger<OperatorService> logger) 
    {
        _accessTokenProvider = accessTokenProvider;
        _logger = logger;
    }

    public async Task<IdentityClientResponse> AddAsync(OperatorDTO operatorDTO)
    {
        operatorDTO.BelongTo = "AAM";
        return await HandleRequestAsync(
            "create",
            @return: true,
            request: (token) => Flurl($"api/users/operator")
                        .WithHeader("Content-Type", "application/json")
                        .WithOAuthBearerToken(token)
                        .PostJsonAsync(operatorDTO.ToSnakeCaseProps())
                        .ReceiveJson()
        );
    }

    private async Task<IdentityClientResponse> HandleRequestAsync<T>(
        string name,
        Func<string?, Task<T?>> request,
        bool @return = false
    )
    {
        try
        {
            var accessToken = await _accessTokenProvider.GetTokenAsync();
            var result = await request(accessToken);
            return new() { StatusCode = HttpStatusCode.OK, Content = @return ? result : null };
        }
        catch (FlurlHttpException ex)
        {
            if (ex.Call.HttpResponseMessage?.StatusCode != HttpStatusCode.BadRequest)
                return new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = await ex.Call.Response.GetJsonListAsync()
                };
            return await ProcessErrorAsync(ex, name);
        }
    }

    private IFlurlRequest Flurl(string appendPath) =>
        new Url(_accessTokenProvider.GetIssuer()).AppendPathSegment(appendPath).WithTimeout(60);

    private Task<IdentityClientResponse> ProcessErrorAsync(FlurlHttpException ex, string action)
    {
        return ex.Call.Response
            .GetStringAsync()
            .ContinueWith(t =>
            {
                if (t.Exception != null)
                    throw t.Exception;

                _logger.LogError(
                    $"Exception when \"{action.ToUpper()}\" user:"
                        + $"{Environment.NewLine} - Status: {ex.StatusCode}"
                        + $"{(!string.IsNullOrEmpty(t.Result) ? $"{Environment.NewLine} - Content: {t.Result}" : "")}"
                );
                return new IdentityClientResponse
                {
                    StatusCode = (HttpStatusCode)ex.Call.Response.StatusCode,
                    Content = t.Result
                };
            });
    }
}

