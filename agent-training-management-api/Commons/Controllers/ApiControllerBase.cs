namespace AtmAPI.Commons.Controllers;

[ApiController]
[RoutePrefix("api")]
[Route("[controller]s")]
public abstract class ApiControllerBase : ControllerBase
{
	internal readonly AtmContext Context;
	internal readonly IMapper Mapper;
	internal readonly IUnitOfWork Uow;
	internal readonly ISieveProcessor SieveProc;

	protected ApiControllerBase(
		AtmContext context,
		IUnitOfWork uow,
		IMapper mapper,
		ISieveProcessor sieveProc
	)
	{
		Uow = uow;
		Mapper = mapper;
		Context = context;
		SieveProc = sieveProc;
	}

	protected async Task HandlePostAsync<TEntity>(
		IEnumerable<TEntity> entitiesToAdd,
		GenericRepository<TEntity>? repository = null
	) where TEntity : class
	{
		entitiesToAdd.ThrowHttpStatus().IfEmpty();

		repository ??= new GenericRepository<TEntity>(Context, SieveProc);

		await repository.InsertRangeAsync(entitiesToAdd);
	}

	protected async Task HandleDeleteAsync<TEntity>(
		Expression<Func<TEntity, bool>> predicate,
		string? includeProperties = null,
		GenericRepository<TEntity>? repository = null
	) where TEntity : class
	{
		repository ??= new GenericRepository<TEntity>(Context, SieveProc);

		var entitiesToDelete = repository.GetQueryable(predicate, includeProperties);

		if (entitiesToDelete.IsAny())
			await repository.DeleteRangeAsync(entitiesToDelete);
	}

	protected async Task<IActionResult> HandleGetAsync<TEntity, TResponse>(
		SieveModel model,
		string? includeProperties = null,
		Expression<Func<TEntity, bool>>? predicate = null,
		GenericRepository<TEntity>? repository = null
	)
		where TEntity : class
		where TResponse : class
	{
		repository ??= new GenericRepository<TEntity>(Context, SieveProc);

		var list = await repository.GetListAsync(model, includeProperties, predicate);
		var count = await repository.CountAsync(model.Filters, includeProperties, predicate);
		var pagination = ResponseSieve.With<TEntity, TResponse>(model, list, count, Mapper);

		return Ok(pagination);
	}
}
