using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AtmAPI.Extensions;

public static class TypeExtensions
{
	private static readonly Regex MatchOnlyWhitespaces = new(@"\s+");
	private static readonly JsonSerializerSettings SnakeCaseSettings =
		new()
		{
			Formatting = Formatting.Indented,
			ContractResolver = new DefaultContractResolver
			{
				NamingStrategy = new SnakeCaseNamingStrategy()
			}
		};

	/// <summary>
	/// 	Get the description attribute of an Enum value
	/// </summary>
	public static string GetDescription(this Enum value)
	{
		var valueInfo = value.GetType().GetMember(value.ToString()).FirstOrDefault();
		return valueInfo?.GetCustomAttribute<DescriptionAttribute>()?.Description
			?? value.ToString();
	}

	/// <summary>
	/// 	Return given string with snake case naming convention
	/// </summary>
	public static string ToSnakeCase(this string? str) =>
		str != null
			? new DefaultContractResolver()
			{
				NamingStrategy = new SnakeCaseNamingStrategy()
			}.GetResolvedPropertyName(str)
			: "";

	/// <summary>
	/// 	If the string is null or whitespace, return true.
	/// 	Otherwise, return false.
	/// </summary>
	public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) =>
		string.IsNullOrWhiteSpace(str) && str != "";

	/// <summary>
	/// 	Replace all whitespaces in given string with replacement string.
	/// </summary>
	/// <param name="replacement">The string to replace the whitespaces with.</param>
	public static string ReplaceWhitespaceWith(this string? str, string replacement) =>
		str != null ? MatchOnlyWhitespaces.Replace(str, replacement) : "";

	/// <summary>
	/// 	Convert properties of the given generic object to snake case naming convention
	/// </summary>
	public static object ToSnakeCaseProps<T>(this T obj) where T : class
	{
		obj.ThrowIfNull();

		var snakeCaseJsonString = JsonConvert.SerializeObject(obj, SnakeCaseSettings);
		var snakeCasePropsObject = JsonConvert.DeserializeObject(snakeCaseJsonString);

		snakeCasePropsObject.ThrowIfNull();

		return snakeCasePropsObject;
	}

	/// <summary>
	/// 	Return the next possible date of specified date.
	/// </summary>
	public static DateTime GetNextDay(
		this DateTime currentDate,
		DayOfWeek nextDayOfWeek,
		bool getSameDayOnMatch = false
	)
	{
		if (getSameDayOnMatch && currentDate.DayOfWeek == nextDayOfWeek)
			return currentDate;

		var daysToAdd = ((int)nextDayOfWeek - (int)currentDate.DayOfWeek + 7) % 7;
		return daysToAdd != 0 ? currentDate.AddDays(daysToAdd) : currentDate.AddDays(7);
	}

	/// <summary>
	/// 	If the source is not null, then return true if any of the elements in the
	/// 	source satisfy the predicate.
	/// </summary>
	public static bool IsAny<TSource>(
		[NotNullWhen(true)] this IEnumerable<TSource>? source,
		Func<TSource, bool> predicate
	) => source != null && source.Any(predicate);

	/// <summary>
	/// 	If the source is not null, then return true if any of the elements in the
	/// 	source satisfy the predicate.
	/// </summary>
	public static bool IsAny<TSource>([NotNullWhen(true)] this IEnumerable<TSource>? source) =>
		source != null && source.Any();

	/// <summary>
	/// 	If the includeProperties parameter is not null or empty, then split the string
	/// 	on commas, and for each property, include it in the query. If useSplitQuery is
	/// 	true, then also call AsSplitQuery() on the query.
	/// </summary>
	public static IQueryable<T> IncludesSplitQuery<T>(
		this IQueryable<T> query,
		string? includeProperties = null,
		bool useSplitQuery = true
	) where T : class
	{
		if (includeProperties.IsAny())
		{
			var props = Regex.Split(includeProperties, "(?<!($|[^\\\\])(\\\\\\\\)*?\\\\),\\s*");
			query = props.Aggregate(query, (curr, prop) => curr.Include(prop));

			if (useSplitQuery && props.Length > 1)
				query = query.AsSplitQuery();
		}

		return query;
	}

	/// <summary>
	/// 	Perform callback on <paramref name="source"/>.
	/// 	Alternative way to avoid init variable, and do something with it.
	/// 	In callback function can use both async and sync.
	/// </summary>
	public static async Task CallbackAsync<T>(this T source, Func<T, Task> callback) =>
		await callback(source);

	/// <summary>
	/// 	Perform callback on <paramref name="source"/>.
	/// 	Alternative way to avoid init variable, and do something with it.
	/// 	Should avoid use callback action as async.
	/// </summary>
	public static void Callback<T>(this T source, Action<T> callback) => callback(source);

	/// <summary>
	/// 	Set property of any object
	/// </summary>
	public static void SetProperty(object obj, string property, object value)
	{
		var prop = obj.GetType().GetProperty(property);
		if (prop != null && prop.CanWrite)
		{
			prop.SetValue(obj, value, null);
		}
	}
}
