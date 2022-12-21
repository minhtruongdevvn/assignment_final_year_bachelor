using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AtmAPI.Helpers;

public class RoutePrefixConvention : IControllerModelConvention
{
	public void Apply(ControllerModel controller)
	{
		foreach (var selector in controller.Selectors)
		{
			// [prefix, parentPrefix, grandpaPrefix,...]
			var prefixes = GetPrefixes(controller.ControllerType);
			if (prefixes.Count == 0)
				continue;

			// combine these prefixes one by one
			var prefixRouteModels = prefixes
				.Select(r => new AttributeRouteModel(new RouteAttribute(r.Prefix)))
				.Aggregate(
					(acc, prefix) =>
						AttributeRouteModel
							.CombineAttributeRouteModel(prefix, acc)
							.ThrowIfNull()
							.Value
				);

			selector.AttributeRouteModel =
				selector.AttributeRouteModel == null
					? selector.AttributeRouteModel = prefixRouteModels
					: AttributeRouteModel.CombineAttributeRouteModel(
						prefixRouteModels,
						selector.AttributeRouteModel
					);
		}
	}

	private static IList<RoutePrefixAttribute> GetPrefixes(Type controllerType)
	{
		var list = new List<RoutePrefixAttribute?>();
		FindPrefixesRec(controllerType, ref list);
		list = list.Where(r => r != null).ToList();
		return list!;

		// find [RoutePrefixAttribute('...')] recursively
		static void FindPrefixesRec(Type type, ref List<RoutePrefixAttribute?> results)
		{
			while (true)
			{
				var prefix = type.GetCustomAttributes(false)
					.OfType<RoutePrefixAttribute>()
					.FirstOrDefault();

				// null is valid because it will seek prefix from parent recursively
				results.Add(prefix);

				var parentType = type.BaseType;
				if (parentType == null)
					return;

				type = parentType;
			}
		}
	}
}
