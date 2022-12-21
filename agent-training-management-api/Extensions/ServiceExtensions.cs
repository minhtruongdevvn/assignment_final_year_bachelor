using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;

namespace AtmAPI.Extensions;

public static class ServiceExtensions
{
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

	public static IServiceCollection AddAuthExt(this IServiceCollection services)
	{
		services
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(opt =>
			{
				opt.Authority = ExternalService.Ids.Issuer;

				opt.TokenValidationParameters.ValidateAudience = false;
				opt.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
				opt.RequireHttpsMetadata = false;

				opt.Events = new JwtBearerEvents
				{
					OnChallenge = ctx =>
					{
						ctx.HandleResponse();
						ctx.Response.ContentType = "application/json";
						ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
						return ctx.Response.WriteAsync(
							JsonConvert.SerializeObject(
								new { messages = "Unauthorized to access this resource." }
							)
						);
					}
				};
			});
		services.AddAuthorization();
		return services;
	}

	public static IServiceCollection AddControllerExt(this IServiceCollection services)
	{
		services
			.AddControllers(opt =>
			{
				opt.ValueProviderFactories.Add(new SnakeCaseQueryValueProviderFactory());
				opt.Conventions.Add(new RoutePrefixConvention());
			})
			.AddControllersAsServices()
			.ConfigureApiBehaviorOptions(
				opt =>
					opt.InvalidModelStateResponseFactory = ctx =>
					{
						var errors = ctx.ModelState
							.SelectMany(ms => ms.Value?.Errors ?? new())
							.Select(e => e.ErrorMessage)
							.ToList();
						errors.ThrowIfNull();

						var result = new ObjectResult(new { messages = errors });
						result.StatusCode = StatusCodes.Status400BadRequest;
						result.ContentTypes.Add(MediaTypeNames.Application.Json);
						return result;
					}
			)
			.AddNewtonsoftJson(opt =>
			{
				opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				opt.SerializerSettings.ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new SnakeCaseNamingStrategy()
				};
			});
		return services;
	}

	public static IServiceCollection AddCorsExt(this IServiceCollection services)
	{
		return services.AddCors(opt =>
		{
			opt.AddPolicy(
				"AllowSpecificOrigin",
				b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
			);
		});
	}

	public static IServiceCollection AddDbContextExt(this IServiceCollection services)
	{
		return services.AddDbContext<AtmContext>(
			opt =>
				opt.UseSqlServer(
					AppSettings.DbConnectionString,
					sqlOpt => sqlOpt.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds)
				)
		);
	}

	public static IServiceCollection AddSwaggerGenExt(this IServiceCollection services)
	{
		return services
			.AddSwaggerGen(opt =>
			{
				opt.OrderActionsBy(apiDesc => $"{apiDesc.RelativePath}");
				opt.DocumentFilter<SwaggerFilters.LowercaseDocument>();
				opt.ParameterFilter<SwaggerFilters.SnakeCaseParameter>();

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				opt.IncludeXmlComments(xmlPath);
			})
			.AddSwaggerGenNewtonsoftSupport();
	}

	public static IServiceCollection AddInjectionsExt(this IServiceCollection services)
	{
		return services
			// option pattern
			.Configure<SieveOptions>(AppSettings.GetSection("Sieve"))
			// di container
			.AddTransient<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>()
			.AddSingleton<IAuthorizationHandler, RoleHandler>()
			.AddSingleton<IAuthorizationHandler, ManageHandler>()
			.AddScoped<ISieveProcessor, AtmSieveProcessor>()
			.AddScoped<IUnitOfWork, UnitOfWork>()
			.AddScoped<IdentityService>()
			.AddScoped<AssignmentService>()
			.AddSingleton<AccessTokenProvider>()
			// others
			.AddAutoMapper(typeof(MappingProfile).Assembly)
			.AddHttpContextAccessor()
			.AddMemoryCache();
	}

	public static IServiceCollection AddAppLogging(this IServiceCollection services)
	{
		return services.AddLogging(loggingBuilder =>
		{
			loggingBuilder.AddConfiguration(AppSettings.GetSection("Logging"));
			loggingBuilder.AddFile(
				"Logs/training_{0:yyyy}-{0:MM}-{0:dd}.json",
				fileLoggerOpts =>
				{
					void AddNewValue(
						JsonTextWriter writer,
						StringBuilder sb,
						string? stringValue = null,
						int? intValue = null
					)
					{
						sb.AppendLine();
						sb.Append('\t');
						if (stringValue != null)
							writer.WriteValue(stringValue);
						else
							writer.WriteValue(intValue);
					}

					fileLoggerOpts.FilterLogEntry = (msg) =>
						msg.LogLevel is LogLevel.Error or LogLevel.Warning;
					fileLoggerOpts.FormatLogFileName = (fName) =>
						string.Format(fName, DateTime.UtcNow);
					fileLoggerOpts.Append = false;
					fileLoggerOpts.FormatLogEntry = (msg) =>
					{
						var sb = new StringBuilder();
						var sw = new StringWriter(sb);
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
				}
			);
		});
	}
}
