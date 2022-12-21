using Hangfire;
using Hangfire.MemoryStorage;

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
	.AddDbContextExt()
	.AddAuthExt()
	.AddCorsExt()
	.AddControllerExt()
	.AddSwaggerGenExt()
	.AddInjectionsExt()
	.AddHangfire(c => c.UseMemoryStorage())
	.AddHangfireServer();

// pipelines
var app = builder.Build();
LoggerHelpers.ServiceProvider = app.Services;

app.UseHstsExt()
	.UseSwaggerExt()
	.UseExceptionHandler(CustomExceptionHandler.ActionBuilder)
	.UseCors("AllowSpecificOrigin")
	.UseRouting()
	.UseAuthentication()
	.UseAuthorization()
	.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
