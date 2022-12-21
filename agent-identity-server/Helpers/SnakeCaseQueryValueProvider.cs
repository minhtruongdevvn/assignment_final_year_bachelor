using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgentIdentityServer.Helpers;

public class SnakeCaseQueryValueProvider : QueryStringValueProvider, IValueProvider
{
	public SnakeCaseQueryValueProvider(
		BindingSource bindingSource,
		IQueryCollection values,
		CultureInfo culture
	) : base(bindingSource, values, culture) { }

	public override bool ContainsPrefix(string prefix) => base.ContainsPrefix(prefix.ToSnakeCase());

	public override ValueProviderResult GetValue(string key) => base.GetValue(key.ToSnakeCase());
}

public class SnakeCaseQueryValueProviderFactory : IValueProviderFactory
{
	public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
	{
		if (context == null)
			throw new ArgumentNullException(nameof(context));

		var valueProvider = new SnakeCaseQueryValueProvider(
			BindingSource.Query,
			context.ActionContext.HttpContext.Request.Query,
			CultureInfo.CurrentCulture
		);

		context.ValueProviders.Add(valueProvider);

		return Task.CompletedTask;
	}
}
