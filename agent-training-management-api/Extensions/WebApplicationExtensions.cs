namespace AtmAPI.Extensions;

public static class WebApplicationExtensions
{
	public static IApplicationBuilder UseSwaggerExt(this IApplicationBuilder app)
	{
		if (IsDev)
			app.UseSwagger().UseSwaggerUI();
		return app;
	}

	public static IApplicationBuilder UseHstsExt(this IApplicationBuilder app)
	{
		if (!IsDev)
			app.UseHsts();
		return app;
	}
}
