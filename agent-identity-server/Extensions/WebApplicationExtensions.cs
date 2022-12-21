namespace AgentIdentityServer.Extensions;

public static class WebApplicationExtensions
{
	public static IApplicationBuilder UseHstsExt(this IApplicationBuilder app)
	{
		if (!IsDev)
			app.UseHsts();
		return app;
	}

	public static IApplicationBuilder UseRazorPagesExt(this IApplicationBuilder app)
	{
		((WebApplication)app).MapRazorPages().RequireAuthorization();
		return app;
	}
}
