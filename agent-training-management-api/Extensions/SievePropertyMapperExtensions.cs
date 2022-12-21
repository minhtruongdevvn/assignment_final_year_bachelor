namespace AtmAPI.Extensions;

public static class SievePropertyMapperExtensions
{
	public static void MapBaseEntityProps<TEntity>(this SievePropertyMapper map)
		where TEntity : EntityBase
	{
		map.MapProps<TEntity>((p => p.Id, null));
		MapBaseMetadataProps<TEntity>(map);
	}

	public static void MapBaseMetadataProps<TEntity>(this SievePropertyMapper map)
		where TEntity : EntityMetadataBase
	{
		map.MapProps<TEntity>(
			(p => p.CreatedBy, "created_by"),
			(p => p.CreatedAt, "created_at"),
			(p => p.UpdatedBy, "updated_by"),
			(p => p.UpdatedAt, "updated_at")
		);
	}

	public static void MapBaseIdentityProps<TEntity>(this SievePropertyMapper map)
		where TEntity : IdentityBase
	{
		map.MapProps<TEntity>(
			(p => p.Code, null),
			(p => p.Email, null),
			(p => p.UserName, "user_name"),
			(p => p.GivenName, "given_name"),
			(p => p.FamilyName, "family_name"),
			(p => p.BirthDate, "birth_date")
		);
	}

	public static void MapProps<TEntity>(
		this SievePropertyMapper map,
		params (Expression<Func<TEntity, object?>> expression, string? name)[] propsToMap
	)
	{
		foreach (var (expression, name) in propsToMap)
		{
			if (name != null)
				map.Property(expression).CanFilter().CanSort().HasName(name);
			else
				map.Property(expression).CanFilter().CanSort();
		}
	}
}
