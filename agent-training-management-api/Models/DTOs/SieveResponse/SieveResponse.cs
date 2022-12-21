namespace AtmAPI.Models.DTOs;

public static class ResponseSieve
{
	public static SieveResponse<TResponse> With<TResponse>(
		SieveModel model,
		IEnumerable<TResponse>? list,
		int totalCount
	) where TResponse : class => new SieveResponse<TResponse>().Process(model, list, totalCount);

	public static SieveResponse<TResponse> With<TSource, TResponse>(
		SieveModel model,
		IEnumerable<TSource>? list,
		int totalCount,
		IMapper mapper
	)
		where TResponse : class
		where TSource : class =>
		new SieveResponse<TResponse>().Process<TSource>(model, list, totalCount, mapper);
}

public class SieveResponse<TResponse> where TResponse : class
{
	private readonly SieveOptions _sieveConfigs = AppSettings.GetSection<SieveOptions>("Sieve");

	public SieveResponse<TResponse> Process(
		SieveModel model,
		IEnumerable<TResponse>? list,
		int totalCount
	)
	{
		Data = list;
		ProcessGeneralProps(model, totalCount);
		return this;
	}

	public SieveResponse<TResponse> Process<TEntity>(
		SieveModel model,
		IEnumerable<TEntity>? list,
		int totalCount,
		IMapper mapper
	)
	{
		Data = mapper.Map<IEnumerable<TResponse>>(list);
		ProcessGeneralProps(model, totalCount);
		return this;
	}

	public int CurrentPage { get; private set; }
	public int PageSize { get; private set; }
	public int TotalItems { get; private set; }
	public int TotalPages { get; private set; }
	public IEnumerable<TResponse>? Data { get; set; }

	private void ProcessGeneralProps(SieveModel model, int totalCount)
	{
		TotalItems = totalCount;
		CurrentPage = model.Page ?? 1;
		PageSize =
			model.PageSize > _sieveConfigs.MaxPageSize
				? _sieveConfigs.MaxPageSize
				: model.PageSize ?? _sieveConfigs.DefaultPageSize;
		TotalPages = (int)Math.Ceiling((float)TotalItems / PageSize);
	}
}
