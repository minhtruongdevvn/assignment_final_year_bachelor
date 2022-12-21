using AgentIdentityServer.Extensions;

var builder = WebApplication.CreateBuilder(args);
AppSettings.Config = builder.Configuration;

// hosts
builder.Logging.ClearProviders();
if (builder.Environment.IsDevelopment())
{
	builder.Logging
		.AddConsole()
		.AddDebug();
}

// services
builder.Services
	.AddAppLogging()
	.AddApplicationInsightsExt("ApplicationInsights:ConnectionString")
	.AddIdentityExt(AppSettings.DbConnectionString)
	.AddDuendeExt(AppSettings.DbConnectionString)
	.AddLocalApiAuthentication()
	.AddInjectionsExt(builder.Configuration)
	.AddCorsExt()
	.AddRazorControllerExt();

var app = builder.Build();
LoggerHelpers.ServiceProvider = app.Services;

// pipelines
app.UseHstsExt()
	.UseRazorPagesExt()
	.UseExceptionHandler(CustomExceptionHandler.ActionBuilder)
	.UseHttpsRedirection()
	.UseStaticFiles()
	.UseRouting()
	.UseCors("AllowSpecificOrigin")
	.UseAuthentication()
	.UseAuthorization()
	.UseIdentityServer()
	.UseCSPPolicy()
	.UseEndpoints(endpoints => endpoints.MapControllers());

if (args.Contains("/seed"))
{
	SeedData.EnsureSeedData(AppSettings.DbConnectionString);
	return;
}

if (args.Contains("/seed-connection"))
{
	var paramList = args.Last().Split(".~");
	if (paramList.Length > 1)
		SeedData.EnsureSeedData(paramList[0], paramList[1]);
	else
		SeedData.EnsureSeedData(paramList[0]);
	return;
}

// init
app.Run();
