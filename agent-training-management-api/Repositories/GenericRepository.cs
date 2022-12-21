using System.Text.RegularExpressions;

namespace AtmAPI.Repositories;

public class GenericRepository<TEntity> where TEntity : class
{
	private readonly AtmContext _context;
	private readonly ISieveProcessor _sieveProc;

	public GenericRepository(AtmContext context, ISieveProcessor sieveProc)
	{
		_context = context;
		_sieveProc = sieveProc;
	}

	public DbSet<TEntity> EntitySet => _context.Set<TEntity>();

	public async Task DeleteAsync(TEntity entityToDelete)
	{
		EntitySet.Remove(entityToDelete);
		await SaveAsync();
	}

	public async Task DeleteByIdAsync(object id)
	{
		var entityToDelete = await EntitySet.FindAsync(id);
		entityToDelete.ThrowIfNullHttpStatus();
		await DeleteAsync(entityToDelete);
	}

	public async Task DeleteRangeAsync(IEnumerable<TEntity> entitiesToDelete)
	{
		EntitySet.RemoveRange(entitiesToDelete);
		await SaveAsync();
	}

	public async Task<TEntity?> GetByIdAsync(int id, string? includeProperties = null)
	{
		var query = EntitySet.AsQueryable().IncludesSplitQuery(includeProperties);
		return await query.SingleOrDefaultAsync(entity => (entity as EntityBase)!.Id == id);
	}

	public async Task<int> CountAsync(
		string? filters = null,
		string? includeProperties = null,
		Expression<Func<TEntity, bool>>? predicate = null
	)
	{
		var query = EntitySet.AsQueryable().IncludesSplitQuery(includeProperties);

		if (predicate != null)
			query = query.Where(predicate);

		if (filters != null)
		{
			query = _sieveProc.Apply(
				new() { Filters = filters },
				applyPagination: false,
				applySorting: false,
				source: query
			);
		}

		return await query.CountAsync();
	}

	public IQueryable<TEntity> GetSieveQueryable(
		SieveModel? model = null,
		string? includeProperties = null,
		Expression<Func<TEntity, bool>>? predicate = null,
		bool useFiltering = true,
		bool useSorting = true,
		bool usePaging = true,
		bool useSplitQuery = true
	)
	{
		var query = EntitySet.AsQueryable().IncludesSplitQuery(includeProperties, useSplitQuery);
		var useSieveProcessing = model != null;
		model ??= new();

		if (useSplitQuery)
		{
			if (model.Sorts.IsNullOrEmpty())
				model.Sorts = "id";
			else if (!Regex.IsMatch(model.Sorts, @"\b[iI]d|-[iI]d\b"))
				model.Sorts = string.Join(',', new[] { model.Sorts, "id" });
		}

		if (predicate != null)
			query = query.Where(predicate);

		return useSieveProcessing
			? _sieveProc.Apply(model, query, null, useFiltering, useSorting, usePaging)
			: query;
	}

	public async Task<List<TEntity>?> GetListAsync(
		SieveModel? model = null,
		string? includeProperties = null,
		Expression<Func<TEntity, bool>>? predicate = null
	)
	{
		var query = GetSieveQueryable(model, includeProperties, predicate);
		return query != null ? await query.ToListAsync() : null;
	}

	public IQueryable<TEntity> GetQueryable(
		Expression<Func<TEntity, bool>>? predicate = null,
		string? includeProperties = null,
		bool useSplitQuery = true
	) => GetSieveQueryable(null, includeProperties, predicate, useSplitQuery: useSplitQuery);

	public async Task<TEntity> InsertAsync(TEntity entity)
	{
		var entry = await EntitySet.AddAsync(entity);
		await SaveAsync();
		return entry.Entity;
	}

	public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
	{
		EntitySet.AddRange(entities);
		await SaveAsync();
	}

	public async Task UpdateAsync(TEntity entityToUpdate)
	{
		EntitySet.Update(entityToUpdate);
		await SaveAsync();
	}

	public Task<IDbContextTransaction> BeginTransactionAsync() =>
		_context.Database.BeginTransactionAsync();

	private async Task SaveAsync()
	{
		var entitiesSavedCounts = await _context.SaveChangesAsync();
		entitiesSavedCounts.Throw().IfEquals(0);
	}
}
