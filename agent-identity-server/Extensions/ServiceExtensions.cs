using Duende.IdentityServer.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace AgentIdentityServer.Extensions;

internal static class ServiceExtensions
{
	private static readonly string? _assembly = typeof(Config).Assembly.GetName().Name;

	public static IServiceCollection AddApplicationInsightsExt(
		this IServiceCollection services,
		string connectionKey
	)
	{
		if (!IsDev)
		{
			services.AddApplicationInsightsTelemetry(
				opt => opt.ConnectionString = AppSettings.GetValue<string>(connectionKey)
			);
		}
		return services;
	}

	public static IServiceCollection AddCorsExt(this IServiceCollection services) =>
		services.AddCors(opt =>
		{
			opt.AddPolicy(
				"AllowSpecificOrigin",
				b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
			);
		});

	public static IServiceCollection AddDuendeExt(
		this IServiceCollection services,
		string connectionString
	)
	{
		services
			.AddIdentityServer(opt =>
			{
				opt.Events.RaiseErrorEvents = true;
				opt.Events.RaiseFailureEvents = true;
				opt.Events.RaiseInformationEvents = true;
				opt.Events.RaiseSuccessEvents = true;

				opt.EmitStaticAudienceClaim = true;
			})
			.AddAspNetIdentity<AppUser>()
			.AddConfigurationStore(
				opt =>
					opt.ConfigureDbContext = b =>
						b.UseSqlServer(
							connectionString,
							sqlOpt =>
							{
								sqlOpt.MigrationsAssembly(_assembly);
								sqlOpt.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
							}
						)
			)
			.AddOperationalStore(opt =>
			{
				opt.EnableTokenCleanup = true;
				opt.ConfigureDbContext = b =>
					b.UseSqlServer(
						connectionString,
						sqlOpt =>
						{
							sqlOpt.MigrationsAssembly(_assembly);
							sqlOpt.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
						}
					);
			})
			.AddProfileService<IdentityProfileService>();
		return services;
	}

	public static IServiceCollection AddIdentityExt(
		this IServiceCollection services,
		string connectionString
	)
	{
		services
			.AddDbContext<AspNetIdentityDbContext>(
				b =>
					b.UseSqlServer(
						connectionString,
						sqlOpt =>
						{
							sqlOpt.MigrationsAssembly(_assembly);
							sqlOpt.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
						}
					)
			)
			.AddIdentity<AppUser, AppRole>(opt =>
			{
				opt.User.RequireUniqueEmail = true;
				opt.Password = new PasswordOptions
				{
					RequiredLength = 6,
					RequireDigit = true,
					RequiredUniqueChars = 0,
					RequireUppercase = true,
					RequireLowercase = true,
					RequireNonAlphanumeric = false
				};
			})
			.AddEntityFrameworkStores<AspNetIdentityDbContext>()
			.AddDefaultTokenProviders();
		return services;
	}

	public static IServiceCollection AddInjectionsExt(
		this IServiceCollection services,
		IConfiguration configuration
	) =>
		services
			// di container
			.AddScoped<ISieveProcessor, IdentitySieveProcessor>()
			// option pattern
			.Configure<SieveOptions>(configuration.GetSection("Sieve"))
			.AddTransient<ICustomTokenRequestValidator, CustomTokenRequestValidator>();

	public static IServiceCollection AddRazorControllerExt(this IServiceCollection services)
	{
		services.AddRazorPages();
		services
			.AddControllers(
				opt => opt.ValueProviderFactories.Add(new SnakeCaseQueryValueProviderFactory())
			)
			.AddNewtonsoftJson(
				opt =>
					opt.SerializerSettings.ContractResolver = new DefaultContractResolver
					{
						NamingStrategy = new SnakeCaseNamingStrategy()
					}
			)
			.ConfigureApiBehaviorOptions(
				opt =>
					opt.InvalidModelStateResponseFactory = ctx =>
					{
						var errors = ctx.ModelState
							.FirstOrDefault()
							.Value?.Errors.Select(i => i.ErrorMessage)
							.ToList();
						errors.ThrowIfNull();

						var result = new ObjectResult(new { messages = errors });
						result.StatusCode = StatusCodes.Status400BadRequest;
						result.ContentTypes.Add(MediaTypeNames.Application.Json);
						return result;
					}
			);
		return services;
	}

	public static IServiceCollection AddAppLogging(this IServiceCollection services)
	{
		return services.AddLogging(loggingBuilder =>
		{
			loggingBuilder.AddConfiguration(AppSettings.GetSection("Logging"));
			loggingBuilder.AddFile("Logs/ids_{0:yyyy}-{0:MM}-{0:dd}.json", fileLoggerOpts =>
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

				fileLoggerOpts.FilterLogEntry = (msg) => msg.LogLevel == LogLevel.Error;
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
