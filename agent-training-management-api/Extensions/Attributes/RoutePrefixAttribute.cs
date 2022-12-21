namespace AtmAPI.Extensions.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RoutePrefixAttribute : Attribute
{
	public RoutePrefixAttribute(string prefix) => Prefix = prefix;

	public string Prefix { get; }
}
