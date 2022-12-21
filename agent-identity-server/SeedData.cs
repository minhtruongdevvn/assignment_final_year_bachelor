using Microsoft.Extensions.Logging;
using System.Globalization;

namespace AgentIdentityServer;

public class SeedData
{
	private static readonly IServiceCollection _services = new ServiceCollection();
	private static readonly ILogger _logger = LoggerFactory.Create(builder =>
		builder.AddConsole().AddEventLog().SetMinimumLevel(LogLevel.Debug)
	).CreateLogger("SeedData");

	private static readonly IMapper _mapper = new MapperConfiguration(cfg =>
	{
		cfg.AddProfile<ClientMapperProfile>();
		cfg.AddProfile<ScopeMapperProfile>();
		cfg.AddProfile<ApiResourceMapperProfile>();
		cfg.AddProfile<IdentityResourceMapperProfile>();
	}).CreateMapper();

	public static void EnsureSeedData(string connectionString, string seed = "")
	{
		_services
			.AddLogging()
			.AddIdentityExt(AppSettings.DbConnectionString)
			.AddOperationalDbContext(
				opt =>
					opt.ConfigureDbContext = b =>
						b.UseSqlServer(
							connectionString,
							sqlOpt => sqlOpt.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
						)
			)
			.AddConfigurationDbContext(
				opt =>
					opt.ConfigureDbContext = b =>
						b.UseSqlServer(
							connectionString,
							sqlOpt => sqlOpt.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
						)
			);

		using var scope = _services
			.BuildServiceProvider()
			.GetRequiredService<IServiceScopeFactory>()
			.CreateScope();

		var persistContext = scope.ServiceProvider.GetService<PersistedGrantDbContext>();
		var confContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();
		var idContext = scope.ServiceProvider.GetService<AspNetIdentityDbContext>();

		persistContext.ThrowIfNull("PersistedGrant context is empty").Value.Database.Migrate();
		confContext.ThrowIfNull("Configuration context is empty").Value.Database.Migrate();
		idContext.ThrowIfNull("AspNetIdentity context is empty").Value.Database.Migrate();

		EnsureSeedData(confContext, seed);
		EnsureRoles(idContext);
		EnsureUsers(scope);
	}

	private static void EnsureRoles(AspNetIdentityDbContext context)
	{
		var roles = new List<AppRole>
		{
			GetRole("operator", "Responsible for managing system"),
			GetRole("agent", "Enrolled into the training and assignment system"),
			GetRole("lecturer", "Responsible for conduct the courses and training agent"),
		};

		using var trans = context.Database.BeginTransaction();
		roles.ForEach(role =>
		{
			if (!context.Roles.AsNoTracking().Any(_ => _.Name == role.Name))
			{
				_logger.Debug($"Creating role \"{role.Name}\"");
				context.Roles.Add(role);
				context.SaveChanges();
			}
			else
			{
				_logger.Debug($"Role \"{role.Name}\" is already exists");
			}
		});
		trans.Commit();

		static AppRole GetRole(string name, string des)
		{
			return new AppRole
			{
				Name = name,
				Description = des,
				NormalizedName = name.ToUpper(CultureInfo.InvariantCulture)
			};
		}
	}

	private static void EnsureUsers(IServiceScope scope)
	{
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
		userManager.ThrowIfNull();

		InitUser("admin@dev.utc.ac", "admin", "Admin0");

		void InitUser(
			string email,
			string? username = null,
			string password = "Agent@123",
			string role = "operator"
		)
		{
			_logger.Debug($"Creating user \"{username ??= email}\"");

			if (userManager.FindByNameAsync(username).Result == null)
			{
				var user = new AppUser
				{
					Email = email,
					EmailConfirmed = true,
					UserName = username,
				};

				var result = userManager.CreateAsync(user, password).Result;
				result.Succeeded
					.Throw(result.Errors.FirstOrDefault()?.Description + "at User \"{username}\"")
					.IfFalse();

				result = userManager
					.AddClaimsAsync(
						user,
						new Claim[]
						{
							new(JwtClaimTypes.Role, role),
							new(JwtClaimTypes.Email, email),
							new(JwtClaimTypes.Name, username),
						}
					)
					.Result;
				result.Succeeded
					.Throw(result.Errors.FirstOrDefault()?.Description + "at User \"{username}\"")
					.IfFalse();

				userManager.AddToRoleAsync(user, role);
			}
			else
			{
				_logger.Debug($"User \"{username}\" is already exists");
			}
		}
	}

	private static void EnsureSeedData(ConfigurationDbContext context, string seed = "")
	{
		try
		{
			var addedClients = new List<Client>();
			var addedApiResources = new List<ApiResource>();
			var addedApiScopes = new List<ApiScope>();

			if (!seed.IsNullOrEmpty())
			{
				var dict = seed.Split(';')
					.Select(x => x.Split("~~"))
					.ToDictionary(x => x[0], x => x[1]);

				AddMatchingKey(dict, addedClients, "Clients");
				AddMatchingKey(dict, addedApiScopes, "ApiScopes");
				AddMatchingKey(dict, addedApiResources, "ApiResources");
			}

			PopulateConfigs(context, context.Clients, Config.Clients, addedClients);
			PopulateConfigs(context, context.ApiScopes, Config.ApiScopes, addedApiScopes);
			PopulateConfigs(context, context.ApiResources, Config.ApiResources, addedApiResources);
			PopulateConfigs(context, context.IdentityResources, Config.IdentityResources);
		}
		catch (Exception ex)
		{
			_logger.Error($"EnsureSeed: {ex.Message}");
			throw;
		}
	}

	private static void PopulateConfigs<TEntity, TEntityConfig>(
		ConfigurationDbContext context,
		DbSet<TEntity> entities,
		IEnumerable<TEntityConfig> entityConfigs,
		IReadOnlyCollection<TEntityConfig>? currentResources = null
	) where TEntity : class
	{
		var name = $"{typeof(TEntity).Name}s";

		_logger.Debug($"Populating \"{name}\"...");
		if (!entities.Any())
		{
			var resources = entityConfigs.ToList();
			if (currentResources != null && currentResources.Any())
				resources.AddRange(currentResources);

			resources.ForEach(res => entities.Add(_mapper.Map<TEntity>(res)));
			context.SaveChanges();
		}
		else
		{
			_logger.Debug($"\"{name}\" is already populated");
		}
	}

	private static void AddMatchingKey<T>(
		IReadOnlyDictionary<string, string> dict,
		List<T> list,
		string key
	)
	{
		if (!dict.ContainsKey(key))
			list.AddRange(dict[key].Split(",,").Select(NtsJson.JsonConvert.DeserializeObject<T>)!);
	}
}
