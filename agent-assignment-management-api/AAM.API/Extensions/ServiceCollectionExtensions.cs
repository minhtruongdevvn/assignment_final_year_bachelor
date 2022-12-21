using AAM.API.Authorization;
using AAM.Application;
using AAM.Infrastructure.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Text;

namespace AAM.API
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureLayer(configuration);
            services.AddHttpContextAccessor();
            services.AddValidatorsFromAssemblyContaining<Program>();
        }
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, AppAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, AgentAppAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, IdentityAuthorizationHandler>();
            services.AddSingleton<AccessTokenProvider>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.Authority = configuration.GetValue<string>("Authentication:IDS_ISSUER");
                    opt.TokenValidationParameters.ValidateIssuer = false;
                    opt.TokenValidationParameters.ValidateAudience = false;
                    opt.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    opt.RequireHttpsMetadata = false;

                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                path.StartsWithSegments("/quest-notify"))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IOperatorService, OperatorService>();
            services.AddScoped<IQuestService, QuestService>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IAgentSkillService, AgentSkillService>();
            services.AddScoped<IAgentQuestService, AgentQuestService>();
            services.AddSignalR();
            services.AddMemoryCache();
        }

        public static IServiceCollection AddAppLogging(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddFile("Logs/assignment_{0:yyyy}-{0:MM}-{0:dd}.json", fileLoggerOpts =>
                {
                    void AddNewValue(
                        JsonTextWriter writer,
                        StringBuilder sb,
                        string? stringValue = null,
                        int? intValue = null
                    )
                    {
                        sb.AppendLine();
                        sb.Append("\t");
                        if (stringValue != null)
                            writer.WriteValue(stringValue);
                        else
                            writer.WriteValue(intValue);
                    }

                    fileLoggerOpts.FilterLogEntry = (msg) => 
                        msg.LogLevel == LogLevel.Error || 
                        msg.LogLevel == LogLevel.Warning;

                    fileLoggerOpts.FormatLogFileName = (fName)
                        => string.Format(fName, DateTime.UtcNow);
                    fileLoggerOpts.Append = false;
                    fileLoggerOpts.FormatLogEntry = (msg) =>
                    {
                        var sb = new StringBuilder();
                        StringWriter sw = new StringWriter(sb);
                        var jsonWriter = new JsonTextWriter(sw);
                        jsonWriter.WriteStartArray();
                        AddNewValue(jsonWriter, sb, DateTime.Now.ToString("o"));
                        AddNewValue(jsonWriter, sb, msg.LogLevel.ToString());
                        AddNewValue(jsonWriter, sb, msg.LogName);
                        AddNewValue(jsonWriter, sb, intValue: msg.EventId.Id);
                        AddNewValue(jsonWriter, sb, msg.Message);
                        AddNewValue(jsonWriter, sb, msg.Exception?.ToString());
                        sb.AppendLine();
                        jsonWriter.WriteEndArray();

                        sb.Append(',');

                        return sb.ToString();
                    };
                });
            });
        }
    }
}