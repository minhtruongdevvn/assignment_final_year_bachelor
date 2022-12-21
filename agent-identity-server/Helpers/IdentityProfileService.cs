namespace AgentIdentityServer.Helpers;

public class IdentityProfileService : IProfileService
{
	private readonly UserManager<AppUser> _userManager;

	public IdentityProfileService(UserManager<AppUser> userManager) => _userManager = userManager;

	public async Task GetProfileDataAsync(ProfileDataRequestContext context)
	{
		var user = await _userManager.GetUserAsync(context.Subject);
		var roles = await _userManager.GetRolesAsync(user);

		var claims = new List<Claim> { new Claim(JwtClaimTypes.Email, user.Email), };

		roles.ToList().ForEach(f => claims.Add(new Claim(JwtClaimTypes.Role, f)));

		if (!string.IsNullOrEmpty(user.BelongTo))
			claims.Add(new Claim("belong_to", user.BelongTo));

		context.IssuedClaims.AddRange(claims);
	}

	public async Task IsActiveAsync(IsActiveContext context)
	{
		var sub = context.Subject.GetSubjectId();
		var user = await _userManager.FindByIdAsync(sub);
		context.IsActive = user != null;
	}
}
