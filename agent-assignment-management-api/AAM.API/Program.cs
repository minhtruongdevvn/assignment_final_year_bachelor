using AAM.AgentSuggestion;
using AAM.AgentSuggestion.Interfaces;
using AAM.API;
using AAM.Application;
using AAM.Infrastructure.DbContexts.Initializer;
using AAM.Infrastructure.Extensions;
using Hangfire;
using Hangfire.MemoryStorage;

if (args.Contains("/dbchanges"))
{
    // perform migration and seeding to the database
    // command: dotnet run --project .\AAM.API /dbchanges
    var dbChangeBuilder = WebApplication.CreateBuilder(args);
    dbChangeBuilder.Services.AddInfrastructure(dbChangeBuilder.Configuration);
    var dbInitializer = dbChangeBuilder.Build().Services.CreateScope()
        .ServiceProvider.GetRequiredService<IDbInitializerService>();
    dbInitializer.Init();
    Environment.Exit(0);
}

var builder = WebApplication.CreateBuilder(args);
var conf = builder.Configuration;

// hosts
builder.Logging.ClearProviders();
if (builder.Environment.IsDevelopment())
{
    builder.Logging
        .AddConsole()
        .AddDebug();
}

builder.Services.AddServices();
builder.Services.AddApplicationLayer(conf);
builder.Services.AddAuth(conf);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(conf);
builder.Services.AddPredictor();
builder.Services.AddHostedService<AAMHostService>();
builder.Services.AddAppLogging(conf);
builder.Services.AddHangfire(c => c.UseMemoryStorage());
builder.Services.AddHangfireServer();

var app = builder.Build();
LoggerExtension.ServiceProvider = app.Services;

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<HubService>("/quest-notify");

app.Run();