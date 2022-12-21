using Microsoft.Extensions.Options;

namespace AgentIdentityServer.Helpers;

public class IdentitySieveProcessor : SieveProcessor
{
	public IdentitySieveProcessor(IOptions<SieveOptions> options) : base(options) { }

	protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
	{
		// AppRole
		mapper.Property<AppRole>(p => p.Name).CanSort().CanFilter();

		// AppUser
		mapper.Property<AppUser>(p => p.Id).CanSort();
		mapper.Property<AppUser>(p => p.UserName).CanSort().CanFilter().HasName("user_name");
		mapper.Property<AppUser>(p => p.Email).CanSort().CanFilter().HasName("email");
		mapper.Property<AppUser>(p => p.EmailConfirmed).CanFilter().HasName("email_confirmed");
		mapper.Property<AppUser>(p => p.PhoneNumber).CanSort().CanFilter().HasName("phone");
		mapper
			.Property<AppUser>(p => p.PhoneNumberConfirmed)
			.CanFilter()
			.HasName("phone_confirmed");

		return mapper;
	}
}
