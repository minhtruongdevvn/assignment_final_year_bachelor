namespace AgentIdentityServer.Middlewares;

public static class CSPPolicyResponseMiddlewareExt
{
	public static IApplicationBuilder UseCSPPolicy(this IApplicationBuilder app) =>
		app.UseMiddleware<CSPPolicyResponse>();
}

public class CSPPolicyResponse
{
	private readonly RequestDelegate _next;

	public CSPPolicyResponse(RequestDelegate next) => _next = next;

	public async Task InvokeAsync(HttpContext context)
	{
		var policies = new[]
		{
			"default-src * self blob: data: gap:",
			"style-src * self 'unsafe-inline' blob: data: gap:",
			"script-src * 'self' 'unsafe-eval' 'unsafe-inline' blob: data: gap:",
			"connect-src self * 'unsafe-inline' blob: data: gap:"
		};
		context.Response.Headers.Add("Content-Security-Policy", string.Join("; ", policies));
		await _next(context);
	}
}
