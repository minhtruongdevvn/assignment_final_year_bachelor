using System.Net.Mail;

namespace AgentIdentityServer.Extensions;

public static class ValidatableExtensions
{
	public static ref readonly Validatable<string> IfNotValidEmail(
		in this Validatable<string> validatable
	)
	{
		try
		{
			_ = new MailAddress(validatable.Value);
		}
		catch (FormatException)
		{
			ExceptionThrower.Throw(
				validatable.ParamName,
				validatable.ExceptionCustomizations,
				"This email is not valid."
			);
		}
		return ref validatable;
	}
}
