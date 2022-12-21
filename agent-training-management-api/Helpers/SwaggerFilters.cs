using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AtmAPI.Helpers;

public class SwaggerFilters
{
	public class LowercaseDocument : IDocumentFilter
	{
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			var paths = swaggerDoc.Paths.ToDictionary(
				entry => string.Join('/', entry.Key.Split('/').Select(x => x.ToLower())),
				entry => entry.Value
			);

			swaggerDoc.Paths = new OpenApiPaths();

			foreach (var (key, value) in paths)
			{
				foreach (var param in value.Operations.SelectMany(o => o.Value.Parameters))
					param.Name = param.Name.ToLower();

				swaggerDoc.Paths.Add(key, value);
			}
		}
	}

	public class SnakeCaseParameter : IParameterFilter
	{
		private readonly SnakeCaseNamingStrategy _namingStrategy = new SnakeCaseNamingStrategy();

		public void Apply(OpenApiParameter parameter, ParameterFilterContext context) =>
			parameter.Name = _namingStrategy.GetPropertyName(parameter.Name, false);
	}
}
